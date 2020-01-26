using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ofx.Battleship.Application.Common.Behaviours;
using System.Reflection;

namespace Ofx.Battleship.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            return services;
        }
    }
}
