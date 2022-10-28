using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    [Table("Sell_Items_To_Customer")]
    public class Sell_Items_To_Customer
    {
        [Key]
        public int id { get; set; }
        public int invoice { get; set; }
        public string bill_date { get; set; }
        public string customer_name { get; set; }

        public string item_name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int total_price_per_item { get; set; }
        public int total_amount { get; set; }
    }
}