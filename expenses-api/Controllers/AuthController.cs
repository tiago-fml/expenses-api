using expenses_api.DTOs.User;
using expenses_api.Services.Jwt;
using expenses_api.Services.Users;
using expenses_api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    
    public AuthController(IJwtService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
    {
        var user = await _userService.GetUserForAuthenticationAsync(userLoginDto.Username);
        if (user is null)
        {
            return NotFound($"User with username: {userLoginDto.Username}, was not found");
        }
        
        if (!PasswordHasher.VerifyPassword(user.HashedPassword, userLoginDto.Password)) return Unauthorized();
        
        var tokenString = _jwtService.GenerateJwtToken(user.Username, user.Id);

        return Ok(new { Token = tokenString });
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Singup([FromBody] UserCreateDTO userCreateDto)
    {
        var user = await _userService.AddUserAsync(userCreateDto);
        
        var tokenString = _jwtService.GenerateJwtToken(user.Username, user.Id);

        return Ok(new { Token = tokenString });
    }
}