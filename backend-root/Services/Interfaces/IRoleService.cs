using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync();
    Task<RoleDto> GetByIdAsync(int id);
    Task AddAsync(RoleDto role);
    Task UpdateAsync(int id, RoleDto role);
    Task DeleteAsync(int id);
    Task<RoleDto> GetByNameAsync(string name);
}
