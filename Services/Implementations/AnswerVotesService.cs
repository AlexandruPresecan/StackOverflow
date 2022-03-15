using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class AnswerVotesService : IService<int, AnswerVoteDTO>
    {
        private readonly ApplicationDbContext _db;

        public AnswerVotesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.AnswerVotes.Select
                (
                    v => new 
                    { 
                        Id = v.Id, 
                        AuthorId = v.AuthorId,
                        AnswerId = v.AnswerId, 
                        Value = v.Value
                    }
                )
            );
        }

        public ServiceResult Get(int id)
        {
            AnswerVote? vote = _db.AnswerVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Answer Vote Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = vote.Id,
                    AuthorId = vote.AuthorId,
                    AnswerId = vote.AnswerId,
                    Value = vote.Value
                }
            );
        }

        public ServiceResult Post(AnswerVoteDTO value)
        {
            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            if (_db.Answers.FirstOrDefault(a => a.Id == value.AnswerId) == null)
                return new ServiceResult("Answer Id not found", false);

            if (_db.AnswerVotes.FirstOrDefault(v => v.AuthorId == value.AuthorId && v.AnswerId == value.AnswerId) != null)
                return new ServiceResult("User already voted this answer", false);

            _db.AnswerVotes.Add(new AnswerVote { AuthorId = value.AuthorId, AnswerId = value.AnswerId, Value = value.Value });
            _db.SaveChanges();

            return new ServiceResult("Answer Vote created");
        }

        public ServiceResult Put(int id, AnswerVoteDTO value)
        {
            AnswerVote? vote = _db.AnswerVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Answer Vote Id not found", false);

            vote.Value = value.Value;

            _db.AnswerVotes.Update(vote);
            _db.SaveChanges();

            return new ServiceResult("Answer Vote updated");
        }

        public ServiceResult Delete(int id)
        {
            AnswerVote? vote = _db.AnswerVotes.FirstOrDefault(v => v.Id == id);

            if (vote == null)
                return new ServiceResult("Answer Vote Id not found", false);

            _db.AnswerVotes.Remove(vote);
            _db.SaveChanges();

            return new ServiceResult("Answer Vote deleted");
        }
    }
}
