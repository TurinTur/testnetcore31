using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test3._1
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		private readonly IWebHostEnvironment _env;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)  // Método 2 de obtener entorno
		{
			Configuration = configuration;
			_env = env;
		}
		

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			if (_env.IsDevelopment())				// Uso de Método 2 
			{
				// Development environment code
			}
			else if (_env.IsStaging())
			{
				// Staging environment code
			}
			else
			{
				// Code for all other environments
			}


			services.AddControllersWithViews();
		}

		

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  // Método 1 de obtener entorno
		{
			if (env.IsDevelopment())											// Uso de método 1
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			
			if (_env.IsDevelopment())               // Uso de Método 2 
			{
				// Development environment code
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}

	/*
	// Startup class to use in the Development environment
	public class StartupDevelopment				// Método 3 para uso de entornos
	{
		public void ConfigureServices(IServiceCollection services)
		{
		}

		public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
		}
	}

	// Startup class to use in the Production environment
	public class StartupProduction
	{
		public void ConfigureServices(IServiceCollection services)
		{
		}

		public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
		}
	}*/

}
