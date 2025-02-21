using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ComputeHash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashBytes = SHA256.HashData(passwordBytes);

        var builder = new StringBuilder();

        foreach (var hashByte in hashBytes)
            builder.Append(hashByte.ToString("x2"));
        
        return builder.ToString();
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new("username", email),
            new(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(2), credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}