using CoffeeBreak.Data;
using CoffeeBreak.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBreak.Controllers
{
    [Authorize(Policy = "admin")]
    public class PricelistController : Controller
    {
        private readonly ApplicationDbContext context;

        public PricelistController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Parse(IFormFile file)
        {
            if (file != null)
            {
                var today = DateTime.UtcNow;
                

                var pricelist = new Pricelist
                {
                    Created = today,
                    NumberOfWeek = GetNumberOfWeek(today),
                    StartAndEndWeek = GetStartAndEndWeek(today),
                };

                await context.Pricelists.AddAsync(pricelist);
                await context.SaveChangesAsync();

                var currentPricelist = await context.Pricelists.FirstOrDefaultAsync(x => x.NumberOfWeek == pricelist.NumberOfWeek);
                var result = new StringBuilder();
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
            }
            return View();
        }

       
        /// <summary>
        /// Calc start day and end day of current week
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        private string GetStartAndEndWeek(DateTime today)
        {
            // TODO correct calc start and end of week (maybe use our timezone and our calendar)
            // remove time in date
            int days = today.DayOfWeek - DayOfWeek.Monday;
            var start = today.AddDays(-days);
            var end = start.AddDays(6);

            return $"Цены действительны с {start} по {end}";
        }

        /// <summary>
        /// Calc number of week 
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        private int GetNumberOfWeek(DateTime today)
        {
            // TODO correct calc number of week
            var calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }
    }
}
