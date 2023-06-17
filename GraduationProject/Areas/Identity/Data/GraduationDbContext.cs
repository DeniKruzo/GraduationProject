using GraduationProject.Areas.Identity.Data;
using GraduationProject.Data.Domains;
using GraduationProject.Domains;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data;

public class GraduationDbContext : IdentityDbContext<ApplicationUser>
{
    public GraduationDbContext(DbContextOptions<GraduationDbContext> options)
        : base(options)
    {

    }

    public DbSet<openOrder> Orders { get; set; }
    public DbSet<CategoryOrder> CategoryOrder { get; set; }
    public DbSet<Specialization> Specialization { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Chat> Chats { get; set; }

    public DbSet<ChatUser> ChatUsers { get; set; }
   




    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ChatUser>()
            .HasKey(x => new { x.ChatId, x.UserId });
    }

    public DbSet<GraduationProject.Models.UpdateProfileModel>? UpdateProfileModel { get; set; }
}
