using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? AuthorId { get; set; }

        [JsonIgnore]
        [ForeignKey("AuthorId")]
        public ApplicationUser? Author { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Text { get; set; }

        public DateTime? CreationDate { get; set; }

        public int VoteCount => Votes == null ? 0 : Votes.Count(v => v.Value == Vote.VoteValue.UpVote) - Votes.Count(v => v.Value == Vote.VoteValue.DownVote);

        public ICollection<QuestionTag>? QuestionTags { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<QuestionVote>? Votes { get; set; }
    }
}
