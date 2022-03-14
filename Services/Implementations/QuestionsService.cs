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

        public QuestionsService(ApplicationDbContext db)
        {
            _db = db;
            _tagsService = new TagsService(db);
            _questionTagsService = new QuestionTagsService(db);
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
                                Score = q.Author.Score
                            },
                            Title = q.Title,
                            CreationDate = q.CreationDate,
                            VoteCount = q.VoteCount,
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

            _db.QuestionTags.Include(qt => qt.Tag);

            return new ServiceResult
            (
                new 
                {
                    Id = question.Id,
                    Author = new
                    {
                        Id = question.AuthorId,
                        UserName = question.Author.UserName,
                        Email = question.Author.Email,
                        Score = question.Author.Score
                    },
                    Title = question.Title,
                    Text = question.Text,
                    CreationDate = question.CreationDate,
                    VoteCount = question.VoteCount,
                    Answers = question.Answers
                        .Select
                        (
                            a => new
                            {
                                Id = a.Id,
                                Author = new
                                {
                                    Id = a.AuthorId,
                                    UserName = a.Author.UserName,
                                    Email = a.Author.Email,
                                    Score = a.Author.Score
                                },
                                CreationDate = a.CreationDate,
                                Text = a.Text,
                                VoteCount = a.VoteCount
                            }
                        ),
                    Tags = _tagsService.GetByQuestion(question)
                }
            );
        }

        public ServiceResult Post(QuestionDTO value)
        {
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

            foreach (string name in value.Tags)
            {
                Tag? tag = _tagsService.GetByName(name);

                if (tag == null)
                {
                    _tagsService.Post(name);
                    tag = _tagsService.GetByName(name);
                }

                _questionTagsService.Post(new QuestionTagDTO() { QuestionId = question.Id, TagId = tag.Id });
            }

            return new ServiceResult("Question created");
        }

        public ServiceResult Put(int id, QuestionDTO value)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == id);

            if (question == null)
                return new ServiceResult("Question Id not found", false);

            question.Text = value.Text == null ? question.Text : value.Text;
            question.Title = value.Title == null ? question.Title : value.Title;

            _db.Questions.Update(question);
            _db.SaveChanges();

            return new ServiceResult("Question updated");
        }

        public ServiceResult Delete(int id)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == id);

            if (question == null)
                return new ServiceResult("Question Id not found", false);

            _db.Questions.Remove(question);
            _db.SaveChanges();

            return new ServiceResult("Question deleted");
        }
    }
}
