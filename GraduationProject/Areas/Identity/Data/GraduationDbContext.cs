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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<GraduationProject.Models.UpdateProfileModel>? UpdateProfileModel { get; set; }
}
