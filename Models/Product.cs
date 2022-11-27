using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Models
{
    public class Product
    {  
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double ProductPrice { get; set; }

        [NotMapped]
        public IFormFile ProductImageFile { get; set; }
        [Required]
        public string ProductImageName { get; set; }
        [Required]
        public string ProductCategory { get; set; }

        public DateTime ProductAddDateAndTime { get; set; } = DateTime.Now;

        
        [DisplayName("Manufacturer ")]
        public int ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [DisplayName("Product Stock")]
        public int ProductStock { get; set; }
    }

    public class OrderRecords
    {
        public IEnumerable<BillDetail> billDetail { get; set; }

        public IEnumerable<BillHeader> billHeader { get; set; }
    }

    
}
