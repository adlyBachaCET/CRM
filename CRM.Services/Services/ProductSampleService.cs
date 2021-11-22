using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ProductSampleService : IProductSampleService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ProductSampleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductSample> Create(ProductSample newProductSample)
        {

            await _unitOfWork.ProductSamples.Add(newProductSample);
            await _unitOfWork.CommitAsync();
            return newProductSample;
        }
        public async Task<List<ProductSample>> CreateRange(List<ProductSample> newProductSample)
        {

            await _unitOfWork.ProductSamples.AddRange(newProductSample);
            await _unitOfWork.CommitAsync();
            return newProductSample;
        }
        public async Task<IEnumerable<ProductSample>> GetAll()
        {
            return
                           await _unitOfWork.ProductSamples.GetAll();
        }

       /* public async Task Delete(ProductSample ProductSample)
        {
            _unitOfWork.ProductSamples.Remove(ProductSample);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<ProductSample>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.ProductSamples
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<ProductSample> GetById(int? id)
        {
            return
              await _unitOfWork.ProductSamples.SingleOrDefault(i=>i.IdProductSample==id&& i.Active==0);
        }
   
        public async Task Update(ProductSample ProductSampleToBeUpdated, ProductSample ProductSample)
        {
            ProductSampleToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ProductSample.Version = ProductSampleToBeUpdated.Version + 1;
            ProductSample.IdProductSample = ProductSampleToBeUpdated.IdProductSample;
            ProductSample.Status = Status.Pending;
            ProductSample.Active = 0;

            await _unitOfWork.ProductSamples.Add(ProductSample);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ProductSample ProductSample)
        {
            //ProductSample musi =  _unitOfWork.ProductSamples.SingleOrDefaultAsync(x=>x.Id == ProductSampleToBeUpdated.Id);
            ProductSample.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ProductSample> ProductSample)
        {
            foreach (var item in ProductSample)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ProductSample>> GetAllActif()
        {
            return
                             await _unitOfWork.ProductSamples.GetAllActif();
        }

        public async Task<IEnumerable<ProductSample>> GetAllInActif()
        {
            return
                             await _unitOfWork.ProductSamples.GetAllInActif();
        }

     
        public async Task<IEnumerable<ProductSample>> GetByIdPharmacy(int id)
        {
            return
                            await _unitOfWork.ProductSamples.GetByIdPharmacy(id);
        }
        
        //public Task<ProductSample> CreateProductSample(ProductSample newProductSample)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteProductSample(ProductSample ProductSample)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ProductSample> GetProductSampleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ProductSample>> GetProductSamplesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateProductSample(ProductSample ProductSampleToBeUpdated, ProductSample ProductSample)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
