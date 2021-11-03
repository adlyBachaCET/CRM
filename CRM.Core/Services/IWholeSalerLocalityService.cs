using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IWholeSalerLocalityService
    {
        Task<WholeSalerLocality> GetById(int id);
        Task<WholeSalerLocality> Create(WholeSalerLocality newWholeSalerLocality);
        Task<List<WholeSalerLocality>> CreateRange(List<WholeSalerLocality> newWholeSalerLocality);
        Task Delete(WholeSalerLocality WholeSalerLocalityToBeDeleted);
        Task DeleteRange(List<WholeSalerLocality> WholeSalerLocality);

        Task<IEnumerable<WholeSalerLocality>> GetAll();
        Task<IEnumerable<WholeSalerLocality>> GetAllActif();
        Task<IEnumerable<WholeSalerLocality>> GetAllInActif();

    }
}
