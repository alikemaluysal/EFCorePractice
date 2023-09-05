using System;
using System.Collections.Generic;

namespace _2_ORM_vs_SQL.Models
{
    public partial class ProductSalesFor1997
    {
        public string CategoryName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal? ProductSales { get; set; }
    }
}
