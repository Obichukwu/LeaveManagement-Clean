namespace HR.LeaveManagement.Application.Models.Email
{
    public class EmailSettings
    {
        public string ApiKey { get; init; } = string.Empty;
        public string FromAddress { get; init; } = string.Empty;
        public string FromName { get; init; } = string.Empty;
    }
}
