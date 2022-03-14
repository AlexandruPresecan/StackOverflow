using Microsoft.AspNetCore.Identity;

namespace StackOverflow.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Score =>
        (
            Questions == null ? 0 : Questions
                .Sum
                (
                    q => q.Votes == null ? 0 :
                        5 * q.Votes.Count(v => v.Value == Vote.VoteValue.UpVote) -
                        2 * q.Votes.Count(v => v.Value == Vote.VoteValue.DownVote)
                )
        ) +
        (
            Answers == null ? 0 : Answers
                .Sum
                (
                    a => a.Votes == null ? 0 :
                        10 * a.Votes.Count(v => v.Value == Vote.VoteValue.UpVote) -
                        2 * a.Votes.Count(v => v.Value == Vote.VoteValue.DownVote)
                ) 
        ) -
        (
            AnswerVotes == null ? 0 : AnswerVotes
                .Count(v => v.Value == Vote.VoteValue.DownVote)
        );

        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public ICollection<QuestionVote>? QuestionVotes { get; set; }
        public ICollection<AnswerVote>? AnswerVotes { get; set; }
    }
}