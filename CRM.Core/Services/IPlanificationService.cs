using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPlanificationService
    {
        Task<Planification> GetById(int? id);
        Task<Planification> Create(Planification newPlanification);
        Task<List<Planification>> CreateRange(List<Planification> newPlanification);
        Task Update(Planification PlanificationToBeUpdated, Planification Planification);
        Task Delete(Planification PlanificationToBeDeleted);
        Task DeleteRange(List<Planification> Planification);

        Task<IEnumerable<Planification>> GetAll();
        Task<IEnumerable<Planification>> GetAllActif();
        Task<IEnumerable<Planification>> GetAllInActif();
        
        Task Approuve(Planification PlanificationToBeUpdated, Planification Planification);
        Task Reject(Planification PlanificationToBeUpdated, Planification Planification);
 

    }
}
