using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Models
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerID { get; set; }
        [Required]
        [DisplayName("Manufacturer Name")]
        public string ManufacturerName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string ManufacturerAddress { get; set; }
        [Required]
        [DisplayName("Contact Number ")]
        public string ManufacturerContactNumber { get; set; }
        [Required]
        [DisplayName("Product Type ")]
        public string ManufacturerProductType{ get; set; }

        public bool ManufacturerIsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
