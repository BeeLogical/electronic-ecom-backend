using System;
using backend_root.Models;

namespace backend_root.DTOs;

public class RegionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Product? Product { get; set; }
}
