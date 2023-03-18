using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using MyWebApplication.Data;
using MyWebApplication.DataContext;
using Microsoft.Extensions.Logging;
using MyWebApplication.Data;

namespace MyWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var init = BuildWebHost(args);

            using (var scope = init.Services.CreateScope())
            {
                var s = scope.ServiceProvider;
                var c = s.GetRequiredService<UserContext>();
                DbInitializer.Initialize(c);
            }

            init.Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>

            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(log => log.AddConsole())
                .Build();
    }
}
