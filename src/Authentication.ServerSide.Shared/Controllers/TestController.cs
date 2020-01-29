using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Authentication.ServerSide.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(new List<string>
			{
				"Value 1",
				"Value 2"
			});
		}

		[Authorize]
		[HttpGet("with-auth")]
		public IActionResult GetAuthorized()
		{
			return Get();
		}

		[Authorize]
		[HttpGet("user")]
		public IActionResult GetUser()
		{
			var identity = HttpContext.User?.Identity;
			if (identity == null) return Unauthorized();

			var user = new
			{
				identity.IsAuthenticated,
				identity.AuthenticationType,
				identity.Name,
				Claims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value } )
			};
			return new JsonResult(user, new JsonSerializerOptions { WriteIndented = true });
		}
	}
}
