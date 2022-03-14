using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class QuestionVotesService : IService<int, QuestionVoteDTO>
    {
        private readonly ApplicationDbContext _db;

        public QuestionVotesService(ApplicationDbContext db)
        {
            _db = db;
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

        public ServiceResult Post(QuestionVoteDTO value)
        {
            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId) == null)
                return new ServiceResult("Question Id not found", false);

            if (_db.QuestionVotes.FirstOrDefault(v => v.AuthorId == value.AuthorId && v.QuestionId == value.QuestionId) != null)
                return new ServiceResult("User already voted this question", false);

            _db.QuestionVotes.Add(new QuestionVote { AuthorId = value.AuthorId, QuestionId = value.QuestionId, Value = value.Value });
            _db.SaveChanges();

            return new ServiceResult("Question Vote created");
        }

        public ServiceResult Put(int id, QuestionVoteDTO value)
        {
            QuestionVote? vote = _db.QuestionVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Question Vote Id not found", false);

            vote.Value = value.Value;

            _db.QuestionVotes.Update(vote);
            _db.SaveChanges();

            return new ServiceResult("Question Vote updated");
        }

        public ServiceResult Delete(int id)
        {
            QuestionVote? vote = _db.QuestionVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Question Vote Id not found", false);

            _db.QuestionVotes.Remove(vote);
            _db.SaveChanges();

            return new ServiceResult("Question Vote deleted");
        }
    }
}
