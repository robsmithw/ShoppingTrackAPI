using System;
using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public class Store : Entity<Guid>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
