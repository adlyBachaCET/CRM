using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentLocalityService
    {
        Task<EstablishmentLocality> GetById(int id);
        Task<EstablishmentLocality> Create(EstablishmentLocality newEstablishmentLocality);
        Task<List<EstablishmentLocality>> CreateRange(List<EstablishmentLocality> newEstablishmentLocality);
        Task Delete(EstablishmentLocality EstablishmentLocalityToBeDeleted);
        Task DeleteRange(List<EstablishmentLocality> EstablishmentLocality);

        Task<IEnumerable<EstablishmentLocality>> GetAll();
        Task<IEnumerable<EstablishmentLocality>> GetAllActif();
        Task<IEnumerable<EstablishmentLocality>> GetAllInActif();

    }
}
