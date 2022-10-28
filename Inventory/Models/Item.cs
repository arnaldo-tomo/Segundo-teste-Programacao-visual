using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int item_id { get; set; }
        public string item_name { get; set; }
        public float price { get; set; }
    }
}