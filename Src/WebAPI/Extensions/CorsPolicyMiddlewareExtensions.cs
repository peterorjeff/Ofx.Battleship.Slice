using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ofx.Battleship.WebAPI.Extensions
{
    public static class CorsPolicyMiddlewareExtensions
    {
        public static IApplicationBuilder UseDeveloperCorsPolicy(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                return builder;
            }

            return builder.UseCors(policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        }
    }
}
