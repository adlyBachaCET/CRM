using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IExternalsService
    {

        Task<Externals> Create(Externals newExternals);
        Task<List<Externals>> CreateRange(List<Externals> newExternals);
        Task Update(Externals ExternalsToBeUpdated, Externals Externals);
        Task Delete(Externals ExternalsToBeDeleted);
        Task DeleteRange(List<Externals> Externals);
        Task<IEnumerable<Externals>> GetAll();
        Task<IEnumerable<Externals>> GetAllActif();
        Task<IEnumerable<Externals>> GetAllInActif();
        Task<IEnumerable<Externals>> GetAllById(int Id);


    }
}
