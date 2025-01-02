using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IService;

public interface IJwtService
{
   string GetJwtToken(UserEntity userEntity);
}
