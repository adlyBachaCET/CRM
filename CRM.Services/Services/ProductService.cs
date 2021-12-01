using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ProductService : IProductService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Create(Product newProduct)
        {

            await _unitOfWork.Products.Add(newProduct);
            await _unitOfWork.CommitAsync();
            return newProduct;
        }
        public async Task<List<Product>> CreateRange(List<Product> newProduct)
        {

            await _unitOfWork.Products.AddRange(newProduct);
            await _unitOfWork.CommitAsync();
            return newProduct;
        }
        public async Task<List<SellingObjectives>> GetSellingObjectivesByIdProduct(int? id)
        {
            List<SellingObjectives> SellingObjectives = new List<SellingObjectives>();
            var SOP = await _unitOfWork.SellingObjectivess.Find(i => i.IdProduct == id && i.Active == 0);
            foreach (var item in SOP)
            {
                var SellingObjective = await _unitOfWork.SellingObjectivess.SingleOrDefault(i => i.IdSellingObjectives == item.IdSellingObjectives);
                SellingObjectives.Add(SellingObjective);
            }
            return SellingObjectives;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return
                           await _unitOfWork.Products.GetAll();
        }

       /* public async Task Delete(Product Product)
        {
            _unitOfWork.Products.Remove(Product);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Product>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Products
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Product> GetById(int? id)
        {
             return
               await _unitOfWork.Products.SingleOrDefault(i=>i.IdProduct==id&& i.Active==0);
        }
        public async Task<List<Product>> GetByIdUser(int id)
        {
            BusinessUnit BusinessUnit = new BusinessUnit();
            var buUser = await _unitOfWork.BuUsers.SingleOrDefault(i=>i.IdUser==id);
            List<Product> products = new List<Product>();
            var list  =await _unitOfWork.Products.Find(i => i.IdBu == buUser.IdBu && i.Active == 0);
           foreach(var item in list)
            {
                products.Add(item);
            }
            return products;
              
        }
        public async Task Update(Product ProductToBeUpdated, Product Product)
        {
            ProductToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Product.Version = ProductToBeUpdated.Version + 1;
            Product.IdProduct = ProductToBeUpdated.IdProduct;
            Product.Status = Status.Pending;
            Product.Active = 0;

            await _unitOfWork.Products.Add(Product);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Product Product)
        {
            //Product musi =  _unitOfWork.Products.SingleOrDefaultAsync(x=>x.Id == ProductToBeUpdated.Id);
            Product.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Product> Product)
        {
            foreach (var item in Product)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllActif()
        {
            return
                             await _unitOfWork.Products.GetAllActif();
        }

        public async Task<IEnumerable<Product>> GetAllInActif()
        {
            return
                             await _unitOfWork.Products.GetAllInActif();
        }

     
        public async Task<IEnumerable<Product>> GetByIdPharmacy(int id)
        {
            return
                            await _unitOfWork.Products.GetByIdPharmacy(id);
        }
        
        //public Task<Product> CreateProduct(Product newProduct)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteProduct(Product Product)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Product> GetProductById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Product>> GetProductsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateProduct(Product ProductToBeUpdated, Product Product)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
