using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Models
{
    public class BillHeader
    {
        [Key]
        public int billHeaderId { get; set; }

        [Required]
        public string billFirstName{ get; set; }

        [Required]
        public string billLastName { get; set; }

        [Required]
        public string billEmail { get; set; }
        [Required]
        public string billPhone { get; set; }
        [Required]
        public string billAddressLine1{ get; set; }
        [Required]
        public string billAddressLine2 { get; set; }
        [Required]
        public string billTown { get; set; }
        [Required]
        public string billProvince { get; set; }

        public DateTime billAddDateAndTime { get; set; } = DateTime.Now;
    }
}
