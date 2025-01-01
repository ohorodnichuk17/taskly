using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces;

public interface IAuthenticationRepository
{
    Task<bool> IsUserExist(string Email);
    Task<string> AddVerificationEmail(string Email, string Code);
    Task<bool> IsVerificationEmailExistAndCodeValid(string Email, string Code);
    Task VerificateEmail(string Email);
    Task CreateNewUser(UserEntity NewUser, string Password);
    Task<UserEntity?> GetUserByEmail(string Email);
    Task<bool> IsPasswordValid(UserEntity User, string Password);
}
