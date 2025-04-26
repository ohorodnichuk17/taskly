using ErrorOr;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IAuthenticationRepository : IRepository<UserEntity>
{
    Task<bool> IsUserExist(string Email);
    Task<bool> IsUserExist(Guid Id);
    Task<string> AddVerificationEmail(string Email, string Code);
    Task<bool> IsVerificationEmailExistAndCodeValid(string Email, string Code);
    Task VerificateEmail(string Email);
    //Task CreateNewUser(UserEntity NewUser, string Password);
    Task<UserEntity?> GetUserByEmail(string Email);
    Task<bool> IsPasswordValid(UserEntity User, string Password);
    Task<ErrorOr<UserEntity>> CreateNewUser(UserEntity NewUser, string Password);
    Task AddChangePasswordKey(string Email, Guid Key);
    Task<bool> HasUserSentRequestToChangePassword(string Email, Guid Key);
    Task<string?> GetUserEmailByChangePasswordKeyAsync(Guid Key);
    Task<ErrorOr<Guid>> ChangePasswordAsync(UserEntity user, string Password);
}
