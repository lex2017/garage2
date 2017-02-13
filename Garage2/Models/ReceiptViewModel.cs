using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        [DisplayName("Parkeringsperiod")]
        public TimeSpan ParkTime { get; set; }
        [DisplayName("Påbörjad")]
        public DateTime ParkAt { get; set; }
        [DisplayName("Avslutad")]
        public DateTime ParkOut { get; set; }
        [DisplayName("Kostnad")]
        public int Cost { get; set; }
    }
}