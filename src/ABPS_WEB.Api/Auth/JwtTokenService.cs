using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ABPS_WEB.Domain.Auth;
using Microsoft.IdentityModel.Tokens;

namespace ABPS_WEB.Api.Auth;

public class JwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config) => _config = config;

    public string CreateToken(string username, int roleId)
    {
        var key = _config["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException(
                "Jwt:Key is not configured. Set it via user-secrets or an environment variable before starting the API.");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("roleId", roleId.ToString()),
            new Claim(ClaimTypes.Role, Enum.IsDefined(typeof(UserRoles), roleId) ? ((UserRoles)roleId).ToString() : "Unknown"),
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        var expiryMinutes = _config.GetValue("Jwt:ExpiryMinutes", 480);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
