using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatlerWaldorfCorp.LocationReporter.Service.Events;
using StatlerWaldorfCorp.LocationReporter.Service.Models;
using StatlerWaldorfCorp.LocationReporter.Service.Service;

namespace StatlerWaldorfCorp.LocationReporter.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));
            services.Configure<TeamServiceOptions>(Configuration.GetSection("teamservice"));

            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter), typeof(CommandEventConverter));
            services.AddSingleton(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
