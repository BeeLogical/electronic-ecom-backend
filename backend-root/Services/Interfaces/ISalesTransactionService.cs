using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface ISalesTransactionService
{
    Task<IEnumerable<SalesTransactionDto>> GetAllAsync();
    Task<SalesTransactionDto> GetByIdAsync(int id);
    Task AddAsync(List<SalesTransactionDto> salestransaction);
    Task UpdateAsync(int id, SalesTransactionDto salestransaction);
    Task DeleteAsync(int id);
    Task<IEnumerable<SalesTransactionDto>> GetByUserIdAsync(int userId);
    Task<IEnumerable<SalesTransactionDto>> GetByProductIdAsync(int productId);
    Task<IEnumerable<SalesTransactionDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<SalesTransactionDto>> GetByProductIdAndDateRangeAsync(int productId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndProductIdAsync(int userId, int productId);
    Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndProductIdAndDateRangeAsync(int userId, int productId, DateTime startDate, DateTime endDate);
    
}
