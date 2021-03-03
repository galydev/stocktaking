using Inventory.Api.Extensions;
using Inventory.Api.Middlewares;
using Inventory.Application.Behaviors;
using Inventory.Application.Mappings;
using Inventory.Application.Wrappers;
using Inventory.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Linq;

namespace Inventory.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AutomapperProfile());
            });

            services.AddDatabaseContext(Configuration.GetConnectionString("InventoryDatabase"));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddServices();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddSwagger();
            services.AddValidationFilters();
            services.AddCorsPolicy();
            services.AddMediators();
            services.AddHealthChecks().AddDbContextCheck<InventoryContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API");
                options.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async(context, report) => 
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });

            app.UseRouting();
            app.UseCors("CorsPolicyInventory");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}