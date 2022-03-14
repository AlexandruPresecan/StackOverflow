using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class QuestionTagsService : IService<int, QuestionTagDTO>
    {
        private readonly ApplicationDbContext _db;

        public QuestionTagsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.QuestionTags.Select
                (
                    qt => new 
                    { 
                        Id = qt.Id, 
                        QuestionId = qt.QuestionId, 
                        TagId = qt.TagId 
                    }
                )
            );
        }

        public ServiceResult Get(int id)
        {
            QuestionTag? questionTag = _db.QuestionTags.FirstOrDefault(t => t.Id == id);

            if (questionTag == null)
                return new ServiceResult("Question Tag Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = questionTag.Id,
                    QuestionId = questionTag.QuestionId,
                    TagId = questionTag.TagId
                }
            );
        }

        public ServiceResult Post(QuestionTagDTO value)
        {
            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId) == null)
                return new ServiceResult("Question Id not found", false);

            if (_db.Tags.FirstOrDefault(t => t.Id == value.TagId) == null)
                return new ServiceResult("Tag Id not found", false);

            _db.QuestionTags.Add(new QuestionTag { QuestionId = value.QuestionId, TagId = value.TagId });
            _db.SaveChanges();

            return new ServiceResult("Question Tag created");
        }

        public ServiceResult Put(int id, QuestionTagDTO value)
        {
            QuestionTag? questionTag = _db.QuestionTags.FirstOrDefault(qt => qt.Id == id);

            if (questionTag == null)
                return new ServiceResult("Question Tag Id not found", false);

            return new ServiceResult("Question Tag updated");
        }

        public ServiceResult Delete(int id)
        {
            QuestionTag? questionTag = _db.QuestionTags.FirstOrDefault(qt => qt.Id == id);

            if (questionTag == null)
                return new ServiceResult("Question Tag Id not found", false);

            _db.QuestionTags.Remove(questionTag);
            _db.SaveChanges();

            return new ServiceResult("Quesetion Tag deleted");
        }
    }
}
