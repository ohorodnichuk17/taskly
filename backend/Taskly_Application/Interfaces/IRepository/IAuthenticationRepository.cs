using ErrorOr;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IAuthenticationRepository : IRepository<UserEntity>
{
    Task<bool> IsUserExist(string email);
    Task<bool> IsUserExist(Guid id);
    Task<string> AddVerificationEmail(string email, string code);
    Task RemovePreviousCodeIfExist(string email);
    Task<bool> IsVerificationEmailExistAndCodeValid(string email, string code);
    Task VerificateEmail(string email);
    //Task CreateNewUser(UserEntity NewUser, string Password);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> GetUserByPublicKey(string publicKey);
    Task<bool> IsPasswordValid(UserEntity user, string password);
    Task<ErrorOr<UserEntity>> CreateNewUser(UserEntity newUser, string password);
    Task AddChangePasswordKey(string email, Guid key);
    Task<bool> HasUserSentRequestToChangePassword(string email, Guid key);
    Task<string?> GetUserEmailByChangePasswordKeyAsync(Guid key);
    Task<ErrorOr<Guid>> ChangePasswordAsync(UserEntity user, string password);
    // Task<UserEntity> GetSolanaUserProfile(Guid userId);
    Task<ErrorOr<UserEntity>> SetUserNameForSolanaUserAsync(string publicKey, string userName);
    Task<string> GenerateReferralCode();
    Task<UserEntity> GetUserByReferralCodeAsync(string referralCode);
    Task<string?> GetUserReferralCodeAsync(Guid userId);
    Task<string> GetUserRoleByIdAsync(Guid UserId);
}
