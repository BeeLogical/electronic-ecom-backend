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

    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(AppDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Region)
            .Include(s => s.SalesTransactions)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name ?? string.Empty,
                RegionId = p.RegionId,
                RegionName = p.Region != null ? p.Region.Name : string.Empty,
                Price = p.Price,
                Quantity = p.Quantity,
                Description = p.Description ?? string.Empty,
                ImagePath = p.ImagePath ?? string.Empty,
                SalesTransaction = p.SalesTransactions.Select(st => st.Id).ToList()
            })
            .ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) throw new KeyNotFoundException("Product not found");

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name ?? string.Empty,
            Description = product.Description ?? string.Empty,
            Price = product.Price,
            Quantity = product.Quantity,
            RegionId = product.RegionId,
            ImagePath = product.ImagePath
        };
        return _mapper.Map<ProductDto>(productDto);
    }

    public async Task AddAsync(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto);

        if (productDto.Region == null || productDto.Region.Count == 0)
            throw new ArgumentException("At least one region must be provided.", nameof(productDto));

        string imagePath = string.Empty;

        if (productDto.Image != null)
        {
            Console.WriteLine($"WebRootPath: {_webHostEnvironment.WebRootPath}");
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.Image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            imagePath = Path.Combine("images", uniqueFileName);
        }
        foreach (var regionId in productDto.Region)
        {
            var product = _mapper.Map<Product>(productDto);
            product.RegionId = regionId;
            product.ImagePath = imagePath;
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

        string imagePath = string.Empty;

        if (productDto.Image != null)
        {
            Console.WriteLine($"WebRootPath: {_webHostEnvironment.WebRootPath}");
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.Image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            imagePath = Path.Combine("images", uniqueFileName);
        }
        product.RegionId = productDto.RegionId;
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.Quantity = productDto.Quantity;
        product.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(imagePath))
        {
            product.ImagePath = imagePath;
        }

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
    public async Task<IEnumerable<ProductDto>> GetBySearchAsync(string searchTerm, int? regionId)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) && !regionId.HasValue)
        {
            return await GetAllAsync();
        }

        bool isInt = int.TryParse(searchTerm, out int intValue);
        bool isDecimal = decimal.TryParse(searchTerm, out decimal decimalValue);

        var query = _context.Products.Include(p => p.Region).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") ||
                EF.Functions.Like(p.Description.ToLower(), $"%{searchTerm}%") ||
                EF.Functions.Like(p.Region.Name.ToLower(), $"%{searchTerm}%") ||
                (isInt && p.RegionId == intValue) ||
                (isDecimal && p.Price == decimalValue));
        }

        if (regionId.HasValue)
        {
            query = query.Where(p => p.RegionId == regionId.Value);
        }

        var products = await query.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name ?? string.Empty,
            RegionId = p.RegionId,
            RegionName = p.Region != null ? p.Region.Name : string.Empty,
            Price = p.Price,
            Quantity = p.Quantity,
            Description = p.Description ?? string.Empty,
            ImagePath = p.ImagePath ?? string.Empty,
        }).ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}
