namespace StackOverflow.DTOs
{
    public class ApplicationUserDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Email { get; set; }
    }
}