namespace Taskly_Application.Interfaces;

public interface IAuthenticationRepository
{
    Task<bool> IsUserExist(string Email);
    Task<string> AddVerificationEmail(string Email, string Code);
    Task<bool> IsVerificationEmailExistAndCodeValid(string Email, string Code);
    Task VerificateEmail(string Email);
}
