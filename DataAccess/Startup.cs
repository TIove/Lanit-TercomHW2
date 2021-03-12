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

namespace DataAccess
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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