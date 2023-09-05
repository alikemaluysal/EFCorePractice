using System;
using System.Collections.Generic;

namespace _2_ORM_vs_SQL.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
