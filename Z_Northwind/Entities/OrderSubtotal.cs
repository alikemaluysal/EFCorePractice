﻿using System;
using System.Collections.Generic;

namespace Z_Northwind.Entities;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
