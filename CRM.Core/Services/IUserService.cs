using CRM.Core.Models;
using CRM_API.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<User> Create(User newUser);
        Task<List<User>> CreateRange(List<User> newUser);
        Task Update(User UserToBeUpdated, User User);
        Task Delete(User UserToBeDeleted);
        Task DeleteRange(List<User> User);
        string GenerateJSONWebToken(User userInfo);
        Task<User> AuthenticateManager(LoginModel lm);
        Task<User> AuthenticateDelegate(LoginModel lm);
        Task<IEnumerable<User>> GetAll();
        Task UpdatePassword(int Id, string Password);
        Task UpdatePhoto(int Id, string FileName);

        Task<IEnumerable<User>> GetAllActif();
        Task<IEnumerable<User>> GetAllInActif();
        Task<IEnumerable<User>> GetAllDelegateByIdBu(int IdBu);
        Task<IEnumerable<User>> GetAllDelegateByIdBu(List<int> Bu);

    }
}
