using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ImdbDAL;
using Microsoft.Extensions.Configuration;

namespace WebApplication1
{
    public class Startup
    {
		private readonly IConfigurationRoot _configuration;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(env.ContentRootPath);
			builder.AddJsonFile("configuration.json");
			_configuration = builder.Build();
		}


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddOptions();
			services.Configure<ImdbSettings>(_configuration.GetSection("Imdb"));

			services.AddTransient<ImdbContext>();

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
