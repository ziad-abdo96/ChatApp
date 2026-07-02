namespace ChatApp.Domain.Interfaces
{
	public interface IConnectionManager
	{
		void AddConnection(int userId, string connectionId);

		void RemoveConnection(int userId);

		string? GetConnectionId(int userId);
	}
}
