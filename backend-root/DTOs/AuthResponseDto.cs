using System;

namespace backend_root.DTOs;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
