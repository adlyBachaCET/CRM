using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Repository
{
    public interface IObjectionRepository : IRepository<Objection>
    {
        Task<IEnumerable<Objection>> GetAll(Status? Status, RequestObjection RequestObjection);
        Task<IEnumerable<Objection>> GetAllByReport(Status? Status, int IdReport);
        Task<IEnumerable<Objection>> GetAll(Status? Status);
        Task<Objection> GetById(int Id, Status? Status, RequestObjection RequestObjection);
        Task<IEnumerable<Objection>> GetAllActif();
        Task<IEnumerable<Objection>> GetAllInActif();
        Task<IEnumerable<Objection>> GetAllAcceptedActif();
        Task<IEnumerable<Objection>> GetAllAcceptedInactifActif();
        Task<IEnumerable<Objection>> GetAllPending();
        Task<IEnumerable<Objection>> GetAllRejected();
        Task<Objection> GetByIdActif(int id);
        Task<IEnumerable<Objection>> GetByIdDoctor(RequestObjection RequestObjection, int id);
        Task<IEnumerable<Objection>> GetByIdActifDoctor(int Id);

        Task<IEnumerable<Objection>> GetByIdActifUser(int Id);
        Task<IEnumerable<Objection>> GetByIdPharmacy(RequestObjection RequestObjection, int id);

    }
}
