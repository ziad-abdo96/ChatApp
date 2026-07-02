namespace ChatApp.Domain.Entities;

public class Message
{
	public int Id { get; set; }

	public string Content { get; set; } = string.Empty;

	public int SenderId { get; set; }

	public User Sender { get; set; } = null!;

	public int? ReceiverId { get; set; }

	public User? Receiver { get; set; }

	public int? GroupId { get; set; }

	public ChatGroup? Group { get; set; }

	public DateTime SentAt { get; set; } = DateTime.UtcNow;
}