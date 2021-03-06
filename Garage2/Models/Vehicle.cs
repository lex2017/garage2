﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Reg.nummer måste finnas.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ange riktig Reg. nummer (minst 3 och max 30 tecken).")]
        [DisplayName("Reg. nummer")]
        public string RegNumber { get; set; }
        [RegularExpression("^[a-zA-ZåäöÅÄÖ]*$", ErrorMessage = "Bara bokstäver är tillåtna.")]
        [DisplayName("Färg")]
        public string Color { get; set; }
        [DisplayName("Märke")]
        public string Manufacturer { get; set; }
        [DisplayName("Modell")]
        public string Model { get; set; }
        [Range(0, 22, ErrorMessage = "Antal hjul måste vara 0-22.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bara siffror är tillåtna.")]
        [DisplayName("Antal hjul")]
        public int NumberOfWheels { get; set; }
        [DisplayName("In-checkningstid")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}")]
        public DateTime ParkAt { get; set; }

        [DisplayName("Fordonstyp")]
        public int VehicleTypeId { get; set; }
        [DisplayName("Medlemsnamn")]
        public int MemberId { get; set; }
        [DisplayName("Medlemsnamn")]
        public virtual Member Member { get; set; }
        [DisplayName("Fordonstyp")]
        public virtual VehicleType VehicleType { get; set; }
        
    }
}
