namespace MathHunt.Contracts.Identity;

public record RegisterCustomRequest(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string password,
    string role
    )
{
    
}