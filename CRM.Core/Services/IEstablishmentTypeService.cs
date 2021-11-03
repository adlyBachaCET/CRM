using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentTypeService
    {
        Task<EstablishmentType> GetById(int id);
        Task<EstablishmentType> Create(EstablishmentType newEstablishmentType);
        Task<List<EstablishmentType>> CreateRange(List<EstablishmentType> newEstablishmentType);
        Task Update(EstablishmentType EstablishmentTypeToBeUpdated, EstablishmentType EstablishmentType);
        Task Delete(EstablishmentType EstablishmentTypeToBeDeleted);
        Task DeleteRange(List<EstablishmentType> EstablishmentType);

        Task<IEnumerable<EstablishmentType>> GetAll();
        Task<IEnumerable<EstablishmentType>> GetAllActif();
        Task<IEnumerable<EstablishmentType>> GetAllInActif();

    }
}
