using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }

    }
}