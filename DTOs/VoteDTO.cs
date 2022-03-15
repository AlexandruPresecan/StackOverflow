using StackOverflow.Enums;

namespace StackOverflow.DTOs
{
    public class VoteDTO
    {
        public string? AuthorId { get; set; }
        public VoteValue Value { get; set; }
    }
}
