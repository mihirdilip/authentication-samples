using AspNetCore.Authentication.ApiKey;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Authentication.ServerSide
{
	class ApiKeyProvider : IApiKeyProvider
	{
		private readonly ILogger<IApiKeyProvider> _logger;
		private readonly IApiKeyCache _apiKeyCache;

		public ApiKeyProvider(ILogger<IApiKeyProvider> logger, IApiKeyCache apiKeyCache)
		{
			_logger = logger;
			_apiKeyCache = apiKeyCache;
		}

		public Task<IApiKey> ProvideAsync(string key)
		{
			try
			{
				return _apiKeyCache.GetApiKeyAsync(key);
			}
			catch (System.Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				throw;
			}
			
		}
	}
}