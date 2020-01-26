using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ofx.Battleship.Application.Common.Interfaces;

namespace Ofx.Battleship.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // Use In-Memory Database for simplicity here, can avoid standing up a db somewhere.
            // Specify a constant name, so we get the same db across scoped lifetimes.
            services.AddDbContext<BattleshipDbContext>(options =>
                options.UseInMemoryDatabase("BattleshipTestDatabase"));

            services.AddScoped<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>());

            return services;
        }
    }
}
