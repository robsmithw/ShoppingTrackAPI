using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public class Stores
    {
        [JsonPropertyName("storeId")]
        public int StoreId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
