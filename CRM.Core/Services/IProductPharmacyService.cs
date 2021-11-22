using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IProductPharmacyService
    {
        Task<ProductPharmacy> GetById(int id);
        Task<ProductPharmacy> Create(ProductPharmacy newProductPharmacy);
        Task<List<ProductPharmacy>> CreateRange(List<ProductPharmacy> newProductPharmacy);
        Task Update(ProductPharmacy ProductPharmacyToBeUpdated, ProductPharmacy ProductPharmacy);
        Task Delete(ProductPharmacy ProductPharmacyToBeDeleted);
        Task DeleteRange(List<ProductPharmacy> ProductPharmacy);
        Task<IEnumerable<ProductPharmacy>> GetByIdPharmacy(int id);
        
    
        Task<IEnumerable<ProductPharmacy>> GetAll();
        Task<IEnumerable<ProductPharmacy>> GetAllActif();
        Task<IEnumerable<ProductPharmacy>> GetAllInActif();


    }
}
