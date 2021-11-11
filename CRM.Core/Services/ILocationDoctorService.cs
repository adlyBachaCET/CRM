using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocationDoctorService
    {
        Task<LocationDoctor> GetById(int id);
        Task<LocationDoctor> Create(LocationDoctor newEstablishmentDoctor);
        Task<List<LocationDoctor>> CreateRange(List<LocationDoctor> newEstablishmentDoctor);
        Task Delete(LocationDoctor EstablishmentDoctorToBeDeleted);
        Task DeleteRange(List<LocationDoctor> EstablishmentDoctor);

        Task<IEnumerable<LocationDoctor>> GetAll();
        Task<IEnumerable<LocationDoctor>> GetAllActif();
        Task<IEnumerable<LocationDoctor>> GetAllInActif();

    }
}
