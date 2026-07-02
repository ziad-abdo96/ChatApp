namespace ChatApp.Domain.Entities;

public class User
{
	public int Id { get; set; }

	public string UserName { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string PasswordHash { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
	public ICollection<ChatGroup> OwnedGroups { get; set; } = new List<ChatGroup>();
}