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
}

public enum SaleStatusEnum
{
    completed,
    pending
}
