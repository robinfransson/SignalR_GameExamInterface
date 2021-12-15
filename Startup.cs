using BullsAndCows;
using BullsAndCows.IO;
using GameEngine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebProject.Controllers;
using WebProject.Hubs;

namespace WebProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public GameController GameController { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSingleton<GameMiddleware>();
            services.AddScoped<IGameUI, GameUI>();
            services.AddScoped<IFileIOWrapper, FileIOWrapper>();
            services.AddScoped<IGameIO>(x => ActivatorUtilities.CreateInstance<FileBasedGameIO>(x, "cowsandbulls.txt", x.GetRequiredService<IFileIOWrapper>()));
            services.AddScoped<IGame, WordGuessingGame>();
            services.AddScoped<GameController>();


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });


            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");


                endpoints.MapHub<GameHub>("/notifications");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.UseCors(x =>
            {
                x.AllowAnyHeader()
                 .AllowAnyMethod()
                 .WithOrigins("http://localhost:3000")
                 .AllowCredentials();
            });
        }
    }
}
