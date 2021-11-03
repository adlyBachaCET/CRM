using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentDoctorService
    {
        Task<EstablishmentDoctor> GetById(int id);
        Task<EstablishmentDoctor> Create(EstablishmentDoctor newEstablishmentDoctor);
        Task<List<EstablishmentDoctor>> CreateRange(List<EstablishmentDoctor> newEstablishmentDoctor);
        Task Delete(EstablishmentDoctor EstablishmentDoctorToBeDeleted);
        Task DeleteRange(List<EstablishmentDoctor> EstablishmentDoctor);

        Task<IEnumerable<EstablishmentDoctor>> GetAll();
        Task<IEnumerable<EstablishmentDoctor>> GetAllActif();
        Task<IEnumerable<EstablishmentDoctor>> GetAllInActif();

    }
}
