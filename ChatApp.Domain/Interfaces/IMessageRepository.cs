using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
	public interface IMessageRepository
	{
		Task AddAsync(Message message);
		Task<List<Message>> GetAllAsync();
		Task<List<Message>> GetConversationAsync(int userId, int otherUserId);
		Task<List<Message>> GetGroupMessagesAsync(int groupId);
	}
}
