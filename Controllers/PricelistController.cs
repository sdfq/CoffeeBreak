using CoffeeBreak.Data;
using CoffeeBreak.Entities;
using CoffeeBreak.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                var pricelist = new Pricelist
                {
                    Created = DateTimeHelper.Today(),
                    NumberOfWeek = DateTimeHelper.GetNumberOfWeek(),
                    StartAndEndWeek = DateTimeHelper.GetStartAndEndOfWeek(),
                };

                await context.Pricelists.AddAsync(pricelist);
                await context.SaveChangesAsync();

                var currentPricelist = await context.Pricelists.FirstOrDefaultAsync(x => x.NumberOfWeek == pricelist.NumberOfWeek);
                var coffeeProducts = new List<CoffeeProduct>();
                using (TextFieldParser parser = new TextFieldParser(file.OpenReadStream()))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        var coffee = new CoffeeProduct
                        {
                            Name = fields[0],
                            
                            PriceBigPacket = decimal.Parse(fields[1], formatter),
                            PriceBigPacketDiscount10 = decimal.Multiply(decimal.Parse(fields[1], formatter), Convert.ToDecimal(0.9)),
                            PriceBigPacketDiscount20 = decimal.Multiply(decimal.Parse(fields[1], formatter), Convert.ToDecimal(0.8)),
                            PriceBigPacketDiscount30 = decimal.Multiply(decimal.Parse(fields[1], formatter), Convert.ToDecimal(0.7)),

                            PriceSmallPacket = decimal.Parse(fields[2], formatter),
                            PriceSmallPacketDiscount10 = decimal.Multiply(decimal.Parse(fields[2], formatter), Convert.ToDecimal(0.9)),
                            PriceSmallPacketDiscount20 = decimal.Multiply(decimal.Parse(fields[2], formatter), Convert.ToDecimal(0.8)),
                            PriceSmallPacketDiscount30 = decimal.Multiply(decimal.Parse(fields[2], formatter), Convert.ToDecimal(0.7)),
                            
                            PricelistId = currentPricelist.Id
                        };

                        coffeeProducts.Add(coffee);
                    }
                }

                await context.AddRangeAsync(coffeeProducts);
                await context.SaveChangesAsync();
            }
            return View();
        }
    }
}
