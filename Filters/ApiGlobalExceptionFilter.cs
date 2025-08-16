using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProvaPub.Exceptions;

namespace ProvaPub.Filters
{
    public class ApiGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public ApiGlobalExceptionFilter(IHostEnvironment env)
            => _env = env;

        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();
            var exception = context.Exception;

            // Adiciona stack trace apenas no desenvolvimento
            if (_env.IsDevelopment())
                details.Extensions.Add("StackTrace", exception.StackTrace);

            if (exception is EntityValidationException validationEx)
            {
                details.Title = "One or more validation errors occurred";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnprocessableEntity";

                // Retorna lista de mensagens de erro
                if (validationEx.Errors?.Any() == true)
                    details.Extensions.Add("Errors", validationEx.Errors.Select(e => e.Message).ToList());
                else
                    details.Detail = validationEx.Message;
            }
            else if (exception is NotFoundException)
            {
                details.Title = "Not Found";
                details.Status = StatusCodes.Status404NotFound;
                details.Type = "NotFound";
                details.Detail = exception.Message;
            }
            else
            {
                details.Title = "An unexpected error occurred";
                details.Status = StatusCodes.Status500InternalServerError;
                details.Type = "UnexpectedError";
                details.Detail = exception.Message;
            }

            context.HttpContext.Response.StatusCode = (int)details.Status;
            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
