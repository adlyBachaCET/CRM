using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface ITagsDoctorRepository : IRepository<TagsDoctor>
    {
        Task<IEnumerable<TagsDoctor>> GetAllActif();
        Task<IEnumerable<TagsDoctor>> GetAllInActif();
        Task<IEnumerable<TagsDoctor>> GetAllAcceptedActif();
        Task<IEnumerable<TagsDoctor>> GetAllAcceptedInactifActif();
        Task<IEnumerable<TagsDoctor>> GetAllPending();
        Task<IEnumerable<TagsDoctor>> GetAllRejected();
        Task<IEnumerable<TagsDoctor>> GetByIdActif(int id);
    }
}
