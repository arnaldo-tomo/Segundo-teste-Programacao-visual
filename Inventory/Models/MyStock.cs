using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    [Table("MyStock")]
    public class MyStock
    {
        [Key]
        public int id { get; set; }
        public int invoice { get; set; }
        public DateTime bill_date { get; set; }
        public string supplier_name { get; set; }
        public DateTime receive_date { get; set; }
        public int carrying_charge { get; set; }
        public string item_name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int total_price_per_item { get; set; }
        public int total_amount { get; set; }
    }
}