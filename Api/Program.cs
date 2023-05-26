using Api.Extentions;
using Api.MiddelWare;
using Core.Entites.Identity;
using Infrastraction.Data;
using Infrastraction.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            builder.Services.AddDbContext<AppDpContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuation = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuation);
            }
            );
            // Add services to the container.
            builder.Services.AddApplicationService();
         
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerDocumentions();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {// Run before swagger
                var services = scope.ServiceProvider;// method get me requird services i need 
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    await context.Database.MigrateAsync();
                    await StoreDbContextSeed.SeedAsync(context, loggerFactory);
                    var userManger = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<AppDpContext>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContxtSeed.SeedUserAsync(userManger);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message, "AN Erro While Seeding the data");
                }
            }
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Api Demo v1"));
                }
            app.UseMiddleware<ExeptionMiddelWare>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDeveloperExceptionPage();
            app.MapControllers();

            app.Run();
        }
    }
}