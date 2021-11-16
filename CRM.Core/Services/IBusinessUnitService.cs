using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IBusinessUnitService
    {
        Task<BusinessUnit> GetById(int id);
        Task<BusinessUnit> Create(BusinessUnit newBusinessUnit);
        Task<List<BusinessUnit>> CreateRange(List<BusinessUnit> newBusinessUnit);
        Task Update(BusinessUnit BusinessUnitToBeUpdated, BusinessUnit BusinessUnit);
        Task Delete(BusinessUnit BusinessUnitToBeDeleted);
        Task DeleteRange(List<BusinessUnit> BusinessUnit);
        Task<BusinessUnit> GetByNames(string Names);

        Task<IEnumerable<BusinessUnit>> GetAll();
        Task<IEnumerable<BusinessUnit>> GetAllActif();
        Task<IEnumerable<BusinessUnit>> GetAllInActif();

    }
}
