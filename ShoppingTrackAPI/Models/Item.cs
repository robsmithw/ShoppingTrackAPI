using System;
using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public partial class Item : Entity<Guid>
    {
        [JsonPropertyName("user_Id")]
        public Guid UserId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("previous_Price")]
        public decimal? PreviousPrice { get; set; }
        [JsonPropertyName("last_Store_Id")]
        public Guid LastStoreId { get; set; }
        [JsonPropertyName("currentStoreId")]
        public Guid CurrentStoreId { get; set; }
        [JsonPropertyName("purchased")]
        public bool Purchased { get; set; }
    }
}
