using GraduationProject.Areas.Identity.Data;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
