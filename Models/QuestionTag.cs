using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class QuestionTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [JsonIgnore]
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }

        [Required]
        public int TagId { get; set; }

        [JsonIgnore]
        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
    }
}
