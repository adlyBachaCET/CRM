using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IVisitUserService
    {
        Task<VisitUser> GetById(int id);

        Task<VisitUser> Create(VisitUser newVisitUser);
        Task<List<VisitUser>> CreateRange(List<VisitUser> newVisitUser);
        Task Update(VisitUser VisitUserToBeUpdated, VisitUser VisitUser);
        Task Delete(VisitUser VisitUserToBeDeleted);
        Task DeleteRange(List<VisitUser> VisitUser);

        Task<IEnumerable<VisitUser>> GetAll();
        Task<IEnumerable<VisitUser>> GetAllActif();
        Task<IEnumerable<VisitUser>> GetAllInActif();
        Task<IEnumerable<VisitUser>> GetAllById(int Id);

    }
}
