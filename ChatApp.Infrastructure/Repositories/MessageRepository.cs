using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using ChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly AppDbContext _context;

		public MessageRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Message message)
		{
			await _context.Messages.AddAsync(message);
			await _context.SaveChangesAsync();
		}

		public Task<List<Message>> GetAllAsync()
		{
			return _context.Messages.OrderBy(m => m.SentAt).ToListAsync();
		}
	}
}
