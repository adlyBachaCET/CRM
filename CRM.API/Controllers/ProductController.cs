using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ProductController : ControllerBase
    {
        public IList<Product> Products;

        private readonly IProductService _ProductService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;
        private readonly IProductSampleService _ProductSampleService;

        private readonly IRequestRpService _RequestRpService;

        private readonly IMapper _mapperService;
        public ProductController(IRequestRpService RequestRpService,
            IUserService UserService, IProductSampleService ProductSampleService, IPharmacyService PharmacyService,
            IDoctorService DoctorService,IProductService ProductService, IMapper mapper)
        {
            _RequestRpService = RequestRpService;
            _UserService = UserService;
            _ProductSampleService = ProductSampleService;
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
           _ProductService = ProductService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ProductResource>> CreateProduct(SaveProductResource SaveProductResource)
  {
            //*** Mappage ***
            var Product = _mapperService.Map<SaveProductResource, Product>(SaveProductResource);
            Product.UpdatedOn = DateTime.UtcNow;
            Product.CreatedOn = DateTime.UtcNow;
            Product.Active = 0;
            Product.Status = 0;
            Product.UpdatedBy = 0;
            Product.CreatedBy = 0;
            var ProductSample = await _ProductSampleService.GetById(SaveProductResource.IdProductSample);

            if (ProductSample != null)
            {
                Product.IdProductSample = ProductSample.IdProductSample;
                Product.ProductSample = ProductSample;
                Product.VersionProductSample = ProductSample.Version;
                Product.StatusProductSample = ProductSample.Status;

            }
            else
            {
                Product.IdProductSample = null;
                Product.ProductSample = null;
                Product.VersionProductSample = null;
                Product.StatusProductSample = null;

            }
            var NewProduct = await _ProductService.Create(Product);
     
            //  *** Mappage ***
            var ProductResource = _mapperService.Map<Product, ProductResource>(NewProduct);
            return Ok(ProductResource);

        }
        [HttpGet]
        public async Task<ActionResult<ProductResource>> GetAllProducts()
        {
            try
            {
                var Employe = await _ProductService.GetAll();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<ProductResource>> GetAllActifProducts()
        {
            try
            {
                var Employe = await _ProductService.GetAllActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<ProductResource>> GetAllInactifProducts()
        {
            try
            {
                var Employe = await _ProductService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductResource>> GetProductById(int Id)
        {
            try
            {
                var Products = await _ProductService.GetById(Id);
                if (Products == null) return NotFound();
                var ProductRessource = _mapperService.Map<Product, ProductResource>(Products);
                return Ok(ProductRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductResource>> UpdateProduct(int Id, SaveProductResource SaveProductResource)
        {

            var ProductToBeModified = await _ProductService.GetById(Id);
            if (ProductToBeModified == null) return BadRequest("Le Product n'existe pas"); //NotFound();
            var Products = _mapperService.Map<SaveProductResource, Product>(SaveProductResource);
            Products.UpdatedOn = DateTime.UtcNow;
            Products.CreatedOn = DateTime.UtcNow;
            Products.Active = 0;
            Products.Status = 0;
            Products.UpdatedBy = 0;
            Products.CreatedBy = Products.CreatedBy;
            //var newProduct = await _ProductService.Create(Products);
            var ProductSample = await _ProductSampleService.GetById(SaveProductResource.IdProductSample);

            if (ProductSample.IdProductSample != ProductToBeModified.IdProductSample)
            {
                Products.IdProductSample = ProductSample.IdProductSample;
                Products.ProductSample = ProductSample;
                Products.VersionProductSample = ProductSample.Version;
                Products.StatusProductSample = ProductSample.Status;

            }
            else
            {
                Products.IdProductSample = ProductToBeModified.IdProductSample;
                Products.ProductSample = ProductToBeModified.ProductSample;
                Products.VersionProductSample = ProductToBeModified.VersionProductSample;
                Products.StatusProductSample = ProductToBeModified.StatusProductSample;

            }
            await _ProductService.Update(ProductToBeModified, Products);

            var ProductUpdated = await _ProductService.GetById(Id);

            var ProductResourceUpdated = _mapperService.Map<Product, ProductResource>(ProductUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            try
            {

                var sub = await _ProductService.GetById(Id);
                if (sub == null) return BadRequest("Le Product  n'existe pas"); //NotFound();
                await _ProductService.Delete(sub);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange(List<int> Ids)
        {
            try
            {
                List<Product> empty = new List<Product>();
                foreach (var item in Ids)
                {
                    var sub = await _ProductService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Product  n'existe pas"); //NotFound();

                }
                await _ProductService.DeleteRange(empty);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
