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

            var user = new ApplicationUser
            {
                UserName = "User",
                LastName = "LastName",
                FirstName = "FirstName"
            };

            var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }

            await using var context = scopeServiceProvider.GetService<ApplicationDbContext>();
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}