using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocationTypeService
    {
        Task<LocationType> GetById(int? id);
        Task<LocationType> Create(LocationType newEstablishmentType);
        Task<List<LocationType>> CreateRange(List<LocationType> newEstablishmentType);
        Task Update(LocationType EstablishmentTypeToBeUpdated, LocationType EstablishmentType);
        Task Delete(LocationType EstablishmentTypeToBeDeleted);
        Task DeleteRange(List<LocationType> EstablishmentType);

        Task<IEnumerable<LocationType>> GetAll();
        Task<IEnumerable<LocationType>> GetAllActif();
        Task<IEnumerable<LocationType>> GetAllInActif();
        Task Approuve(LocationType LocationTypeToBeUpdated, LocationType LocationType);
        Task Reject(LocationType LocationTypeToBeUpdated, LocationType LocationType);


    }
}
