using System.IO;
using Broker.Logger;
using DataAccess.Commands;
using DataAccess.Commands.Interfaces;
using DataAccess.DataBases;
using DataAccess.Mapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            UpdateDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "logger.txt");
            File.Delete(logPath);
            loggerFactory.AddFile(logPath);
            var logger = loggerFactory.CreateLogger("FileLogger");
            
            app.Run(async (context) => 
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);
                logger.LogCritical("Processing request {0}", context.Request.Path);
                logger.LogDebug("Processing request {0}", context.Request.Path);
                logger.LogError("Processing request {0}", context.Request.Path);
                logger.LogTrace("Processing request {0}", context.Request.Path);
                logger.LogWarning("Processing request {0}", context.Request.Path);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<BooksDbContext>();
            ConfigureMassTransit(services);
            services.AddTransient<IBookResponseMapper, BookResponseMapper>();
            services.AddTransient<IDbBookMapper, DbBookMapper>();
            services.AddTransient<IDbAuthorBookMapper, DbAuthorBookMapper>();
            services.AddTransient<IDeleteBookCommand, DeleteBookCommand>();
            services.AddTransient<IPostBookCommand, PostBookCommand>();
            services.AddTransient<IGetBookCommand, GetBookCommand>();
            services.AddTransient<IPutBookCommand, PutBookCommand>();
        }

        private void ConfigureMassTransit(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BookConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("service2");
                        host.Password("service2");
                    });

                    cfg.ReceiveEndpoint(
                        "Book",
                        ep => { ep.ConfigureConsumer<BookConsumer>(context); });
                });
            });

            services.AddMassTransitHostedService();
        }
        
        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<BooksDbContext>();

            context?.Database.Migrate();
        }
    }
}