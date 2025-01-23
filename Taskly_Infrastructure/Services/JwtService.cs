using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Domain.Other;

namespace Taskly_Infrastructure.Services;

public class JwtService(IOptions<AuthanticationSettings> options) : IJwtService
{
    private readonly string JwtKey = options.Value.JwtKey;
    public string GetJwtToken(UserEntity userEntity)
    {
        var claimes = new Claim[] {
        new Claim(type:"id",value:userEntity.Id.ToString()),
        new Claim(type:"email",value:userEntity.Email!)
        };

        var token = new JwtSecurityToken(
            claims: claimes,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(JwtKey)
                ),
            SecurityAlgorithms.HmacSha256Signature)
        );
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}
