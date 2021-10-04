using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sample_Net31
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(opt => opt.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
