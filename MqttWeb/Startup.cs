using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MqttWeb.Services;
using MqttWeb.Data;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MqttWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<IDbConnectionFactory, MqttDbConnectionFactory>(provider => new MqttDbConnectionFactory("Data Source=mqtt.db"));
            services.AddScoped<MqttState>();
            services.AddScoped<MqttService>();
            //services.AddDbContext<MqttContext>(options => options.UseSqlite("Data Source=mqtt.db"));
            //services.AddScoped<MqttContextFactory>();

            services.AddTransient<MqttConfigurationRepository>();
            services.AddTransient<LogRepository>();

                        //Setup data migrations 
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString("Data Source=mqtt.db")
                    // Define the assemblies containing the migrations
                    .ScanIn(typeof(MqttWeb.Data.Migrations.Initial).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner, IDbConnectionFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // Run database migrations 
            migrationRunner.MigrateUp();
            
        }
    }
}
