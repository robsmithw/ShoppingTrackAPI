using System;

namespace ShoppingTrackAPI.Models
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
