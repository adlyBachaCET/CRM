using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocationService
    {
        Task<Location> GetById(int id);
        Task<Location> Create(Location newEstablishment);
        Task<List<Location>> CreateRange(List<Location> newEstablishment);
        Task Update(Location EstablishmentToBeUpdated, Location Establishment);
        Task Delete(Location EstablishmentToBeDeleted);
        Task DeleteRange(List<Location> Establishment);
        Task<IEnumerable<Service>> GetAllServices(int Id);
        Task<IEnumerable<Location>> GetAllByType(int TypeName);
        Task<IEnumerable<Location>> GetAll();
        Task<IEnumerable<Location>> GetAllActif();
        Task<IEnumerable<Location>> GetAllInActif();
        Task Approuve(Location LocationToBeUpdated, Location Location);
        Task Reject(Location LocationToBeUpdated, Location Location); 
        Task<IEnumerable<Location>> GetByNearByActif(string Locality1, string Locality2, string Locality3, int CodePostal);
        Task<Location> GetByExistantActif(string Name, int? IdlocationType);
        Task<Location> GetByExactExistantActif(string Name, int PostalCode, string Locality1, string Locality2);

    }
}
