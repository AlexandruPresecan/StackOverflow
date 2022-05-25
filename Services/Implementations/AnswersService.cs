using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class AnswersService : IService<int, AnswerDTO>
    {
        private readonly ApplicationDbContext _db;

        public AnswersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.Answers
                    .Include(a => a.Votes)
                    .Include(a => a.Author)
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
                                Score = a.Author.Score,
                                Banned = a.Author.Banned
                            },
                            Text = a.Text,
                            CreationDate = a.CreationDate,
                            VoteCount = a.VoteCount,
                            Votes = a.Votes
                                .Select
                                (
                                    v => new
                                        {
                                            Id = v.Id,
                                            AuthorId = v.AuthorId,
                                            Value = v.Value,
                                        }
                                ),
                            QuestionId = a.QuestionId
                        }
                    )
            );
        }

        public ServiceResult Get(int id)
        {
            Answer? answer = _db.Answers
                .Include(a => a.Votes)
                .Include(a => a.Author)
                .Include(a => a.Question)
                .FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = answer.Id,
                    Author = new
                    {
                        Id = answer.AuthorId,
                        UserName = answer.Author?.UserName,
                        Email = answer.Author?.Email,
                        Score = answer.Author?.Score,
                        Banned = answer.Author?.Banned
                    },
                    Text = answer.Text,
                    CreationDate = answer.CreationDate,
                    VoteCount = answer.VoteCount,
                    Votes = answer.Votes?
                        .Select
                        (
                            v => new
                            {
                                Id = v.Id,
                                AuthorId = v.AuthorId,
                                Value = v.Value,
                            }
                        ),
                    QuestionId = answer.QuestionId
                }
            );
        }

        public IEnumerable<object> GetByQuestion(Question question)
        {
            return _db.Answers
                    .Include(a => a.Votes)
                    .Include(a => a.Author)
                    .Where(a => a.QuestionId == question.Id)
                    .ToList()
                    .OrderByDescending(a => a.VoteCount)
                    .Select
                    (
                        a => new
                        {
                            Id = a.Id,
                            Author = new
                            {
                                Id = a.AuthorId,
                                UserName = a.Author?.UserName,
                                Email = a.Author?.Email,
                                Score = a.Author?.Score,
                                Banned = a.Author?.Banned
                            },
                            Text = a.Text,
                            CreationDate = a.CreationDate,
                            VoteCount = a.VoteCount,
                            Votes = a.Votes?
                                .Select
                                (
                                    v => new
                                    {
                                        Id = v.Id,
                                        AuthorId = v.AuthorId,
                                        Value = v.Value,
                                    }
                                ),
                            QuestionId = a.QuestionId
                        }
                    );
        }

        public ServiceResult Post(AnswerDTO value, HttpContext httpContext)
        {
            if (value.AuthorId == null || !Convert.ToBoolean(httpContext.Items["Admin"]))
                value.AuthorId = httpContext.Items["UserId"]?.ToString();

            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId) == null)
                return new ServiceResult("Question Id not found", false);

            Answer answer = new Answer()
            {
                AuthorId = value.AuthorId,
                QuestionId = value.QuestionId,
                CreationDate = DateTime.Now,
                Text = value.Text
            };

            _db.Answers.Add(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer created");
        }

        public ServiceResult Put(int id, AnswerDTO value, HttpContext httpContext)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != answer.AuthorId)
                return new ServiceResult("Answer does not belong to user", false);

            answer.Text = value.Text == null ? answer.Text : value.Text;

            _db.Answers.Update(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer updated");
        }

        public ServiceResult Delete(int id, HttpContext httpContext)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != answer.AuthorId)
                return new ServiceResult("Answer does not belong to user", false);

            foreach (AnswerVote vote in _db.AnswerVotes.Where(v => v.AnswerId == answer.Id))
                _db.AnswerVotes.Remove(vote);

            _db.Answers.Remove(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer deleted");
        }

        public int GetVoteCount(int id)
        {
            Answer? answer = _db.Answers.Include(a => a.Votes).FirstOrDefault(a => a.Id == id);
            return answer == null ? 0 : answer.VoteCount;
        }
    }
}
