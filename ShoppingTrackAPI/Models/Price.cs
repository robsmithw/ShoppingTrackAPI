using System;
using System.Collections.Generic;

namespace ShoppingTrackAPI.Models
{
    public class Price : Entity<Guid>
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public decimal CurrentPrice { get; set; }
        public Guid StoreId { get; set; }
        public DateTime DateOfPrice { get; set; }
    }
}
