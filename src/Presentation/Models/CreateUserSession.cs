namespace ATMSystem.Presentation.Models;

public class CreateUserSession
{
    public Guid AccountId { get; set; }

    public string Password { get; set; } = string.Empty;
}