using System;
using System.Collections.Generic;

namespace backend_root.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public short RegionId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
