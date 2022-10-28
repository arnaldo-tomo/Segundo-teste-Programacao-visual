using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    [Table("TotalItemsQuantity")]
    public class TotalItemsQuantity
    {
        [Key]
        public int id { get; set; }
        public string item_name { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
    }
}