// using MathHunt.Core.Abstraction.IRepositories;
// using MathHunt.Core.Abstraction.IServices;
// using MathHunt.Core.Models;
//
// namespace MathHunt.Application;
//
// public class AppUserService(IAppUserRepository repository) : IAppUserService
// {
//     public async Task<List<AppUser>> GetAllUser()
//     {
//         return await repository.Get();
//     }
//     
//     public async Task<string> RegisterUser(AppUser user, string password, string role)
//     {
//         return await repository.Register(user, password, role);
//     }
//     
//     public async Task<bool> LoginUser(string email, string password, bool rememberMe, bool lockOnFail)
//     {
//         return await repository.Login(email, password, rememberMe, lockOnFail);
//     }
//     
//     public async Task<bool> DeleteUser(string email)
//     {
//         return await repository.Delete(email);
//     }
//     
//     public async Task<AppUser?> GetUsersSkill(string email)
//     {
//         return await repository.GetSkill(email);
//     }
// }