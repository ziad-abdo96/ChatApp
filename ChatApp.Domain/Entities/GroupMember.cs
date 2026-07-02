namespace ChatApp.Domain.Entities
{
	public class GroupMember
	{
		public int GroupId { get; set; }
		public ChatGroup ChatGroup { get; set; } = null!;
		public int UserId { get; set; }
		public User User { get; set; } = null!;
	}
}
