using CRM.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ISupportService
    {
        Task<Support> GetById(int? id);
        Task<Support> Create(Support newService);
        Task<List<Support>> CreateRange(List<Support> newService);
        Task Update(Support ServiceToBeUpdated, Support Service);
        Task Delete(Support ServiceToBeDeleted);
        Task DeleteRange(List<Support> Service);
        Task Send(string Name,string EmailLogin);
        Task<IEnumerable<Support>> GetAll();
        Task<IEnumerable<Support>> GetAllActif();
        Task<IEnumerable<Support>> GetAllInActif();
        Task<Support> GetByNameActif(string Name);
        Task<ClaimsPrincipal> getPrincipal(string token);



    }
}
