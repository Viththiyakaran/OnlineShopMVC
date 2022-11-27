using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Models
{
    public class BillDetail
    {
        public BillDetail()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int billDetailId { get; set; }

       // [ForeignKey("BillHeader")]//very important
        public int billHeaderID { get; set; }
      

        public string billProduct { get; set; }
        public int billQty { get; set; }

        public double billPrice { get; set; }
        
        public int productID { get; set; }
        
    }
}
