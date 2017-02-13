using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class StatisticsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Fordonsantal")]
        public int VehicleCount { get; set; }
        [DisplayName("Fordonstyp")]
        public VehicleType Type { get; set; }
        [DisplayName("Hjulantal")]
        public int WheelsTotal { get; set; }
        [DisplayName("Nuvarande intäkt")]
        public int CostTotal { get; set; }
    }

}