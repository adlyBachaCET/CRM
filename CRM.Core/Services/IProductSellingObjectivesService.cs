using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IProductSellingObjectivesService
    {
        Task<ProductSellingObjectives> GetById(int? id);
        Task<ProductSellingObjectives> Create(ProductSellingObjectives newProductSellingObjectives);
        Task<List<ProductSellingObjectives>> CreateRange(List<ProductSellingObjectives> newProductSellingObjectives);
        Task Update(ProductSellingObjectives ProductSellingObjectivesToBeUpdated, ProductSellingObjectives ProductSellingObjectives);
        Task Delete(ProductSellingObjectives ProductSellingObjectivesToBeDeleted);
        Task DeleteRange(List<ProductSellingObjectives> ProductSellingObjectives);
        Task<IEnumerable<ProductSellingObjectives>> GetByIdPharmacy(int id);
        Task<List<ProductSellingObjectives>> GetByIdUser(int id);


        Task<IEnumerable<ProductSellingObjectives>> GetAll();
        Task<IEnumerable<ProductSellingObjectives>> GetAllActif();
        Task<IEnumerable<ProductSellingObjectives>> GetAllInActif();


    }
}
