using System.Linq;
using Blog.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EntityDoesNotExistsException exception:
                    context.Result = new NotFoundObjectResult(exception.Message);
                    break;
                case DomainValidationException exception:
                    var problemDetails = new ValidationProblemDetails
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    problemDetails.Errors.Add("Domain validation errors", exception.Errors.ToArray());

                    context.Result = new BadRequestObjectResult(problemDetails);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
            }
        }
    }
}