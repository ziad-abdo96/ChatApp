using ChatApp.Domain.Interfaces;

namespace ChatApp.Infrastructure.Repositories
{
	public class ConnectionManager : IConnectionManager
	{
		private readonly Dictionary<int, string> _connections = new();

		public void AddConnection(int userId, string connectionId)
		{
			_connections[userId] = connectionId;
			
		}

		public void RemoveConnection(int userId)
		{
			_connections.Remove(userId);
		}

		public string? GetConnectionId(int userId)
		{
			_connections.TryGetValue(userId, out var connectionId);

			return connectionId;
		}
	}
}
