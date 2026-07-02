using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using ChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
	public class GroupRepository : IGroupRepository
	{
		private readonly AppDbContext _context;

		public GroupRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ChatGroup> CreateAsync(ChatGroup chatGroup)
		{
			await _context.ChatGroups.AddAsync(chatGroup);
			await _context.SaveChangesAsync();

			return chatGroup;
		}

		public async Task AddMemberAsync(GroupMember groupMember)
		{
			var groupExists = await _context.ChatGroups
				.AnyAsync(g => g.Id == groupMember.GroupId);
			
			if (!groupExists)
				throw new Exception("Group not found.");

			var userExists = await _context.Users
				.AnyAsync(u => u.Id == groupMember.UserId);

			if (!userExists)
				throw new Exception("User not found.");

			var memberExists = await _context.GroupMembers
				.AnyAsync(m => m.GroupId == groupMember.GroupId && m.UserId == groupMember.UserId);

			if (memberExists)
				throw new Exception("User is already a member of the group.");

			await _context.GroupMembers.AddAsync(groupMember);
			await _context.SaveChangesAsync();
		}

		public async Task<ChatGroup?> GetGroupAsync(int groupId)
		{
			return await _context.ChatGroups
				.Include(g => g.Members)
					.ThenInclude(m => m.User)
				.FirstOrDefaultAsync(g => g.Id == groupId);
		}

		public async Task<List<ChatGroup>> GetGroupsAsync(int userId)
		{
			return await _context.GroupMembers
				.Where(m => m.UserId == userId)
				.Select(m => m.ChatGroup)
				.Include(g => g.Members)
					.ThenInclude(m => m.User)
				.ToListAsync();
		}

		public async Task RemoveMemberAsync(int groupId, int userId)
		{
			var gm = await _context.GroupMembers.FirstOrDefaultAsync(m => m.GroupId == groupId && m.UserId == userId);
			if (gm == null)
				throw new Exception("User is not a member of the group.");

			_context.GroupMembers.Remove(gm);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int groupId)
		{
			var group = await _context.ChatGroups.FindAsync(groupId);

			if (group == null)
				throw new Exception("Group not found.");

			_context.ChatGroups.Remove(group);

			await _context.SaveChangesAsync();
		}
	}
}
