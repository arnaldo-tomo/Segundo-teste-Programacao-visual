using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    [Table("login")]
    public class login
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}