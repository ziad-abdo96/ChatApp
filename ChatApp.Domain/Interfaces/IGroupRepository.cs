using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
	public interface IGroupRepository
	{
		Task<ChatGroup> CreateAsync(ChatGroup chatGroup);
		Task AddMemberAsync(GroupMember groupMember);
		Task<List<ChatGroup>> GetGroupsAsync(int userId);
		Task<ChatGroup?> GetGroupAsync(int groupId);
		Task RemoveMemberAsync(int  groupId, int userId);
		Task DeleteAsync(int groupId);
	}
}
