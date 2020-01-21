using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication.ServerSide
{
	enum ApiKeyIn
	{
		Header,
		QueryParams,
		HeaderOrQueryParams
	}

	public class Startup
	{
		private const ApiKeyIn ApiKeyIn = ServerSide.ApiKeyIn.HeaderOrQueryParams;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSharedServices();

			services.AddSingleton<IApiKeyCache, ApiKeyCache>();

			var authBuilder = services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme);

			var apiKeyOptions = new Action<ApiKeyOptions>(options =>
			{
				options.Realm = "Authentication.ServerSide.ApiKey";
				options.KeyName = "X-API-Key";
			});

			switch (ApiKeyIn)
			{
				case ApiKeyIn.Header:
					authBuilder.AddApiKeyInHeader<ApiKeyProvider>(apiKeyOptions);
					break;

				
				case ApiKeyIn.QueryParams:
					authBuilder.AddApiKeyInQueryParams<ApiKeyProvider>(apiKeyOptions);
					break;
				
				case ApiKeyIn.HeaderOrQueryParams:
				default:
					authBuilder.AddApiKeyInHeaderOrQueryParams<ApiKeyProvider>(apiKeyOptions);
					break;
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			app.UseSharedPipeline();
		}
	}
}
