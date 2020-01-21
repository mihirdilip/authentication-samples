using System.Collections.Generic;

namespace Authentication.ServerSide
{
	public interface IUserCache
	{
		List<User> GetUsers();
	}

	class UserCache : IUserCache
	{
		public List<User> GetUsers() => new List<User>
		{
			new User { Username = "Admin", Password = "Admin" },
			new User { Username = "User", Password = "User" }
		};
	}
}
