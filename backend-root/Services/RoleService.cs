using System;
using AutoMapper;
using backend_root.Data;
using backend_root.DTOs;
using backend_root.Models;
using backend_root.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_root.Services;

public class RoleService : IRoleService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public RoleService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        var roles = await _context.Roles
        .Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            UserCount = _context.Users.Count(user => user.Role == role.Name)
        })
        .ToListAsync();
        return _mapper.Map<List<RoleDto>>(roles);
    }
    public async Task<RoleDto> GetByIdAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        return _mapper.Map<RoleDto>(role);
    }
    public async Task AddAsync(RoleDto roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, RoleDto roleDto)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) throw new Exception("Role not found");

        role.Name = roleDto.Name;

        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) throw new Exception("Role not found");

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
    public async Task<RoleDto> GetByNameAsync(string name)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        return _mapper.Map<RoleDto>(role);
    }

}
