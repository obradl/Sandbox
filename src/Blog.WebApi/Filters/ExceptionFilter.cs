using System.Linq;
using System.Net;
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
                case BlogDomainException ex:
                    var problemDetails = new ValidationProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    var ex2 = context.Exception.InnerException as ValidationException;
                    var h = ex2.Errors.Select(d => d.ErrorMessage).ToArray();
                    problemDetails.Errors.Add("DomainValidations", h);

                    context.Result = new BadRequestObjectResult(problemDetails);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }
        }
    }
}