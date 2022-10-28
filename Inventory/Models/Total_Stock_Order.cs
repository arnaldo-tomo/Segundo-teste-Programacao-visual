using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    [Table("Total_Stock_Order")]
    public class Total_Stock_Order
    {
        [Key]
        public int id { get; set; }
        public int invoice_no_t { get; set; }
        public int total_order_amount { get; set; }

        public DateTime date { get; set; }
    }
}