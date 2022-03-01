using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Score => 0;

        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<QuestionVote>? QuestionVotes { get; set; }
        public ICollection<AnswerVote>? AnswerVotes { get; set; }
    }
}