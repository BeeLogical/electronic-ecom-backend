using System;

namespace backend_root.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int RegionId { get; set; }
    public string? RegionName { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
    public List<int>? Region { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
}
