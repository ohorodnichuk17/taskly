using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IService;

public interface IJwtService
{
   string GetJwtToken(UserEntity userEntity, bool rememberMe);
   string GetJwtToken(string publicKey, bool rememberMe);
}
