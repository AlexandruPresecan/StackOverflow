using StackOverflow.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser? Author { get; set; }

        [Required]
        [EnumDataType(typeof(VoteValue))]
        public VoteValue Value { get; set; }
    }
}
