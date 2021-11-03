using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IBuDoctorService
    {
        Task<BuDoctor> GetById(int id);
        Task<BuDoctor> Create(BuDoctor newBuDoctor);
        Task<List<BuDoctor>> CreateRange(List<BuDoctor> newBuDoctor);
        Task Delete(BuDoctor BuDoctorToBeDeleted);
        Task DeleteRange(List<BuDoctor> BuDoctor);

        Task<IEnumerable<BuDoctor>> GetAll();
        Task<IEnumerable<BuDoctor>> GetAllActif();
        Task<IEnumerable<BuDoctor>> GetAllInActif();

    }
}
