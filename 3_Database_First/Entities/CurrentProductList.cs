using System;
using System.Collections.Generic;

namespace _3_Database_First.Entities;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
