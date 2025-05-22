using System;

namespace backend_root.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Role { get; set; }
    public required StatusEnum Status { get; set; }
    public required string Password { get; set; }
}

public enum StatusEnum
{
    pending,
    active,
    inactive
}