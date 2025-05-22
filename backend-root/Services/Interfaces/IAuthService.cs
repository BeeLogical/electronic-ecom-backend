using System;
using backend_root.DTOs;

namespace backend_root.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginDto);
}
