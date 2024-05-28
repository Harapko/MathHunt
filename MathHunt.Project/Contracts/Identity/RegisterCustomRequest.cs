namespace MathHunt.Contracts.Identity;

public record RegisterCustomRequest(
    string userName,
    string userSurname,
    string email,
    string phoneNumber,
    string password,
    string role
    )
{
    
}