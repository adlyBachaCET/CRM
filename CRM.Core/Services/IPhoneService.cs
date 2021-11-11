using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPhoneService
    {
        Task<Phone> GetById(int id);
        Task<IEnumerable<Phone>> GetByIdDoctor(int id);
        Task<IEnumerable<Phone>> GetByIdPharmacy(int id);
        Task<IEnumerable<Phone>> GetByIdWholeSaler(int id);
        Task<Phone> GetByNumber(int Num);
        Task<Phone> Create(Phone newPhone);
        Task<List<Phone>> CreateRange(List<Phone> newPhone);
        Task Update(Phone PhoneToBeUpdated, Phone Phone);
        Task Delete(Phone PhoneToBeDeleted);
        Task DeleteRange(List<Phone> Phone);

        Task<IEnumerable<Phone>> GetAll();
        Task<IEnumerable<Phone>> GetAllActif();
        Task<IEnumerable<Phone>> GetAllInActif();

    }
}
