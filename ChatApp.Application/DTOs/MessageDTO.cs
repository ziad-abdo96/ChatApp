namespace ChatApp.Application.DTOs
{
	public class MessageDTO
	{
		public int SenderId { get; set; }

		public string SenderName { get; set; } = string.Empty;

		public string Content { get; set; } = string.Empty;

		public DateTime SentAt { get; set; }
	}
}