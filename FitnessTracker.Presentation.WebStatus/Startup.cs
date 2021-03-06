﻿using FitnessTracker.Presentation.WebStatus.BackgroundProcesses;
using FitnessTracker.Presentation.WebStatus.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitnessTracker.Presentation.WebStatus
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHealthChecksUI();  // add health check UI

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<KeepAlive>(Configuration.GetSection("KeepAlive"));  // load config settings for keepalive
            // Add background service
            services.AddSingleton<IHostedService, WebStatusHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseHealthChecksUI();

            var options = new RewriteOptions()
           .AddRedirect("(.*)", "/healthchecks-ui");  // if health check ui path is not specified go to it

            app.UseRewriter(options);
        }
    }
};