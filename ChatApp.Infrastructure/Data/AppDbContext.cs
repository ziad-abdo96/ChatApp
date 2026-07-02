using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<User> Users { get; set; }

	public DbSet<Message> Messages { get; set; }

	public DbSet<ChatGroup> ChatGroups { get; set; }

	public DbSet<GroupMember> GroupMembers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

		modelBuilder.Entity<GroupMember>()
			.HasKey(x => new { x.GroupId, x.UserId });

		modelBuilder.Entity<GroupMember>()
			.HasOne(gm => gm.ChatGroup)
			.WithMany(g => g.Members)
			.HasForeignKey(gm => gm.GroupId);

		modelBuilder.Entity<GroupMember>()
			.HasOne(gm => gm.User)
			.WithMany(u => u.GroupMembers)
			.HasForeignKey(gm => gm.UserId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<ChatGroup>()
			.HasOne(g => g.Owner)
			.WithMany(o => o.OwnedGroups)
			.HasForeignKey(g => g.OwnerId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}