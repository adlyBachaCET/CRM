using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IVisitService
    {
        Task<Visit> GetById(int id);

        Task<Visit> Create(Visit newVisit);
        Task<List<Visit>> CreateRange(List<Visit> newVisit);
        Task Update(Visit VisitToBeUpdated, Visit Visit);
        Task Delete(Visit VisitToBeDeleted);
        Task DeleteRange(List<Visit> Visit);

        Task<IEnumerable<Visit>> GetAll();
        Task<IEnumerable<Visit>> GetAllActif();
        Task<IEnumerable<Visit>> GetAllInActif();
        Task<IEnumerable<Visit>> GetAllById(int Id);

    }
}
