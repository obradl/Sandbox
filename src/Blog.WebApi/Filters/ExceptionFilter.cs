using System.Linq;
using Blog.ApplicationCore.Common;
using Blog.Domain;
using FluentValidation;
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
                case EntityDoesNotExistsException ex:
                    context.Result = new NotFoundObjectResult(ex.Message);
                    break;
                case BlogDomainException _:
                    var problemDetails = new ValidationProblemDetails
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    var validationException = context.Exception.InnerException as ValidationException;
                    var errors = validationException.Errors.Select(d => d.ErrorMessage).ToArray();
                    problemDetails.Errors.Add("Domain validation errors", errors);

                    context.Result = new BadRequestObjectResult(problemDetails);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
            }
        }
    }
}