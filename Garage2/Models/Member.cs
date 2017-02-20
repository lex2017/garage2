using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [DisplayName("Namn")]
        [Required(ErrorMessage = "Namnet måste finnas.")]
        public string Name { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}