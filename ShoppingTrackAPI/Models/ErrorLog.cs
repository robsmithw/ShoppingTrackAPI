using System;
using System.Collections.Generic;

namespace ShoppingTrackAPI.Models
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string CallStack { get; set; }
    }
}
