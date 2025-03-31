using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APP.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
	private readonly IUserRepository _userRepo;
	private readonly IConfiguration _config;

	public AuthController(IUserRepository userRepo, IConfiguration config)
	{
		_userRepo = userRepo;
		_config = config;
	}

	[HttpPost("users")]
	public async Task<IActionResult> Register(UserDto dto)
	{
		var existing = await _userRepo.GetByUsernameAsync(dto.Username);
		if (existing != null) return BadRequest("User already exists.");

		var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);
		var user = new User { Username = dto.Username, PasswordHash = hashed };
		await _userRepo.AddAsync(user);
		return Ok(new { message = "User created" });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(UserDto dto)
	{
		var user = await _userRepo.GetByUsernameAsync(dto.Username);
		if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
			return Unauthorized();

		var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
						new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
											};
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var token = new JwtSecurityToken(
				_config["JWT_Issuer"],
				_config["JWT_Audience"],
				claims,
				expires: DateTime.UtcNow.AddDays(1),
				signingCredentials: creds
		);

		return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token) });
	}
}
