using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentServiceService
    {
        Task<EstablishmentService> GetById(int id);
        Task<EstablishmentService> Create(EstablishmentService newEstablishmentService);
        Task<List<EstablishmentService>> CreateRange(List<EstablishmentService> newEstablishmentService);
        Task Delete(EstablishmentService EstablishmentServiceToBeDeleted);
        Task DeleteRange(List<EstablishmentService> EstablishmentService);

        Task<IEnumerable<EstablishmentService>> GetAll();
        Task<IEnumerable<EstablishmentService>> GetAllActif();
        Task<IEnumerable<EstablishmentService>> GetAllInActif();

    }
}
