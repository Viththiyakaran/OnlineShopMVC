using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Models
{
    public class Users
    {
        [Key]
        public int userid { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string  userFirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string userLastName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string userEmail { get; set; }
        [Required]
        [DisplayName("Phone")]
        public string userPhone { get; set; }
        [Required]
        [DisplayName("Address Line 1")]
        public string userAddressLine1{ get; set; }
        [Required]
        [DisplayName("Address Line 2")]
        public string userAddressLine2 { get; set; }
        [Required]
        [DisplayName("Town/City")]
        public string userTown { get; set; }
        [Required]
        [DisplayName("Province")]
        public string userProvince { get; set; }
        [Required]
        [DisplayName("Password")]
        public string userPassword{ get; set; }
        [Required]
        [DisplayName("Re Password")]
        public string userRePassword { get; set; }
    }
}
