using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityDoesNotExistsException ex)
            {
                context.Result = new NotFoundObjectResult(ex.Message);
            }
            else
            {
                context.Result = new JsonResult("An error occured");
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
