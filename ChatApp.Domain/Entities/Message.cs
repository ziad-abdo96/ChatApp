namespace ChatApp.Domain.Entities;

public class Message
{
	public int Id { get; set; }

	public string Content { get; set; } = string.Empty;

	public int SenderId { get; set; }
	public string SenderName { get; set; } = string.Empty;

	public DateTime SentAt { get; set; } = DateTime.UtcNow;
}