using Meteors.AspNetCore.Infrastructure.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteLinqApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.Infrastructure.Databases.InMemory
{
    public class NoteLinqAppInMemoryDbContext : MrIdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public NoteLinqAppInMemoryDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //by call this I added indexing of DateCreating for performance on order by.
            // query filter on DateCreated
            base.OnModelCreating(builder);

            //change the shcma and Name for Identity Tables -- Default came as dbo.AspNet...  which is is dont like it
            builder.Entity<Account>().ToTable("Users", "app");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles", "app");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "app");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "app");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "app");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "app");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "app");

        }

        public DbSet<Classification> Classification { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
