using ChatApp.API.Hubs;
using ChatApp.Application.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class GroupController : ControllerBase
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly IConnectionManager _connectionManager;

		public GroupController(
			IGroupRepository groupRepository,
			IHubContext<ChatHub> hubContext,
			IConnectionManager connectionManager)
		{
			_groupRepository = groupRepository;
			_hubContext = hubContext;
			_connectionManager = connectionManager;
		}

		[HttpGet("{groupId}")]
		public async Task<IActionResult> GetGroup(int groupId)
		{
			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				return NotFound("Group not found.");

			return Ok(group);
		}


		[HttpGet("my")]
		public async Task<IActionResult> My()
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var groups = await _groupRepository.GetGroupsAsync(userId);

			return Ok(groups);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateGroupDto createGroupDto)
		{
			var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var group = new ChatGroup
			{
				Name = createGroupDto.Name,
				OwnerId = ownerId,
			};

			await _groupRepository.CreateAsync(group);

			await _groupRepository.AddMemberAsync(new GroupMember
			{
				UserId = ownerId,
				GroupId = group.Id
			});
			return Ok(group);
		}

		[HttpPost("{groupId}/members")]
		public async Task<IActionResult> AddMember([FromRoute] int groupId, [FromBody] AddMemberDto addMemberDto)
		{
			await _groupRepository.AddMemberAsync(new GroupMember
			{
				UserId = addMemberDto.UserId,
				GroupId = groupId
			});

			var connectionId = _connectionManager.GetConnectionId(addMemberDto.UserId);

			if (connectionId != null)
			{
				await _hubContext.Groups.AddToGroupAsync(
					connectionId,
					groupId.ToString());
			}

			return Ok("Member added successfully.");
		}

		[HttpGet("{groupId}/members")]
		public async Task<IActionResult> GetGroupMembers(int groupId)
		{
			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				return NotFound("Group not found.");

			var members = group.Members
				.Select(m => new GroupMemberDto
				{
					Id = m.User.Id,
					UserName = m.User.UserName
				})
				.ToList();

			return Ok(members);
		}

		[HttpDelete("{groupId}/members/{memberId}")]
		public async Task<IActionResult> RemoveMember(int groupId, int memberId)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				return NotFound("Group not found.");

			if (group.OwnerId != userId)
				return Forbid();

			if (memberId == group.OwnerId)
				return BadRequest("The owner cannot be removed from the group.");

			await _groupRepository.RemoveMemberAsync(groupId, memberId);

			var connectionId = _connectionManager.GetConnectionId(memberId);
			if (connectionId != null)
			{
				await _hubContext.Groups.RemoveFromGroupAsync(
					connectionId,
					groupId.ToString());
			}

			return Ok("Member removed successfully.");
		}


		[HttpDelete("{groupId}")]
		public async Task<IActionResult> Delete(int groupId)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var group = await _groupRepository.GetGroupAsync(groupId);

			if (group == null)
				return NotFound("Group not found.");

			if (group.OwnerId != userId)
				return Forbid();

			await _groupRepository.DeleteAsync(groupId);

			return NoContent();
		}
	}

}
