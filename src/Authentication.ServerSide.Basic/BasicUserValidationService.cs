using Authentication.ServerSide.Services;
using Mihir.AspNetCore.Authentication.Basic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.ServerSide
{
	public class BasicUserValidationService : IBasicUserValidationService
	{
		private readonly IUserCache _userCache;

		public BasicUserValidationService(IUserCache userCache)
		{
			_userCache = userCache;
		}

		public Task<bool> IsValidAsync(string username, string password)
		{
			var isValid = _userCache.GetUsers().Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password, StringComparison.Ordinal));
			return Task.FromResult(isValid);
		}
	}
}