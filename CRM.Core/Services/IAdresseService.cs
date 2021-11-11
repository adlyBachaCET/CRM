using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IAdresseService
    {
        Task<Adresse> GetByIdUser(int id);
        Task<Adresse> Create(Adresse newEstablishmentLocality);
        Task<List<Adresse>> CreateRange(List<Adresse> newEstablishmentLocality);
        Task Delete(Adresse EstablishmentLocalityToBeDeleted);
        Task DeleteRange(List<Adresse> EstablishmentLocality);
        Task<List<Locality>> GetCompleteAdresse(int? IdAdresse);
        Task<Adresse> GetByStreetName(string Name);
        Task<IEnumerable<Adresse>> GetAll();
        Task<IEnumerable<Adresse>> GetAllActif();
        Task<IEnumerable<Adresse>> GetAllInActif();
        Task Update(Adresse EstablishmentLocalityToBeUpdated, Adresse EstablishmentLocality);

    }
}
