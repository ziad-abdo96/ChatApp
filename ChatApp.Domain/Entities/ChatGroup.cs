namespace ChatApp.Domain.Entities
{
	public class ChatGroup
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int OwnerId { get; set; }
		public User Owner { get; set; } = null!;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
		public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
	}
}
