using System.ComponentModel.DataAnnotations;

namespace ChatApp.Application.DTOs;

public class RegisterDTO
{
	[Required]
	public string UserName { get; set; } = string.Empty;
	
	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MinLength(6)] 
	public string Password { get; set; } = string.Empty;
}