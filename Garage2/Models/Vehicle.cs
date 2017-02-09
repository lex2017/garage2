using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [DisplayName("Fordonstyp")]
        public VehicleType Type { get; set; }
        [DisplayName("Reg. nummer")]
        public string RegNumber { get; set; }
        [DisplayName("Färg")]
        public string Color { get; set; }
        [DisplayName("Märke")]
        public string Manufacturer { get; set; }
        [DisplayName("Modell")]
        public string Model { get; set; }
        [DisplayName("Antal hjul")]
        public int NumberOfWheels { get; set; }
    }

    public enum VehicleType
    {
        Bil,
        Motorcykel,
        Flygplan,
        Buss,
        Båt
    };
}
