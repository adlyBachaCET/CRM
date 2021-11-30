using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ProductSellingObjectivesService : IProductSellingObjectivesService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ProductSellingObjectivesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductSellingObjectives> Create(ProductSellingObjectives newProductSellingObjectives)
        {

            await _unitOfWork.ProductSellingObjectivess.Add(newProductSellingObjectives);
            await _unitOfWork.CommitAsync();
            return newProductSellingObjectives;
        }
        public async Task<List<ProductSellingObjectives>> CreateRange(List<ProductSellingObjectives> newProductSellingObjectives)
        {

            await _unitOfWork.ProductSellingObjectivess.AddRange(newProductSellingObjectives);
            await _unitOfWork.CommitAsync();
            return newProductSellingObjectives;
        }
        public async Task<IEnumerable<ProductSellingObjectives>> GetAll()
        {
            return
                           await _unitOfWork.ProductSellingObjectivess.GetAll();
        }

       /* public async Task Delete(ProductSellingObjectives ProductSellingObjectives)
        {
            _unitOfWork.ProductSellingObjectivess.Remove(ProductSellingObjectives);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<ProductSellingObjectives>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.ProductSellingObjectivess
        //          .GetAllWithArtisteAsync();
        //}

  
        public async Task<List<ProductSellingObjectives>> GetByIdUser(int id)
        {
    
            
            return null;
              
        }
        public async Task Update(ProductSellingObjectives ProductSellingObjectivesToBeUpdated, ProductSellingObjectives ProductSellingObjectives)
        {
            ProductSellingObjectivesToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ProductSellingObjectives ProductSellingObjectives)
        {
            //ProductSellingObjectives musi =  _unitOfWork.ProductSellingObjectivess.SingleOrDefaultAsync(x=>x.Id == ProductSellingObjectivesToBeUpdated.Id);
            ProductSellingObjectives.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ProductSellingObjectives> ProductSellingObjectives)
        {
            foreach (var item in ProductSellingObjectives)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllActif()
        {
            return
                             await _unitOfWork.ProductSellingObjectivess.GetAllActif();
        }

        public async Task<IEnumerable<ProductSellingObjectives>> GetAllInActif()
        {
            return
                             await _unitOfWork.ProductSellingObjectivess.GetAllInActif();
        }

     
        public async Task<IEnumerable<ProductSellingObjectives>> GetByIdPharmacy(int id)
        {
            return
                            null;
        }

        public Task<ProductSellingObjectives> GetById(int? id)
        {
            throw new System.NotImplementedException();
        }

        //public Task<ProductSellingObjectives> CreateProductSellingObjectives(ProductSellingObjectives newProductSellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteProductSellingObjectives(ProductSellingObjectives ProductSellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ProductSellingObjectives> GetProductSellingObjectivesById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ProductSellingObjectives>> GetProductSellingObjectivessByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateProductSellingObjectives(ProductSellingObjectives ProductSellingObjectivesToBeUpdated, ProductSellingObjectives ProductSellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
