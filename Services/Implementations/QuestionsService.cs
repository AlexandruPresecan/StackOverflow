using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class QuestionsService : IService<int, QuestionDTO>
    {
        private readonly ApplicationDbContext _db;
        private readonly TagsService _tagsService;
        private readonly QuestionTagsService _questionTagsService;
        private readonly AnswersService _answersService;

        public QuestionsService(ApplicationDbContext db, AnswersService answersService, QuestionTagsService questionTagsService, TagsService tagsService)
        {
            _db = db;
            _tagsService = tagsService;
            _questionTagsService = questionTagsService;
            _answersService = answersService;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.Questions
                    .Include(q => q.Votes)
                    .Include(q => q.Author)
                    .OrderBy(q => q.CreationDate)
                    .Select
                    (
                        q => new
                        {
                            Id = q.Id,
                            Author = new
                            {
                                Id = q.AuthorId,
                                UserName = q.Author.UserName,
                                Email = q.Author.Email,
                                Score = q.Author.Score,
                                Banned = q.Author.Banned
                            },
                            Title = q.Title,
                            Text = q.Text,
                            CreationDate = q.CreationDate,
                            VoteCount = q.VoteCount,
                            Votes = q.Votes
                                .Select
                                (
                                    v => new
                                    {
                                        Id = v.Id,
                                        AuthorId = v.AuthorId,
                                        Value = v.Value,
                                    }
                                ),
                            Tags = _tagsService.GetByQuestion(q)
                        }
                    )
            );
        }

        public ServiceResult GetByTag(string tagName)
        {
            Tag? tag = _tagsService.GetByName(tagName);

            if (tag == null)
                return new ServiceResult("Tag not found", false);

            return new ServiceResult
            (
                _db.Questions
                    .Include(q => q.Votes)
                    .Include(q => q.Author)
                    .Include(q => q.QuestionTags)
                    .Where
                    (
                        q => q.QuestionTags
                            .Select(qt => qt.TagId)
                            .Contains(tag.Id)
                    )
                    .Select
                    (
                        q => new
                        {
                            Id = q.Id,
                            Author = new
                            {
                                Id = q.AuthorId,
                                UserName = q.Author.UserName,
                                Email = q.Author.Email,
                                Score = q.Author.Score,
                                Banned = q.Author.Banned
                            },
                            Title = q.Title,
                            Text = q.Text,
                            CreationDate = q.CreationDate,
                            VoteCount = q.VoteCount,
                            Votes = q.Votes
                                .Select
                                (
                                    v => new
                                    {
                                        Id = v.Id,
                                        AuthorId = v.AuthorId,
                                        Value = v.Value,
                                    }
                                ),
                            Tags = _tagsService.GetByQuestion(q)
                        }
                    )
            );
        }

        public ServiceResult GetByName(string name)
        {
            return new ServiceResult
            (
                _db.Questions
                    .Include(q => q.Votes)
                    .Include(q => q.Author)
                    .Include(q => q.QuestionTags)
                    .Where(q => q.Title.Contains(name))
                    .Select
                    (
                        q => new
                        {
                            Id = q.Id,
                            Author = new
                            {
                                Id = q.AuthorId,
                                UserName = q.Author.UserName,
                                Email = q.Author.Email,
                                Score = q.Author.Score,
                                Banned = q.Author.Banned
                            },
                            Title = q.Title,
                            Text = q.Text,
                            CreationDate = q.CreationDate,
                            VoteCount = q.VoteCount,
                            Votes = q.Votes
                                .Select
                                (
                                    v => new
                                    {
                                        Id = v.Id,
                                        AuthorId = v.AuthorId,
                                        Value = v.Value,
                                    }
                                ),
                            Tags = _tagsService.GetByQuestion(q)
                        }
                    )
            );
        }

        public ServiceResult Get(int id)
        {
            Question? question = _db.Questions
                .Include(q => q.Votes)
                .Include(q => q.Answers)
                .Include(q => q.Author)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
                return new ServiceResult("Question Id not found", false);

            return new ServiceResult
            (
                new 
                {
                    Id = question.Id,
                    Author = new
                    {
                        Id = question.AuthorId,
                        UserName = question.Author?.UserName,
                        Email = question.Author?.Email,
                        Score = question.Author?.Score,
                        Banned = question.Author?.Banned
                    },
                    Title = question.Title,
                    Text = question.Text,
                    CreationDate = question.CreationDate,
                    VoteCount = question.VoteCount,
                    Votes = question.Votes?
                        .Select
                        (
                            v => new
                            {
                                Id = v.Id,
                                AuthorId = v.AuthorId,
                                Value = v.Value,
                            }
                        ),
                    Answers = _answersService.GetByQuestion(question),
                    Tags = _tagsService.GetByQuestion(question)
                }
            );
        }

        public ServiceResult Post(QuestionDTO value, HttpContext httpContext)
        {
            if (value.AuthorId == null || !Convert.ToBoolean(httpContext.Items["Admin"]))
                value.AuthorId = httpContext.Items["UserId"]?.ToString();

            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            Question question = new Question()
            {
                AuthorId = value.AuthorId,
                CreationDate = DateTime.Now,
                Text = value.Text,
                Title = value.Title,
            };

            _db.Questions.Add(question);
            _db.SaveChanges();

            if (value.Tags != null)
                foreach (string name in value.Tags)
                {
                    Tag? tag = _tagsService.GetByName(name);

                    if (tag == null)
                    {
                        _tagsService.Post(name, httpContext);
                        tag = _tagsService.GetByName(name);
                    }

                    _questionTagsService.Post(new QuestionTagDTO() { QuestionId = question.Id, TagId = tag.Id }, httpContext);
                }

            return new ServiceResult("Question created");
        }

        public ServiceResult Put(int id, QuestionDTO value, HttpContext httpContext)
        {
            Question? question = _db.Questions.Include(q => q.QuestionTags).FirstOrDefault(q => q.Id == id);

            if (question == null)
                return new ServiceResult("Question Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != question.AuthorId)
                return new ServiceResult("Question does not belong to user", false);

            question.Text = value.Text == null ? question.Text : value.Text;
            question.Title = value.Title == null ? question.Title : value.Title;

            _db.Questions.Update(question);
            _db.SaveChanges();

            foreach (int questionTagId in question.QuestionTags.Select(qt => qt.Id))
                _questionTagsService.Delete(questionTagId, httpContext);

            if (value.Tags != null)
                foreach (string name in value.Tags)
                {
                    Tag? tag = _tagsService.GetByName(name);

                    if (tag == null)
                    {
                        _tagsService.Post(name, httpContext);
                        tag = _tagsService.GetByName(name);
                    }

                    _questionTagsService.Post(new QuestionTagDTO() { QuestionId = question.Id, TagId = tag.Id }, httpContext);
                }

            return new ServiceResult("Question updated");
        }

        public ServiceResult Delete(int id, HttpContext httpContext)
        {
            Question? question = _db.Questions.Include(q => q.QuestionTags).FirstOrDefault(q => q.Id == id);

            if (question == null)
                return new ServiceResult("Question Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != question.AuthorId)
                return new ServiceResult("Question does not belong to user", false);

            foreach (int questionTagId in question.QuestionTags.Select(qt => qt.Id))
                _questionTagsService.Delete(questionTagId, httpContext);

            foreach (QuestionVote vote in _db.QuestionVotes.Where(v => v.QuestionId == question.Id))
                _db.QuestionVotes.Remove(vote);

            _db.Questions.Remove(question);
            _db.SaveChanges();

            return new ServiceResult("Question deleted");
        }

        public int GetVoteCount(int id)
        {
            Question? question = _db.Questions.Include(q => q.Votes).FirstOrDefault(q => q.Id == id);
            return question == null ? 0 : question.VoteCount;
        }
    }
}
