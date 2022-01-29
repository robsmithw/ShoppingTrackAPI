using System.Text.Json.Serialization;

namespace ShoppingTrackAPI.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
