﻿using ErrorOr;

using FluentValidation;

using MediatR;

namespace SGMC.Test.Application.Common.Behaviors;
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(validationResult => validationResult.Errors)
                .Where(failure => failure != null)
                .Select(failure => Error.Validation(
                    failure.PropertyName,
                    failure.ErrorMessage))
                .ToList();

            if (errors.Count != 0)
            {
                return (dynamic)errors;
            }
        }

        return await next();
    }
}