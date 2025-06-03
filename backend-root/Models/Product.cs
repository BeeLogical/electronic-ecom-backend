using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_root.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [ForeignKey("Region")]
    public int RegionId { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public Region? Region { get; set; }
    public string? ImagePath { get; set; }
    
    public ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
}
