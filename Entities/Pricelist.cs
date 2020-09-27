using System;
using System.Collections;
using System.Collections.Generic;

namespace CoffeeBreak.Entities
{
    public class Pricelist
    {
        public Guid Id { get; set; }

        public int NumberOfWeek { get; set; }

        public string StartAndEndWeek { get; set; }

        public DateTime Created { get; set; }

        public ICollection<CoffeeProduct> CoffeeProducts { get; set; }
    }
}
