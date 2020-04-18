using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ofx.Battleship.API.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Infrastructure
{
    public class Server : IAsyncDisposable
    {
        private IHost _server;

        public HttpClient Client;
        public IBattleshipDbContext DbContext => _server.Services.GetRequiredService<IBattleshipDbContext>();

        public Server()
        {
            var port = Ports.GetNextAvailablePort();

            _server = CreateApiHost(port);
            Client = new HttpClient { BaseAddress = new Uri($"http://localhost:{port}") };
        }

        public async Task StartAsync()
        {
            await _server.StartAsync();
        }

        private IHost CreateApiHost(int port)
        {
            var hostBuilder = Program.CreateHostBuilder(null)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options => options.ListenAnyIP(port));
                });

            // We need to call this separately, so that it is after UseStartup and the DbContext is loaded.
            hostBuilder.ConfigureServices((context, services) =>
            {
                // Remove the application BattleshipDbContext registration.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BattleshipDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBattleshipDbContext));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<BattleshipDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    
                }, ServiceLifetime.Singleton);

                services.AddSingleton<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>());
            });

            var host = hostBuilder.Build();
            return host;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await _server.StopAsync(TimeSpan.FromSeconds(3));
                _server?.Dispose();
            }
            catch(Exception) { }
        }
    }
}
