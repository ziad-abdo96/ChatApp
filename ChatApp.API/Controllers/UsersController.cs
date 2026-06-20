using ChatApp.Application.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		[HttpGet("me")]
		[Authorize]
		public IActionResult Me()
		{
			return Ok("You are authenticated");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _userRepository.GetAllAsync();
			
			var result = users.Select(u => new UserDTO
			{
				Id = u.Id,
				UserName = u.UserName,
			}).ToList();

			return Ok(result);
		}
	}
}
