using DataAccess.Commands;
using DataAccess.Commands.Interfaces;
using DataAccess.Mapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccess
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
            services.AddTransient<IBookMapper, BookMapper>();
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

                    cfg.ReceiveEndpoint("Book", ep => { ep.ConfigureConsumer<BookConsumer>(context); });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}