using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
	public interface IMessageRepository
	{
		Task AddAsync(Message message);
		Task<List<Message>> GetAllAsync();
	}
}
