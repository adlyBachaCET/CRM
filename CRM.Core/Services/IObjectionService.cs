using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IObjectionService
    {
        Task<Objection> GetById(int id);
        Task<Objection> Create(Objection newObjection);
        Task<List<Objection>> CreateRange(List<Objection> newObjection);
        Task Update(Objection ObjectionToBeUpdated, Objection Objection);
        Task Delete(Objection ObjectionToBeDeleted);
        Task DeleteRange(List<Objection> Objection);
        Task<IEnumerable<Objection>> GetByIdPharmacy(RequestObjection RequestObjection, int id);


        Task<IEnumerable<Objection>> GetAll();
        Task<IEnumerable<Objection>> GetAllActif();
        Task<IEnumerable<Objection>> GetAllInActif();
        Task<IEnumerable<Objection>> GetByIdDoctor(RequestObjection RequestObjection, int id);
        Task<IEnumerable<Objection>> GetByIdActifDoctor(int Id);
        Task<IEnumerable<Objection>> GetByIdActifUser(int Id);
        Task<IEnumerable<Objection>> GetAll(Status? Status, RequestObjection RequestObjection);
        Task<Objection> GetById(int Id, Status? Status, RequestObjection RequestObjection);
        Task Reject(Objection ObjectionToBeUpdated, Objection Objection);
        Task Approuve(Objection ObjectionToBeUpdated, Objection Objection);
    }
}
