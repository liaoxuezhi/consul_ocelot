using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsulCore;
using ConsulCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API001
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RegisterConsul(appLifeTime, new HealthService()
            {
                IP = Configuration["Service:IP"],
                Port = Convert.ToInt32(Configuration["Service:Port"]),
                Name = Configuration["Service:Name"],
            }, new ConsulService()
            {
                IP = Configuration["Consul:IP"],
                Port = Convert.ToInt32(Configuration["Consul:Port"])
            });
            app.Map("/health", builder =>
            {
                builder.Run(async context => await context.Response.WriteAsync("health"));
            });
            app.UseMvc();
        }
    }
}
