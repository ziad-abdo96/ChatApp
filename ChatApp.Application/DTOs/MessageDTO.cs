namespace ChatApp.Application.DTOs
{
	public class MessageDTO
	{
		public string SenderName { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime SentAt { get; set; }
	}
}
