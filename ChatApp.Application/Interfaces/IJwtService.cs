using ChatApp.Domain.Entities;

namespace ChatApp.Application.Interfaces
{
	public interface IJwtService
	{
		string GenerateToken(User user);
	}
}
