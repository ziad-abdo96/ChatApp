
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
		private readonly IGroupRepository _groupRepository;
		private readonly IConnectionManager _connectionManager;

		public ChatHub(
			IMessageRepository messageRepository,
			IGroupRepository groupRepository,
			IConnectionManager connectionManager)
		{
			_messageRepository = messageRepository;
			_groupRepository = groupRepository;
			_connectionManager = connectionManager;
		}

		public override async Task OnConnectedAsync()
		{
			var userName = Context.User?.Identity?.Name;

			var userId = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

			if (!string.IsNullOrEmpty(userName))
			{
				_onlineUsers.Add(userName);
				await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers);
			}


			_connectionManager.AddConnection(
				userId,
				Context.ConnectionId);


			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var userName = Context.User?.Identity?.Name;
			var userId = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);



			if (!string.IsNullOrEmpty(userName))
			{
				_onlineUsers.Remove(userName);
				await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers);
			}

			_connectionManager.RemoveConnection(userId);

			await base.OnDisconnectedAsync(exception);
		}


		public async Task SendMessage(int receiverId, string message)
		{
			if (string.IsNullOrWhiteSpace(message))
				return;
			var userName = Context.User?.Identity?.Name;
			var userId = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

			//	Console.WriteLine($"ReceiverId: {receiverId}");
			//	Console.WriteLine($"Connections: {string.Join(",", _connectionUserMap.Keys)}");

			var newMessage = new Message
			{
				Content = message,
				SenderId = userId,
				ReceiverId = receiverId,
				SentAt = DateTime.UtcNow,
			};

			await _messageRepository.AddAsync(newMessage);

			var connectionId = _connectionManager.GetConnectionId(receiverId);

			if (connectionId != null)
			{
				await Clients.Client(connectionId)
					.SendAsync("ReceiveMessage", userName, message);
			}

		}

		public async Task JoinGroup(int groupId)
		{
			var userId = int.Parse(Context.User!
				.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				throw new Exception("Group not found.");

			if (!group.Members.Any(x => x.UserId == userId))
				throw new Exception("You are not a member of this group.");

			await Groups.AddToGroupAsync(
				Context.ConnectionId,
				groupId.ToString());
		}

		public async Task SendGroupMessage(int groupId, string message)
		{
			if (string.IsNullOrWhiteSpace(message))
				return;

			var userId = int.Parse(Context.User!
				.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var userName = Context.User?.Identity?.Name;

			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				throw new Exception("Group not found.");

			if (!group.Members.Any(x => x.UserId == userId))
				throw new Exception("You are not a member of this group.");

			var messageEntity = new Message
			{
				Content = message,
				SenderId = userId,
				GroupId = groupId,
				SentAt = DateTime.UtcNow
			};

			await _messageRepository.AddAsync(messageEntity);

			await Clients.Group(groupId.ToString())
				.SendAsync(
					"ReceiveGroupMessage",
					userId,
					userName,
					message,
					groupId);
		}

		public Task LeaveGroup(int groupId)
		{
			return Groups.RemoveFromGroupAsync(
				Context.ConnectionId,
				groupId.ToString());
		}

	}

}

