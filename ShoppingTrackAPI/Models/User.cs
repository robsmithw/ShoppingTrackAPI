using System;

namespace ShoppingTrackAPI.Models
{
    public class User : Entity<Guid>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool Validated { get; set; }
    }
}
