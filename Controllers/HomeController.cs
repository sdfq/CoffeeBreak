using CoffeeBreak.Data;
using CoffeeBreak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBreak.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// If user is authenticated display priceList for coffee and tea 
        /// If user not authenicated redirect to account/login
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            var pricelist = await context.Pricelists.FirstOrDefaultAsync(p => p.NumberOfWeek == 39);
            var coffeeProducts = await context.CoffeeProducts.Where(p => p.PricelistId == pricelist.Id).ToListAsync();

            var models = new List<CoffeeViewModel>();
            foreach(var coffee in coffeeProducts)
            {
                var model = new CoffeeViewModel
                {
                    Id = coffee.Id,
                    Name = coffee.Name,
                    PriceBigPacket = coffee.PriceBigPacket,
                    PriceSmallPacket = coffee.PriceSmallPacket,
                };

                models.Add(model);
            }

            return View(models);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
