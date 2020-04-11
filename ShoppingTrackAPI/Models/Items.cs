using System;
using System.Collections.Generic;

namespace ShoppingTrackAPI.Models
{
    public partial class Items
    {
        public int ItemId { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; }
        public decimal? Previous_Price { get; set; }
        public int? Last_Store_Id { get; set; }
        public bool Deleted { get; set; }
        public bool Purchased { get; set; }
    }
}
