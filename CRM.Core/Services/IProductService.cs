using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IProductService
    {
        Task<Product> GetById(int? id);
        Task<Product> Create(Product newProduct);
        Task<List<Product>> CreateRange(List<Product> newProduct);
        Task Update(Product ProductToBeUpdated, Product Product);
        Task Delete(Product ProductToBeDeleted);
        Task DeleteRange(List<Product> Product);
        Task<IEnumerable<Product>> GetByIdPharmacy(int id);
        Task<List<Product>> GetByIdUser(int id);
        Task<List<SellingObjectives>> GetSellingObjectivesByIdProduct(int? id);

        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAllActif();
        Task<IEnumerable<Product>> GetAllInActif();


    }
}
