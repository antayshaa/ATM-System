namespace ATMSystem.Presentation.Models;

public class DeleteAdminSession
{
    public Guid SessionId { get; set; }

    public string Password { get; set; } = string.Empty;
}