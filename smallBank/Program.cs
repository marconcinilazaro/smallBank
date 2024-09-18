using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using smallBank; 

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                // Carregar arquivos JSON de configuração dependendo do ambiente
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                // Carregar segredos do usuário no ambiente de desenvolvimento
                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

    
                if (args != null)
                {
                    config.AddCommandLine(args);
                }

            })
            .UseDefaultServiceProvider((context, options) =>
            {
                // Validação de escopo apenas no desenvolvimento
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            })
            .ConfigureServices((context, services) =>
            {
                // Configurar AutoMapper
                services.AddAutoMapper(typeof(Program)); 

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel();
                webBuilder.UseIISIntegration();
                webBuilder.UseUrls("http://*.localhost:6005");
                webBuilder.UseStartup<Startup>();
            });
    }
}