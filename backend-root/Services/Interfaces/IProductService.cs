using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task AddAsync(ProductDto product);
    Task UpdateAsync(int id, ProductDto product);
    Task DeleteAsync(int id);
    Task<IEnumerable<ProductDto>> GetByRegionIdAsync(int regionId);
}
