using Authentication.ServerSide.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.ServerSide
{
	public static class Extensions
	{
		public static IServiceCollection AddSharedServices(this IServiceCollection services)
		{
			services.AddControllers();

			services.AddSingleton<IUserCache, UserCache>();
			return services;
		}
	}
}
