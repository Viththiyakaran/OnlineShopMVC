using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
       
        public string ProductImageName { get; set; }

        public string ProductCategory { get; set; }

        public DateTime ProductAddDateAndTime { get; set; } = DateTime.Now;
    }

    
}
