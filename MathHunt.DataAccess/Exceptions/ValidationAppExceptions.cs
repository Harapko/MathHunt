namespace MathHunt.DataAccess.Exceptions;

public class ValidationAppExceptions(IReadOnlyDictionary<string, string[]> errors) : Exception("One or more validations errors occurred")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}