using System;
using System.Collections.Generic;

namespace ShoppingTrackAPI.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
    }
}
