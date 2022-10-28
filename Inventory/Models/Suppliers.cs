using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    [Table("Suppliers")]
    public class Suppliers
    {
        [Key]
        public int sup_id { get; set; }
        public string sup_name { get; set; }
        public int mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
}