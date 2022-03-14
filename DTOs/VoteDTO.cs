using StackOverflow.Models;

namespace StackOverflow.DTOs
{
    public class VoteDTO
    {
        public string? AuthorId { get; set; }
        public Vote.VoteValue Value { get; set; }
    }
}
