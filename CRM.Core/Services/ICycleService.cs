using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ICycleService
    {
        Task<Cycle> GetById(int id);//
        Task<Cycle> Create(Cycle newCycle);//
        Task<List<Cycle>> CreateRange(List<Cycle> newCycle);
        Task Update(Cycle CycleToBeUpdated, Cycle Cycle);
        Task Delete(Cycle CycleToBeDeleted);
        Task DeleteRange(List<Cycle> Cycle);

        Task<IEnumerable<Cycle>> GetAll();
        Task<IEnumerable<Cycle>> GetAllActif();
        Task<IEnumerable<Cycle>> GetAllInActif();

    }
}
