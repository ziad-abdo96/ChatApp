using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Data
{
	public class AppDbContext:DbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Message> Messages { get; set; }
	}
}
