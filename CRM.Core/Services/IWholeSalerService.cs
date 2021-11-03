using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWholeSalerService
    {
        Task<WholeSaler> GetById(int id);
        Task<WholeSaler> Create(WholeSaler newWholeSaler);
        Task<List<WholeSaler>> CreateRange(List<WholeSaler> newWholeSaler);
        Task Update(WholeSaler WholeSalerToBeUpdated, WholeSaler WholeSaler);
        Task Delete(WholeSaler WholeSalerToBeDeleted);
        Task DeleteRange(List<WholeSaler> WholeSaler);

        Task<IEnumerable<WholeSaler>> GetAll();
        Task<IEnumerable<WholeSaler>> GetAllActif();
        Task<IEnumerable<WholeSaler>> GetAllInActif();

    }
}
