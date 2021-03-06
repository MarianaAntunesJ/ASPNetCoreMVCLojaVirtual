using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LojaVirtual
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //Use o startup para inicialização 
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
