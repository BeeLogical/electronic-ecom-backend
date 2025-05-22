using System;
using AutoMapper;
using backend_root.Data;
using backend_root.DTOs;
using backend_root.Models;
using backend_root.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_root.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task AddAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) throw new Exception("Product not found");

        product.Name = productDto.Name;
        product.Price = productDto.Price;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) throw new Exception("Product not found");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<ProductDto>> GetByRegionIdAsync(int regionId)
    {
        var products = await _context.Products
            .Where(p => p.RegionId == regionId)
            .ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}
