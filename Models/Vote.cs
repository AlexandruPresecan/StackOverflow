using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class Vote
    {
        public enum VoteValue { UpVote, DownVote }

        [Key]
        public int Id { get; set; }

        [Required]
        public string? AuthorId { get; set; }

        [JsonIgnore]
        [ForeignKey("AuthorId")]
        public ApplicationUser? Author { get; set; }

        [Required]
        public VoteValue Value { get; set; }
    }
}
