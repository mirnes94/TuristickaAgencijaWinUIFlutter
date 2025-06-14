using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TuristickaAgencija.Services.Database;
//using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            
            var host=CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TuristickaAgencijaContext>();
                Data.Seed(service);
            }

            host.Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
