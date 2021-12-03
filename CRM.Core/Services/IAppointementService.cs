using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IAppointementService
    {
        Task<List<Appointement>> GetByIdUser(int id);
        Task<Appointement> GetById(int id);
        Task<Appointement> Create(Appointement newAppointement);
        Task<List<Appointement>> CreateRange(List<Appointement> newAppointement);
        Task Update(Appointement AppointementToBeUpdated, Appointement Appointement);
        Task Delete(Appointement AppointementToBeDeleted);
        Task DeleteRange(List<Appointement> Appointement);

        Task<IEnumerable<Appointement>> GetAll();
        Task<IEnumerable<Appointement>> GetAllActif();
        Task<IEnumerable<Appointement>> GetAllInActif();
        
        Task Approuve(Appointement AppointementToBeUpdated, Appointement Appointement);
        Task Reject(Appointement AppointementToBeUpdated, Appointement Appointement);
 

    }
}
