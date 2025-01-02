<<<<<<< HEAD:Taskly_Application/Interfaces/IAuthenticationRepository.cs
﻿using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces;
=======
﻿namespace Taskly_Application.Interfaces.IRepository;
>>>>>>> friend/main:Taskly_Application/Interfaces/IRepository/IAuthenticationRepository.cs

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
