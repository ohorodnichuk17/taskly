using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Domain.ValueObjects;

namespace Taskly_Infrastructure.Services;

public class JwtService(IOptions<AuthenticationSettings> options) : IJwtService
{
    private readonly string JwtKey = options.Value.JwtKey;
    public string GetJwtToken(UserEntity userEntity, bool rememberMe)
    {
        var claimes = new Claim[] {
        new Claim(type:"id",value:userEntity.Id.ToString()),
        new Claim(type:"email",value:userEntity.Email!),
        };

        var token = new JwtSecurityToken(
            claims: claimes,
            notBefore: DateTime.UtcNow,
            expires: rememberMe == true ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(JwtKey)
                ),
            SecurityAlgorithms.HmacSha256Signature)
        );
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }

    /// <summary>
    /// Generates a JWT token for a Solana wallet address.
    /// </summary>
    /// <param name="publicKey">The Solana wallet public key.</param>
    /// <param name="rememberMe">Whether the token should have an extended expiration time.</param>
    /// <returns>A JWT token as a string.</returns>
    public string GetJwtToken(string publicKey, string userId, bool rememberMe)
    {
        var claims = new Claim[]
        {
            new Claim(type:"id", value : userId),
            new Claim(type: "publicKey", value: publicKey),
            new Claim(type: "jti", value: Guid.NewGuid().ToString())
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
                SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
