using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetAllByType(int TypeName);
        Task<IEnumerable<Service>> GetAllServices(int Id);
        Task<IEnumerable<Location>> GetAllActif();
        Task<IEnumerable<Location>> GetAllInActif();
        Task<IEnumerable<Location>> GetAllAcceptedActif();
        Task<IEnumerable<Location>> GetAllAcceptedInactifActif();
        Task<Location> GetByIdActif(int id);

        Task<IEnumerable<Location>> GetAllPending();
        Task<IEnumerable<Location>> GetAllRejected();

        Task<Location> GetByExistantActif(string Name, int? IdlocationType);
        Task<Location> GetByExistantNameActif(string Name);

        Task<IEnumerable<Location>> GetByNearByActif(string Locality1, string Locality2, int CodePostal);
        Task<Location> GetByExactExistantActif(string Name, int PostalCode, string Locality1, string Locality2);


    }
}
