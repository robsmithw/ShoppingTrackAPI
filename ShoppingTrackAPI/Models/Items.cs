using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public partial class Items
    {
        [JsonPropertyName("itemId")]
        public int ItemId { get; set; }
        [JsonPropertyName("user_Id")]
        public int User_Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("previous_Price")]
        public decimal? Previous_Price { get; set; }
        [JsonPropertyName("last_Store_Id")]
        public int? Last_Store_Id { get; set; }
        [JsonPropertyName("currentStoreId")]
        public int CurrentStoreId { get; set; }
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }
        [JsonPropertyName("purchased")]
        public bool Purchased { get; set; }
    }
}
