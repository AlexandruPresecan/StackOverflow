using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    public class AnswerVote : Vote 
    {
        [Required]
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer? Answer { get; set; }
    }
}
