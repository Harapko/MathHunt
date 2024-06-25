using MathHunt.Contracts.CompanyContract;
using MathHunt.Core.Models;

namespace MathHunt.Contracts.Identity;


public record GETAllUserResponse(
    string id,
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string gitHubLink,
    string descriptionSkill,
    string role,
    string? photoPath,
    DateTime? lockEnd,
    bool isLock
    );



