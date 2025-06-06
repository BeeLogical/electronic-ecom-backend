using System;

namespace backend_root.DTOs;
using backend_root.Models;

public class UserDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Role { get; set; }
    public required StatusEnum Status { get; set; }
    public string? Password { get; set; }
    public int SalesCount { get; set; }
}
