using System;

namespace backend_root.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int UserCount { get; set; }
}
