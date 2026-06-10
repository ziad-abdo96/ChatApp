using ChatApp.Application.DTOs;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
				SenderName = m.SenderName,
				Content = m.Content,
				SentAt = m.SentAt
			}).ToList();
			return Ok(result);
		}
	}
}
