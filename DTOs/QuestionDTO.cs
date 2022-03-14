namespace StackOverflow.DTOs
{
    public class QuestionDTO
    { 
        public string? AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public ICollection<string>? Tags { get; set; }
    }
}
