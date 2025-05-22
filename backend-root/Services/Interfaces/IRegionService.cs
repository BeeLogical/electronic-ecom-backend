using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface IRegionService
{
    Task<IEnumerable<RegionDto>> GetAllAsync();
    Task<RegionDto> GetByIdAsync(int id);
    Task AddAsync(RegionDto region);
    Task UpdateAsync(int id, RegionDto region);
    Task DeleteAsync(int id);
}
