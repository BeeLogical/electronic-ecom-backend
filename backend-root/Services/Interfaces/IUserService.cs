using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task AddAsync(UserDto user);
    Task UpdateAsync(int id, UserDto user);
    Task DeleteAsync(int id);
}
