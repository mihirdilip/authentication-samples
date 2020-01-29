using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.ServerSide
{
	public static class Extensions
	{
		public static IServiceCollection AddSharedServices(this IServiceCollection services)
		{
			services.AddControllers();
			
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
