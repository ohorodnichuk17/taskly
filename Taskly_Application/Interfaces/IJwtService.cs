using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces;

public interface IJwtService
{
   string GetJwtToken(User user);
}
