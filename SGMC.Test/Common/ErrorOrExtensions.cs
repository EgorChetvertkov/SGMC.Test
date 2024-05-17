using ErrorOr;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SGMC.Test.Common;

public static class ErrorOrExtensions
{
    public static IActionResult GetProblemDetails(this List<Error> errors, ControllerBase controller)
    {
        if (errors.Count == 0)
        {
            return controller.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return controller.ValidationProblem(modelStateDictionary);
        }

        Error firstError = errors.FirstOrDefault();

        int statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Unexpected => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError,
        };

        return controller.Problem(
            firstError.Description,
            null,
            statusCode,
            firstError.Code,
            firstError.Type.ToString());
    }
}
