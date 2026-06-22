using ChatApp.Application.DTOs;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MessageController : ControllerBase
	{
		private readonly IMessageRepository _messageRepository;

		public MessageController(IMessageRepository messageRepository)
		{
			this._messageRepository = messageRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var messages = await _messageRepository.GetAllAsync();

			var result = messages.Select(m => new MessageDTO
			{
				SenderId = m.SenderId,
				SenderName = m.SenderName,
				Content = m.Content,
				SentAt = m.SentAt
			}).ToList();
			return Ok(result);
		}


		[HttpGet("conversation/{otherUserId}")]
		public async Task<IActionResult> GetConversatioin(int otherUserId)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var messages = await _messageRepository.GetConversationAsync(userId, otherUserId);

			var result = messages.Select(m => new MessageDTO
			{
				SenderId = m.SenderId,
				SenderName = m.SenderName,
				Content = m.Content,
				SentAt = m.SentAt
			}).ToList();

			return Ok(result);
		}
	}
}
