using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IRequestRpService
    {
        Task<RequestRp> GetById(int? id);
        Task<RequestRp> Create(RequestRp newRequestRp);
        Task<List<RequestRp>> CreateRange(List<RequestRp> newRequestRp);
        Task Update(RequestRp RequestRpToBeUpdated, RequestRp RequestRp);
        Task Delete(RequestRp RequestRpToBeDeleted);
        Task DeleteRange(List<RequestRp> RequestRp);

        Task<IEnumerable<RequestRp>> GetAll();
        Task<IEnumerable<RequestRp>> GetAllActif();
        Task<IEnumerable<RequestRp>> GetAllInActif();
        Task Approuve(RequestRp RequestRpToBeUpdated, RequestRp RequestRp);
        Task Reject(RequestRp RequestRpToBeUpdated, RequestRp RequestRp);
    }
}
