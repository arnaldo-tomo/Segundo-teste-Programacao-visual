using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    [Table("Total_Customer_Order")]
    public class Total_Customer_Order
    {
        [Key]
        public int id { get; set; }
        public int invoice_no_tc { get; set; }
        public int total_order_amount { get; set; }

        public string date { get; set; }
    }
}