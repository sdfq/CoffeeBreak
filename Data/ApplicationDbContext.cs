using CoffeeBreak.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeBreak.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private const string moneyType = "decimal";
        private const int maxCharsForName = 50;
        private const int maxCharsForDescription = 100;

        public DbSet<Pricelist> Pricelists { get; set; }

        public DbSet<CoffeeProduct> CoffeeProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildPricelists(builder);
            BuildCoffeeProduct(builder);
        }

        private void BuildCoffeeProduct(ModelBuilder builder)
        {
            builder.Entity<CoffeeProduct>(action =>
            {
                action.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(maxCharsForName);

                action.Property(x => x.PriceBigPacket)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceBigPacketDiscount10)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceBigPacketDiscount20)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceBigPacketDiscount30)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceSmallPacket)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceSmallPacketDiscount10)
                        .IsRequired()
                        .HasColumnType(moneyType);

                action.Property(x => x.PriceSmallPacketDiscount20)
                       .IsRequired()
                       .HasColumnType(moneyType);

                action.Property(x => x.PriceSmallPacketDiscount30)
                       .IsRequired()
                       .HasColumnType(moneyType);
            });
        }

        private void BuildPricelists(ModelBuilder builder)
        {
            builder.Entity<Pricelist>(action =>
            {
                action.Property(x => x.NumberOfWeek)
                        .IsRequired();

                action.Property(x => x.Created)
                        .IsRequired();

                action.Property(x => x.StartAndEndWeek)
                        .HasMaxLength(maxCharsForDescription)
                        .IsRequired();

                action.HasMany(x => x.CoffeeProducts).WithOne();
            });
        }
    }
}
