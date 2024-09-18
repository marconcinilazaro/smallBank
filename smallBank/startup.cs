using Lw.Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using smallBank.Service.Services.Interface;
using smallBank.Service.Services;
using System.Net;
using System.Reflection;
using System.Text;
using smallBank.Infra.Interfaces;
using smallBank.Infra.Context;
using smallBank.Service.AutoMapper;

namespace smallBank
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuring migrations to IdentityServer4
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IAccountTransactionService, AccountTransactionService>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            #region Contexts
            // Injeção do contexto do banco de dados MySQL
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("Conexao")));


            #endregion


            #region API

            services
                .AddRouting()
                .AddControllers()
                .AddControllersAsServices();

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmallBank",
                    Version = "v1",
                    Description = "API para controle de saldo bancário",
                    Contact = new OpenApiContact
                    {
                        Name = "Small Bank",
                        Url = new Uri("https://smallbank.com/contact")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            services.Configure<IISOptions>(options => { options.ForwardClientCertificate = false; });
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
                builder.WithOrigins("http://front.com")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        // Log the exception or handle it
                    }
                });
            });

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmallBank API V1");
                c.RoutePrefix = string.Empty; 
            });

            #endregion

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
