using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class TagsService : IService<int, string>
    {
        private readonly ApplicationDbContext _db;

        public TagsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.Tags
                    .Select
                    (
                        t => new
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Questions = _db.Questions
                                .Include(q => q.QuestionTags)
                                .Where
                                (
                                    q => q.QuestionTags
                                        .Select(qt => qt.TagId)
                                        .Contains(t.Id)
                                )
                                .ToList()
                        }
                    )
            );
        }

        public ServiceResult Get(int id)
        {
            Tag? tag = _db.Tags.FirstOrDefault(t => t.Id == id);

            if (tag == null)
                return new ServiceResult("Tag Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Questions = _db.Questions
                        .Include(q => q.QuestionTags)
                        .Where
                        (
                            q => q.QuestionTags
                                .Select(qt => qt.TagId)
                                .Contains(tag.Id)
                        )
                        .ToList()
                }
            );
        }

        public Tag? GetByName(string name)
        {
            return _db.Tags.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<string?> GetByQuestion(Question question)
        {
            return _db.QuestionTags
                .Include(qt => qt.Tag)
                .Where(qt => qt.QuestionId == question.Id)
                .Select(qt => qt.Tag.Name);
        }

        public ServiceResult Post(string value)
        {
            if (GetByName(value) != null)
                return new ServiceResult("Tag already exists", false);

            Tag tag = new Tag { Name = value };

            _db.Tags.Add(tag);
            _db.SaveChanges();

            return new ServiceResult(tag);
        }

        public ServiceResult Put(int id, string value)
        {
            Tag? tag = _db.Tags.FirstOrDefault(t => t.Id == id);

            if (tag == null)
                return new ServiceResult("Tag Id not found", false);

            if (GetByName(value) != null)
                return new ServiceResult("Tag already exists", false);

            tag.Name = value == null ? tag.Name : value;

            _db.Tags.Update(tag);
            _db.SaveChanges();

            return new ServiceResult(tag);
        }

        public ServiceResult Delete(int id)
        {
            Tag? tag = _db.Tags.FirstOrDefault(t => t.Id == id);

            if (tag == null)
                return new ServiceResult("Tag Id not found", false);

            _db.Tags.Remove(tag);
            _db.SaveChanges();

            return new ServiceResult("Tag deleted");
        }
    }
}
