using System;

namespace CoffeeBreak.Entities
{
    public class CoffeeProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal PriceBigPacket { get; set; }

        public decimal PriceSmallPacket { get; set; }

        public decimal PriceBigPacketDiscount10 { get; set; }

        public decimal PriceSmallPacketDiscount10 { get; set; }

        public decimal PriceBigPacketDiscount20 { get; set; }

        public decimal PriceSmallPacketDiscount20 { get; set; }

        public decimal PriceBigPacketDiscount30 { get; set; }

        public decimal PriceSmallPacketDiscount30 { get; set; }

        public virtual Guid PricelistId { get; set; }
    }
}
