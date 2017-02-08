using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string RegNumber { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int NumberOfWheels { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Motorcycle,
        Airplane,
        Bus,
        Boat
    };
}
