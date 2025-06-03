using System;

namespace backend_root.DTOs;

public class TokenRequestDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
