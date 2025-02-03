using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Domain.Entities;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicatioinUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ApplicatioinUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedRoles(builder);
        builder.Entity<Blog>()
            .HasOne(b => b.User)
            .WithMany(u => u.Blogs)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData
           (
           new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
           new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
           new IdentityRole() { Name = "HR", ConcurrencyStamp = "3", NormalizedName = "HR" }
           );

    }
}
