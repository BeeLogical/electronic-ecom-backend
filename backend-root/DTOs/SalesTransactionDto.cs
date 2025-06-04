using System;

namespace backend_root.DTOs;

public class SalesTransactionDto
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
    public required decimal TotalPrice { get; set; }
    public required SaleStatusEnum SaleStatus { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public required int RegionId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string RegionName { get; set; } = string.Empty;

}

public enum SaleStatusEnum
{
    completed,
    pending
}
