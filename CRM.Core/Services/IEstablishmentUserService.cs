using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IEstablishmentUserService
    {
        Task<EstablishmentUser> GetById(int id);
        Task<EstablishmentUser> Create(EstablishmentUser newEstablishmentUser);
        Task<List<EstablishmentUser>> CreateRange(List<EstablishmentUser> newEstablishmentUser);
        Task Delete(EstablishmentUser EstablishmentUserToBeDeleted);
        Task DeleteRange(List<EstablishmentUser> EstablishmentUser);

        Task<IEnumerable<EstablishmentUser>> GetAll();
        Task<IEnumerable<EstablishmentUser>> GetAllActif();
        Task<IEnumerable<EstablishmentUser>> GetAllInActif();

    }
}
