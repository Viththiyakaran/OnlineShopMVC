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
        [DisplayName("Password")]
        public string userPassword{ get; set; }
        [Required]
        [DisplayName("Re Password")]
        public string userRePassword { get; set; }
    }
}
