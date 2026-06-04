using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
namespace ChatApp.API.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private static readonly HashSet<string> _onlineUsers = new();
		private readonly IMessageRepository _messageRepository;

		public ChatHub(IMessageRepository messageRepository)
		{
			this._messageRepository = messageRepository;
		}
		public override async Task OnConnectedAsync()
		{
			var userName = Context.User?.Identity?.Name;
			if (!string.IsNullOrEmpty(userName))
			{
				_onlineUsers.Add(userName);
				await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers);
			}
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var userName = Context.User?.Identity?.Name;
			if (!string.IsNullOrEmpty(userName))
			{
				_onlineUsers.Remove(userName);
				await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers);
			}
			await base.OnDisconnectedAsync(exception);
		}


		public async Task SendMessage(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
				return;
			var userName = Context.User?.Identity?.Name;
			var userId = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
			var newMessage = new Message
			{
				Content = message,
				SenderName = userName!,
				SenderId = userId,
				SentAt = DateTime.UtcNow,
			};

			await _messageRepository.AddAsync(newMessage);

			await Clients.All.SendAsync(
				"ReceiveMessage",
				userName,
				message
				);
		}

	}
}
