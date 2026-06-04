using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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
			return Ok(messages);
		}
	}
}
