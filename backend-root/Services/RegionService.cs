using System;
using AutoMapper;
using backend_root.Data;
using backend_root.DTOs;
using backend_root.Models;
using backend_root.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_root.Services;

public class RegionService : IRegionService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public RegionService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RegionDto>> GetAllAsync()
    {
        var regions = await _context.Regions.ToListAsync();
        return _mapper.Map<List<RegionDto>>(regions);
    }
    public async Task<RegionDto> GetByIdAsync(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        return _mapper.Map<RegionDto>(region);
    }
    public async Task AddAsync(RegionDto regionDto)
    {
        var region = _mapper.Map<Region>(regionDto);
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, RegionDto regionDto)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region == null) throw new Exception("Region not found");

        region.Name = regionDto.Name;

        _context.Regions.Update(region);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region == null) throw new Exception("Region not found");

        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();
    }

}
