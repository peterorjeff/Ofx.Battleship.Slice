using Microsoft.AspNetCore.Builder;
using Ofx.Battleship.WebAPI.Middleware;

namespace Ofx.Battleship.WebAPI.Extensions
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
