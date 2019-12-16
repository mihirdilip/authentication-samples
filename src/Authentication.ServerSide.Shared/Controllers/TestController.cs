using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
	}
}
