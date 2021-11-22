using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IProductSampleService
    {
        Task<ProductSample> GetById(int? id);
        Task<ProductSample> Create(ProductSample newProductSample);
        Task<List<ProductSample>> CreateRange(List<ProductSample> newProductSample);
        Task Update(ProductSample ProductSampleToBeUpdated, ProductSample ProductSample);
        Task Delete(ProductSample ProductSampleToBeDeleted);
        Task DeleteRange(List<ProductSample> ProductSample);
        Task<IEnumerable<ProductSample>> GetByIdPharmacy(int id);
        
    
        Task<IEnumerable<ProductSample>> GetAll();
        Task<IEnumerable<ProductSample>> GetAllActif();
        Task<IEnumerable<ProductSample>> GetAllInActif();


    }
}
