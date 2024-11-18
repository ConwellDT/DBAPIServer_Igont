using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace DBAPIServer
{
    public class Program
    {
        private static log4net.ILog log;

        public static void Main(string[] args)
        {
            /*
             DBAPIServer --urls http://localhost:5000;https://localhost:5001
             */
            log4net.GlobalContext.Properties["filepath"] = args.Length > 0 && !args[0].Equals("--urls") ? args[0] : "DBAPIServer";// "DBAPIServer";
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Debug(string.Join(" ", args));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
