using StackOverflow.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser? Author { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }

        public int VoteCount => Votes == null ? 0 : 
            Votes.Count(v => v.Value == VoteValue.UpVote) - 
            Votes.Count(v => v.Value == VoteValue.DownVote);

        public ICollection<AnswerVote>? Votes { get; set; }
    }
}
