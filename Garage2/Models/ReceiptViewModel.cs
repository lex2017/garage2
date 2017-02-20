using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        [DisplayName("Reg. nummer")]
        public string RegNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh} timmar {0:mm} minuter")]
        [DisplayName("Parkeringsperiod")]
        public TimeSpan ParkTime { get; set; }
        [DisplayName("Påbörjad")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}")]
        public DateTime ParkAt { get; set; }
        [DisplayName("Avslutad")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}")]
        public DateTime ParkOut { get; set; }
        [DisplayName("Kostnad")]
        public int Cost { get; set; }
    }
}