using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IInfoService
    {
        Task<Info> GetById(int id);
        Task<Info> Create(Info newInfo);
        Task<List<Info>> CreateRange(List<Info> newInfo);
        Task Update(Info InfoToBeUpdated, Info Info);
        Task Delete(Info InfoToBeDeleted);
        Task DeleteRange(List<Info> Info);

        Task<IEnumerable<Info>> GetAll();
        Task<IEnumerable<Info>> GetAllActif();
        Task<IEnumerable<Info>> GetAllInActif();
        Task<IEnumerable<Info>> GetByIdDoctor(int id);
    }
}
