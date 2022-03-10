using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.Contrib.PolymorphTemplateSelector.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseWebRoot("wwwroot")
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                })
                .Build();
    }
}