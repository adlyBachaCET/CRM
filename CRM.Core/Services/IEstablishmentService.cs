using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentService
    {
        Task<Establishment> GetById(int id);
        Task<Establishment> Create(Establishment newEstablishment);
        Task<List<Establishment>> CreateRange(List<Establishment> newEstablishment);
        Task Update(Establishment EstablishmentToBeUpdated, Establishment Establishment);
        Task Delete(Establishment EstablishmentToBeDeleted);
        Task DeleteRange(List<Establishment> Establishment);

        Task<IEnumerable<Establishment>> GetAll();
        Task<IEnumerable<Establishment>> GetAllActif();
        Task<IEnumerable<Establishment>> GetAllInActif();

    }
}
