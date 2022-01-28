using api.Setup;
using data;
using data.DataAccess;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using services;

namespace api
{
    public class Startup
    {
        private const string AllowAllOrigin = "_allowAllOrigin";

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();

            services.AddRouting();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOrigin,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.ConfigureExternalCookie(options => { options.Cookie.SameSite = SameSiteMode.None; });

            // database
            var connStr = Configuration.GetConnectionString("AppDbConnection");
            services.AddScoped(_ => new AppDbContext(connStr));

            // services / providers
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    LogManager.GetCurrentClassLogger().Error("Failed error: " + exceptionHandlerPathFeature?.Error);
                });
            });
            app.UseHsts();

            app.UseCors(AllowAllOrigin);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Filter().Count().Expand().Filter().Select().MaxTop(100).OrderBy();
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.MapODataRoute("ODataRoute", "api", AppODataConfigurationBuilder.SetupEdmModel());
            });

            app.UseCookiePolicy();
        }
    }
}
