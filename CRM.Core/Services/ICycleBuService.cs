using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ICycleBuService
    {
        Task<CycleBu> GetById(int id);
        Task<CycleBu> Create(CycleBu newCycleBu);
        Task<List<CycleBu>> CreateRange(List<CycleBu> newCycleBu);
        Task Delete(CycleBu CycleBuToBeDeleted);
        Task DeleteRange(List<CycleBu> CycleBu);

        Task<IEnumerable<CycleBu>> GetAll();
        Task<IEnumerable<CycleBu>> GetAllActif();
        Task<IEnumerable<CycleBu>> GetAllInActif();

    }
}
