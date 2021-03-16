using Application.Services;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BloggerContext: IdentityDbContext<ApplicationUser>
    {
        private readonly UserResolverService _userService;
        public BloggerContext(DbContextOptions<BloggerContext> options, UserResolverService userService):base(options)
        {
            _userService = userService;
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Author> Authors { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach(var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy = _userService.GetUser();

                if (entityEntry.State==EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = _userService.GetUser();
                }
            }

            return await base.SaveChangesAsync();
        }

        public string GetUser()
        {
            return _userService.GetUser();
        }
    }
}
