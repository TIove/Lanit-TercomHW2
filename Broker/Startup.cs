using System;
using Broker.Commands;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace Broker
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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