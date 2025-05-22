using System;
using System.Collections.Generic;

namespace backend_root.Models;

public partial class SalesTransaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public SaleStatusEnum SaleStatus { get; set; } = SaleStatusEnum.pending;
    
    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public enum SaleStatusEnum
{
    completed,
    pending
}