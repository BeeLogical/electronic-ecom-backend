using System;

namespace backend_root.DTOs;

public class SalesTransactionForm
{
    public List<SalesTransactionDto> TransactionDto { get; set; } = new();
}
