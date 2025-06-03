using System;
using System.Collections.Generic;

namespace backend_root.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
