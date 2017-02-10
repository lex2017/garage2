using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public DateTime ParkTime { get; set; }
        public DateTime ParkAt { get; set; }
        public TimeSpan ParkOut { get; set; }
        public int Cost { get; set; }
    }
}