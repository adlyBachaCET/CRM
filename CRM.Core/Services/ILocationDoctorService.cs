using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocationDoctorService
    {
        Task<LocationDoctor> GetById(int id1, int Id2);

        Task<LocationDoctor> GetByIdActif(int id, int IdLocation);
        Task<LocationDoctor> Create(LocationDoctor newEstablishmentDoctor);
        Task<List<LocationDoctor>> CreateRange(List<LocationDoctor> newEstablishmentDoctor);
        Task Delete(LocationDoctor EstablishmentDoctorToBeDeleted);
        Task DeleteRange(List<LocationDoctor> EstablishmentDoctor);
        Task<IEnumerable<LocationDoctor>> GetAllAcceptedActif(int Id);
        Task<IEnumerable<LocationDoctor>> GetAll();
        Task<IEnumerable<LocationDoctor>> GetAllActif();
        Task<IEnumerable<LocationDoctor>> GetAllInActif();
        Task<LocationDoctor> GetByIdLocationAndService(int id, int IdLocation);
    }
}
