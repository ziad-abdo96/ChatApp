using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpGet("me")]
		[Authorize]
		public IActionResult Me()
		{
			return Ok("You are authenticated");
		}
	}
}
