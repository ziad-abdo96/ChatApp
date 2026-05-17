using ChatApp.Application.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController: ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public AuthController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			var user = await _userRepository.GetByEmailAsync(loginDTO.Email);
		
			if(user == null)
				return Unauthorized(); 

			if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
				return Unauthorized();
			return Ok("Login Success");
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			var existingUser = await _userRepository.GetByEmailAsync(registerDTO.Email);

			if (existingUser != null)
				return BadRequest("Email already exists");

			var newUser = new User
			{
				UserName = registerDTO.UserName,
				Email = registerDTO.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
				CreatedAt = DateTime.UtcNow
			};
			await _userRepository.AddAsync(newUser);

			return Ok("Registration Success");
		}
	}
}
