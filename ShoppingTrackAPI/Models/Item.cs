using System;
using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public partial class Item : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal? PreviousPrice { get; set; }
        public Guid LastStoreId { get; set; }
        public Guid CurrentStoreId { get; set; }
        public bool Purchased { get; set; }
    }
}
