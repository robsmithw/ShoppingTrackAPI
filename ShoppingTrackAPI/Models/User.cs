using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingTrackAPI.Models
{
    public class User
    {
        public string Username { get; set; }
        public int User_Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool Validated { get; set; }
    }
}
