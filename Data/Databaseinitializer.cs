using Microsoft.Extensions.DependencyInjection;
using CoffeeBreak.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace CoffeeBreak.Data
{
    internal static class Databaseinitializer
    {
        public static async void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            var admin = new ApplicationUser
            {
                UserName = "admin",
                LastName = "Perevozchikov",
                FirstName = "Andrey"
            };

            var customer = new ApplicationUser
            {
                UserName = "user",
                LastName = "LastName",
                FirstName = "FirstName"
            };

            var result = userManager.CreateAsync(admin, "123qwe").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "admin")).GetAwaiter().GetResult();
            }

            var resultCustomer = userManager.CreateAsync(customer, "123qwe").GetAwaiter().GetResult();
            if (resultCustomer.Succeeded)
            {
                userManager.AddClaimAsync(customer, new Claim(ClaimTypes.Role, "customer")).GetAwaiter().GetResult();
            }

            await using var context = scopeServiceProvider.GetService<ApplicationDbContext>();
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}