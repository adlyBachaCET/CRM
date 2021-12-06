using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetByIdUser(int id);
        Task<Activity> GetById(int id);
        Task<Activity> Create(Activity newActivity);
        Task<List<Activity>> CreateRange(List<Activity> newActivity);
        Task Update(Activity ActivityToBeUpdated, Activity Activity);
        Task Delete(Activity ActivityToBeDeleted);
        Task DeleteRange(List<Activity> Activity);
        Task<List<Activity>> GetByIdUserByToday(int id);
        Task<IEnumerable<Activity>> GetAll();
        Task<IEnumerable<Activity>> GetAllActif();
        Task<IEnumerable<Activity>> GetAllInActif();
        
        Task Approuve(Activity ActivityToBeUpdated, Activity Activity);
        Task Reject(Activity ActivityToBeUpdated, Activity Activity);
 

    }
}
