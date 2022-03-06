using System;
using System.Collections.Generic;

namespace ShoppingTrackAPI.Models
{
    public partial class ErrorLog : Entity<Guid>
    {
        public string Location { get; set; }
        public string CallStack { get; set; }
    }
}
