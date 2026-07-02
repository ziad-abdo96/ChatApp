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
			return _context.Messages
				.Include(m => m.Sender)
				.OrderBy(m => m.SentAt).ToListAsync();
		}

		public async Task<List<Message>> GetConversationAsync(int userId, int otherUserId)
		{
			return await _context.Messages
				.Include(m => m.Sender)
				.Where(m =>
					(m.SenderId == userId && m.ReceiverId == otherUserId) ||
					(m.SenderId == otherUserId && m.ReceiverId == userId))
				.OrderBy(m => m.SentAt)
				.ToListAsync();
		}

		public async Task<List<Message>> GetGroupMessagesAsync(int groupId)
		{
			return await _context.Messages
				.Include(m => m.Sender)
				.Where(m => m.GroupId == groupId)
				.OrderBy(m => m.SentAt)
				.ToListAsync();
		}
	}
}
