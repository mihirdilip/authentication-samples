using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.ServerSide
{
	public static class Extensions
	{
		public static IServiceCollection AddSharedServices(this IServiceCollection services)
		{
			services.AddControllers(options =>
			{
				// ALWAYS USE HTTPS (SSL) protocol in production when using Basic authentication.
				//options.Filters.Add<RequireHttpsAttribute>();

				// All the requests will need to be authorized. 
				// Alternatively, add [Authorize] attribute to Controller or Action Method where necessary.
				options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
			});
			
			return services;
		}

		public static IApplicationBuilder UseSharedPipeline(this IApplicationBuilder app)
		{
			//var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
		}
	}
}
