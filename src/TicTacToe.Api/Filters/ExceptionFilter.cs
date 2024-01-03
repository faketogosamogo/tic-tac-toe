using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicTacToe.Api.Exceptions;

namespace TicTacToe.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is GameNotFoundException)
        {
            context.Result = new NotFoundResult();
        }
        else if (context.Exception is CoordinatesDuplicateException or GameEndedException)
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = context.Exception.Message
            };
        }
    }
}