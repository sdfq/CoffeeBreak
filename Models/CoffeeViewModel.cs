using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Models
{
    public enum BagSize
    {
        [Display(Name = "250г")]
        SmallPacket,
        [Display(Name = "1кг")]
        BigPacket,
    }

    public enum Grind
    {
        [Display(Name = "Мелкий помол")]
        FineGround = 1,
        [Display(Name = "Грубый помол")]
        CoarseGround,
        [Display(Name = "Зерно")]
        WholeBeans,
    }

    public class CoffeeViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public decimal PriceBigPacket { get; set; }
        
        public decimal PriceSmallPacket { get; set; }

        public Grind Ground { get; set; }

        public BagSize Packet { get; set; }

        public int Count { get; set; }
    }
}
