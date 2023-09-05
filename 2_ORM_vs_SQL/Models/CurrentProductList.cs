using System;
using System.Collections.Generic;

namespace _2_ORM_vs_SQL.Models
{
    public partial class CurrentProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
