using Microsoft.AspNetCore.Identity;
using StackOverflow.Enums;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Score { get; set; }
       
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<QuestionVote>? QuestionVotes { get; set; }
        public ICollection<AnswerVote>? AnswerVotes { get; set; }
    }
}