using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MathHunt.DataAccess.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ValidationException = FluentValidation.ValidationException;

namespace MathHunt.DataAccess.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        
        // var failures = validators
        //     .Select(x => x.Validate(context))
        //     .SelectMany(x => x.Errors)
        //     .Where(x => x is not null)
        //     .GroupBy(
        //         x=>x.PropertyName.Substring(x.PropertyName.IndexOf('.') + 1),
        //         x=>x.ErrorMessage, (propertyName, errorMessages) => new
        //         {
        //             Key = propertyName,
        //             Values = errorMessages.Distinct().ToArray()
        //         })
        //     .ToDictionary(x=>x.Key, x=>x.Values);

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}