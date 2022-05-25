using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Score { get; set; }
        public bool Banned { get; set; }
       
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<QuestionVote>? QuestionVotes { get; set; }
        public ICollection<AnswerVote>? AnswerVotes { get; set; }
    }
}