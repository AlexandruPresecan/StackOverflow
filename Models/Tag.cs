using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<QuestionTag>? QuestionTags { get; set; }
    }
}
