using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ProductPharmacyService : IProductPharmacyService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ProductPharmacyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductPharmacy> Create(ProductPharmacy newProductPharmacy)
        {

            await _unitOfWork.ProductPharmacys.Add(newProductPharmacy);
            await _unitOfWork.CommitAsync();
            return newProductPharmacy;
        }
        public async Task<List<ProductPharmacy>> CreateRange(List<ProductPharmacy> newProductPharmacy)
        {

            await _unitOfWork.ProductPharmacys.AddRange(newProductPharmacy);
            await _unitOfWork.CommitAsync();
            return newProductPharmacy;
        }
        public async Task<IEnumerable<ProductPharmacy>> GetAll()
        {
            return
                           await _unitOfWork.ProductPharmacys.GetAll();
        }

       /* public async Task Delete(ProductPharmacy ProductPharmacy)
        {
            _unitOfWork.ProductPharmacys.Remove(ProductPharmacy);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<ProductPharmacy>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.ProductPharmacys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<ProductPharmacy> GetById(int id)
        {
             return
               await _unitOfWork.ProductPharmacys.SingleOrDefault(i => i.IdPharmacy == id && i.Active == 0);
        }
   
        public async Task Update(ProductPharmacy ProductPharmacyToBeUpdated, ProductPharmacy ProductPharmacy)
        {
            ProductPharmacyToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ProductPharmacy.Version = ProductPharmacyToBeUpdated.Version + 1;
            ProductPharmacy.IdPharmacy = ProductPharmacyToBeUpdated.IdPharmacy;
            ProductPharmacy.Status = Status.Pending;
            ProductPharmacy.Active = 0;

            await _unitOfWork.ProductPharmacys.Add(ProductPharmacy);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ProductPharmacy ProductPharmacy)
        {
            //ProductPharmacy musi =  _unitOfWork.ProductPharmacys.SingleOrDefaultAsync(x=>x.Id == ProductPharmacyToBeUpdated.Id);
            ProductPharmacy.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ProductPharmacy> ProductPharmacy)
        {
            foreach (var item in ProductPharmacy)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllActif()
        {
            return
                             await _unitOfWork.ProductPharmacys.GetAllActif();
        }

        public async Task<IEnumerable<ProductPharmacy>> GetAllInActif()
        {
            return
                             await _unitOfWork.ProductPharmacys.GetAllInActif();
        }

     
        public async Task<IEnumerable<ProductPharmacy>> GetByIdPharmacy(int id)
        {
            return
                            await _unitOfWork.ProductPharmacys.GetByIdPharmacy(id);
        }
        
        //public Task<ProductPharmacy> CreateProductPharmacy(ProductPharmacy newProductPharmacy)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteProductPharmacy(ProductPharmacy ProductPharmacy)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ProductPharmacy> GetProductPharmacyById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ProductPharmacy>> GetProductPharmacysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateProductPharmacy(ProductPharmacy ProductPharmacyToBeUpdated, ProductPharmacy ProductPharmacy)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
