namespace StackOverflow.Services
{
    public class ServiceResult
    {
        public ServiceResult(object? value, bool success = true)
        {
            Value = value;
            Success = success;
        }

        public object? Value { get; set; }
        public bool Success { get; set; }
    }
}
