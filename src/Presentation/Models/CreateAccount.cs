namespace ATMSystem.Presentation.Models;

public class CreateAccount
{
    public Guid SessionId { get; set; }

    public string Password { get; set; } = string.Empty;
}