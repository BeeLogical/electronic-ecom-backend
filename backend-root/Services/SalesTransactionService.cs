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
        var transactions = await _context.SalesTransactions
        .Include(p => p.Product)
        .Include(p => p.User)
        .ToListAsync();

        return transactions.Select(p => new SalesTransactionDto
        {
            Id = p.Id,
            ProductId = p.ProductId,
            UserId = p.UserId,
            ProductName = p.Product?.Name ?? "",
            UserName = p.User?.Name ?? "",
            SaleStatus = Enum.Parse<DTOs.SaleStatusEnum>(p.SaleStatus.ToString()),
            TotalPrice = p.TotalPrice,
            Quantity = p.Quantity,
            UpdatedAt = p.UpdatedAt,
            RegionId = p.RegionId
        });
    }
    public async Task<SalesTransactionDto> GetByIdAsync(int id)
    {
        var transaction = await _context.SalesTransactions.FindAsync(id);
        return _mapper.Map<SalesTransactionDto>(transaction);
    }
    public async Task AddAsync(List<SalesTransactionDto> transactionDto)
    {
        if (transactionDto == null || transactionDto.Count == 0)
            throw new ArgumentException("Transaction list is empty.");

        var transactions = _mapper.Map<List<SalesTransaction>>(transactionDto);

        foreach (var transaction in transactions)
        {
            var product = await _context.Products.FindAsync(transaction.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {transaction.ProductId} not found.");

            if (product.Quantity < transaction.Quantity)
                throw new InvalidOperationException($"Insufficient stock for product ID {transaction.ProductId}.");

            product.Quantity -= transaction.Quantity;
        }

        _context.SalesTransactions.AddRange(transactions);
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
            .Include(p => p.Product)
            .Include(p => p.User)
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
    public async Task<IEnumerable<SalesTransactionDto>> GetAllGroupedByProductAsync()
    {
        var groupedTransactions = await _context.SalesTransactions
            .Include(p => p.Product)
            .Include(p => p.User)
            .GroupBy(p => new { p.Product.Name })
            .Select(g => new SalesTransactionDto
            {
                ProductId = 0,
                ProductName = g.Key.Name ?? "",
                Quantity = g.Sum(x => x.Quantity),
                TotalPrice = g.Sum(x => x.TotalPrice),
                SaleStatus = DTOs.SaleStatusEnum.completed,
                UserId = 0,
                RegionId = 0,
            })
            .ToListAsync();

        return groupedTransactions;
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetAllGroupedByRegionAsync()
    {
        var groupedByRegion = await _context.SalesTransactions
            .Include(t => t.Product)
                .ThenInclude(p => p.Region)
            .GroupBy(t => new
            {
                RegionId = t.Product.RegionId,
                RegionName = t.Product.Region.Name
            })
            .Select(g => new SalesTransactionDto
            {
                RegionId = g.Key.RegionId,
                RegionName = g.Key.RegionName ?? "",
                Quantity = g.Sum(x => x.Quantity),
                TotalPrice = g.Sum(x => x.TotalPrice),
                SaleStatus = DTOs.SaleStatusEnum.completed,
                ProductId = 0,
                ProductName = "",
                UserId = 0,
                UserName = "",
                UpdatedAt = g.Max(x => x.UpdatedAt)
            })
            .ToListAsync();

        return groupedByRegion;
    }
    public async Task<IEnumerable<SalesTransactionDto>> GetAllGroupedByRegionAndProductAsync()
    {
        var groupedData = await _context.SalesTransactions
            .Include(t => t.Product)
                .ThenInclude(p => p.Region)
            .GroupBy(t => new
            {
                RegionId = t.Product.RegionId,
                RegionName = t.Product.Region.Name,
                ProductId = t.ProductId,
                ProductName = t.Product.Name
            })
            .Select(g => new SalesTransactionDto
            {
                RegionId = g.Key.RegionId,
                RegionName = g.Key.RegionName ?? "",
                ProductId = g.Key.ProductId,
                ProductName = g.Key.ProductName ?? "",
                Quantity = g.Sum(x => x.Quantity),
                TotalPrice = g.Sum(x => x.TotalPrice),
                SaleStatus = DTOs.SaleStatusEnum.completed,
                UserId = 0,
                UserName = "",
                UpdatedAt = g.Max(x => x.UpdatedAt)
            })
            .ToListAsync();

        return groupedData;
    }


}
