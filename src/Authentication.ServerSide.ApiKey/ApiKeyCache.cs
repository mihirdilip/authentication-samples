using AspNetCore.Authentication.ApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.ServerSide
{
	interface IApiKeyCache
	{
		Task<IApiKey> GetApiKeyAsync(string key);
	}

	class ApiKeyCache : IApiKeyCache
	{
		private List<IApiKey> _cache = new List<IApiKey>
		{
			new ApiKey("Key1", "Admin"),
			new ApiKey("Key2", "User"),
		};

		public Task<IApiKey> GetApiKeyAsync(string key)
		{
			var apiKey = _cache.FirstOrDefault(k => k.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
			return Task.FromResult(apiKey);
		}
	}
}
