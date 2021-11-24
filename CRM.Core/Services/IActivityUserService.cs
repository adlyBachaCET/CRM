using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IActivityUserService
    {
        Task<ActivityUser> GetById(int? id);
        Task<ActivityUser> Create(ActivityUser newActivityUser);
        Task<List<ActivityUser>> CreateRange(List<ActivityUser> newActivityUser);
        Task Update(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser);
        Task Delete(ActivityUser ActivityUserToBeDeleted);
        Task DeleteRange(List<ActivityUser> ActivityUser);

        Task<IEnumerable<ActivityUser>> GetAll();
        Task<IEnumerable<ActivityUser>> GetAllActif();
        Task<IEnumerable<ActivityUser>> GetAllInActif();
        
        Task Approuve(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser);
        Task Reject(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser);
 

    }
}
