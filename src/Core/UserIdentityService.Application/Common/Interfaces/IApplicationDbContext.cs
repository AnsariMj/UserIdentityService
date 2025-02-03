using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UserIdentityService.Domain.Entities;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Blog> Blogs { get; set; }
    DbSet<ApplicatioinUser> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
