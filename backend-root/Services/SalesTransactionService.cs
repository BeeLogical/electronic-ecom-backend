using System;
using AutoMapper;
using backend_root.Data;
using backend_root.DTOs;
using backend_root.Models;
using backend_root.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_root.Services;

public class SalesTransactionService : ISalesTransactionService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public SalesTransactionService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetAllAsync()
    {
        var transactions = await _context.SalesTransactions.ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    public async Task<SalesTransactionDto> GetByIdAsync(int id)
    {
        var transaction = await _context.SalesTransactions.FindAsync(id);
        return _mapper.Map<SalesTransactionDto>(transaction);
    }
    public async Task AddAsync(SalesTransactionDto transactionDto)
    {
        var transaction = _mapper.Map<SalesTransaction>(transactionDto);
        _context.SalesTransactions.Add(transaction);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, SalesTransactionDto transactionDto)
    {
        var transaction = await _context.SalesTransactions.FindAsync(id);
        if (transaction == null) throw new Exception("Transaction not found");

        transaction.ProductId = transactionDto.ProductId;
        transaction.UserId = transactionDto.UserId;
        transaction.Quantity = transactionDto.Quantity;
        transaction.TotalPrice = transactionDto.TotalPrice;

        _context.SalesTransactions.Update(transaction);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var transaction = await _context.SalesTransactions.FindAsync(id);
        if (transaction == null) throw new Exception("Transaction not found");

        _context.SalesTransactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetByUserIdAsync(int userId)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetByProductIdAsync(int productId)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.ProductId == productId)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.UpdatedAt >= startDate && t.UpdatedAt <= endDate)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    
    public async Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
        var transactions = await _context.SalesTransactions
            .Where(t => t.UserId == userId && t.UpdatedAt >= startDate && t.UpdatedAt <= endDate)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetByProductIdAndDateRangeAsync(int productId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.ProductId == productId && t.UpdatedAt >= startDate && t.UpdatedAt <= endDate)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }  
    public async Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndProductIdAsync(int userId, int productId)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.UserId == userId && t.ProductId == productId)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetByUserIdAndProductIdAndDateRangeAsync(int userId, int productId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _context.SalesTransactions
            .Where(t => t.UserId == userId && t.ProductId == productId && t.UpdatedAt >= startDate && t.UpdatedAt <= endDate)
            .ToListAsync();
        return _mapper.Map<List<SalesTransactionDto>>(transactions);
    }

}
