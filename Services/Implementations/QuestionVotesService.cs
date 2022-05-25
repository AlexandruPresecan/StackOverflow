using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Enums;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class QuestionVotesService : IService<int, QuestionVoteDTO>
    {
        private readonly ApplicationDbContext _db;
        private readonly ApplicationUsersService _usersService;

        public QuestionVotesService(ApplicationDbContext db, ApplicationUsersService usersService)
        {
            _db = db;
            _usersService = usersService;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.QuestionVotes.Select
                (
                    v => new 
                    { 
                        Id = v.Id, 
                        AuthorId = v.AuthorId,
                        QuestionId = v.QuestionId, 
                        Value = v.Value
                    }
                )
            );
        }

        public ServiceResult Get(int id)
        {
            QuestionVote? vote = _db.QuestionVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Question Vote Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = vote.Id,
                    AuthorId = vote.AuthorId,
                    QuestionId = vote.QuestionId,
                    Value = vote.Value
                }
            );
        }

        public ServiceResult Post(QuestionVoteDTO value, HttpContext httpContext)
        {
            if (value.AuthorId == null || !Convert.ToBoolean(httpContext.Items["Admin"]))
                value.AuthorId = httpContext.Items["UserId"]?.ToString();

            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId) == null)
                return new ServiceResult("Question Id not found", false);

            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId)?.AuthorId == value.AuthorId)
                return new ServiceResult("Cannot vote your own question", false);

            if (_db.QuestionVotes.FirstOrDefault(v => v.AuthorId == value.AuthorId && v.QuestionId == value.QuestionId) != null)
                return new ServiceResult("User already voted this question", false);

            _db.QuestionVotes.Add(new QuestionVote { AuthorId = value.AuthorId, QuestionId = value.QuestionId, Value = value.Value });
            _db.SaveChanges();

            Question? question = _db.Questions.FirstOrDefault(q => q.Id == value.QuestionId);
            _usersService.UpdateScore(question.AuthorId, value.Value == VoteValue.UpVote ? 5 : -2);

            return new ServiceResult("Question Vote created");
        }

        public ServiceResult Put(int id, QuestionVoteDTO value, HttpContext httpContext)
        {
            QuestionVote? vote = _db.QuestionVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Question Vote Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != vote.AuthorId)
                return new ServiceResult("Vote does not belong to user", false);

            bool voteChanged = vote.Value != value.Value;
            vote.Value = value.Value;

            _db.QuestionVotes.Update(vote);
            _db.SaveChanges();

            if (voteChanged)
            {
                Question? question = _db.Questions.FirstOrDefault(q => q.Id == vote.QuestionId);
                _usersService.UpdateScore(question.AuthorId, vote.Value == VoteValue.UpVote ? 7 : -7);
            }

            return new ServiceResult("Question Vote updated");
        }

        public ServiceResult Delete(int id, HttpContext httpContext)
        {
            QuestionVote? vote = _db.QuestionVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Question Vote Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != vote.AuthorId)
                return new ServiceResult("Vote does not belong to user", false);

            Question? question = _db.Questions.FirstOrDefault(q => q.Id == vote.QuestionId);
            _usersService.UpdateScore(question.AuthorId, vote.Value == VoteValue.UpVote ? -5 : 2);

            _db.QuestionVotes.Remove(vote);
            _db.SaveChanges();

            return new ServiceResult("Question Vote deleted");
        }
    }
}
