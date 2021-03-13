using System;
using System.IO;
using Broker.Commands;
using Broker.Logger;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;

namespace Broker
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "logger.txt");
            File.Delete(logPath);
            loggerFactory.AddFile(logPath);
            var logger = loggerFactory.CreateLogger("FileLogger");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureMassTransit(services);
            services.AddTransient<
                IRabbitRequestCommand<BookResponse, BookRequest>,
                RabbitRequestCommand<BookResponse, BookRequest>
            >();
        }

        private void ConfigureMassTransit(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("service1");
                        host.Password("service1");
                    });
                });

                x.AddRequestClient<BookRequest>(new Uri("rabbitmq://localhost/Book"));
            });

            services.AddMassTransitHostedService();
        }
    }
}