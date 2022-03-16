using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CertIdentityTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args); //.Build().Run();
            var logFactory = new LoggerFactory();

            var logger = logFactory.CreateLogger<Program>();


            hostBuilder.ConfigureAppConfiguration((hostingcontext, config) =>
            {
                //Docker
                config.AddKeyPerFile(directoryPath: "/run/secrets", optional: false);
                //"mysecret.txt","WARMACHINEROX"

                Console.WriteLine($"DIRECTORY => {Directory.GetCurrentDirectory()}");
                //VS'den çalýþtýrýrken
                //config.AddKeyPerFile(directoryPath: Path.Combine(Directory.GetCurrentDirectory(),"mysecretfolder"), optional: false);
            }).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
