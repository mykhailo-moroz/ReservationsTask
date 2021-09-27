using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reservation.API.Models;
using Reservation.API.Services;
using Reservation.API.Validators;
using Reservation.Data.Context;
using Reservation.Data.Services;

namespace Reservation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ReservationSettings>(Configuration.GetSection(ReservationSettings.OptionsKey));

            services.AddDbContext<ReservationDBContext>(options => options.UseInMemoryDatabase(databaseName: "Reservations"));

            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IReservationService, ReservationService>();

            services.AddControllers();

            services.AddMvc()
                .AddFluentValidation();
            services.AddTransient<IValidator<ReservationModel>, ReservationValidator>();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservations API V1");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
