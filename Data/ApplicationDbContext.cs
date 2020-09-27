using CoffeeBreak.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeBreak.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Pricelist> Pricelists { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildPriceLists(builder);
        }

        private void BuildPriceLists(ModelBuilder builder)
        {
            builder.Entity<Pricelist>(action =>
            {
                action.Property(prop => prop.NumberOfWeek)
                        .IsRequired();

                action.Property(prop => prop.Created)
                        .IsRequired();

                action.Property(prop => prop.StartAndEndWeek)
                        .IsRequired();
            });
        }

    }
}
