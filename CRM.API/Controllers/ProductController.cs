using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ProductController : ControllerBase
    {

        private readonly IProductService _ProductService;
        private readonly IUserService _UserService;
        private readonly IBusinessUnitService _BuService;


        private readonly IMapper _mapperService;
        public ProductController(
            IUserService UserService, IBusinessUnitService BuService,          IProductService ProductService, IMapper mapper)
        {
            _UserService = UserService;
            _BuService = BuService;
           _ProductService = ProductService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ProductResource>> CreateProduct([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, SaveProductResource SaveProductResource)
  {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            //*** Mappage ***
            var Product = _mapperService.Map<SaveProductResource, Product>(SaveProductResource);
            Product.UpdatedOn = DateTime.UtcNow;
            Product.CreatedOn = DateTime.UtcNow;
            Product.Active = 0;
            Product.Status = 0;
            Product.UpdatedBy = IdUser;
            Product.CreatedBy = IdUser;
            var Bu = await _BuService.GetById(SaveProductResource.IdBu);

            if (Bu != null)
            {
                Product.IdBu = Bu.IdBu;
                Product.Bu = Bu;
                Product.VersionBu = Bu.Version;
                Product.StatusBu = Bu.Status;

            }
            else
            {
                Product.IdBu = null;
                Product.Bu = null;
                Product.VersionBu = null;
                Product.StatusBu = null;

            }
            var NewProduct = await _ProductService.Create(Product);
     
            //  *** Mappage ***
            var ProductResource = _mapperService.Map<Product, ProductResource>(NewProduct);
            return Ok(ProductResource);

        }
        [HttpGet]
        public async Task<ActionResult<ProductResource>> GetAllProducts([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Products = await _ProductService.GetAll();
                List<ProductResource> ProductResources = new List<ProductResource>();
                foreach (var item in Products)
                {
                   var ProductResource = _mapperService.Map<Product, ProductResource>(item);
                    var Bu = await _BuService.GetById(ProductResource.IdBu);
                    var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                    ProductResource.Bu = BusinessUnit;
                    ProductResources.Add(ProductResource);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(ProductResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<ProductResource>> GetAllActifProducts([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Products = await _ProductService.GetAllActif();
                List<ProductResource> ProductResources = new List<ProductResource>();
                foreach (var item in Products)
                {
                    var ProductResource = _mapperService.Map<Product, ProductResource>(item);
                    var Bu = await _BuService.GetById(ProductResource.IdBu);
                    var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                    ProductResource.Bu = BusinessUnit;
                    ProductResources.Add(ProductResource);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(ProductResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<ProductResource>> GetAllInactifProducts([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Products = await _ProductService.GetAllInActif();
                List<ProductResource> ProductResources = new List<ProductResource>();
                foreach (var item in Products)
                {
                    var ProductResource = _mapperService.Map<Product, ProductResource>(item);
                    var Bu = await _BuService.GetById(ProductResource.IdBu);
                    var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                    ProductResource.Bu = BusinessUnit;
                    ProductResources.Add(ProductResource);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(ProductResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SellingObjectives/{Id}")]
        public async Task<ActionResult<List<SellingObjectivesResource>>> GetSellingObjectivesByIdProduct([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token,int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var SellingObjectives = await _ProductService.GetSellingObjectivesByIdProduct(Id);
                List<SellingObjectivesResource> SellingObjectivesResources = new List<SellingObjectivesResource>();
                foreach (var item in SellingObjectives)
                {
                    var SellingObjectivesResource = _mapperService.Map<SellingObjectives, SellingObjectivesResource>(item);
                  //  var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                   //SellingObjectivesResource.Bu = BusinessUnit;
                    SellingObjectivesResources.Add(SellingObjectivesResource);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(SellingObjectivesResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductResource>> GetProductById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Products = await _ProductService.GetById(Id);
                if (Products == null) return NotFound();
                var ProductRessource = _mapperService.Map<Product, ProductResource>(Products);
                var Bu = await _BuService.GetById(ProductRessource.IdBu);
                var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                ProductRessource.Bu = BusinessUnit;
                var SellingObjectives = await _ProductService.GetSellingObjectivesByIdProduct(Id);
                List<SellingObjectivesResource> SellingObjectivesResources = new List<SellingObjectivesResource>();
                foreach (var item in SellingObjectives)
                {
                    var SellingObjectivesResource = _mapperService.Map<SellingObjectives, SellingObjectivesResource>(item);
                    //  var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                    //SellingObjectivesResource.Bu = BusinessUnit;
                    SellingObjectivesResources.Add(SellingObjectivesResource);
                }
                return Ok(new { ProductRessource = ProductRessource , SellingObjectivesResources = SellingObjectivesResources });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByBu")]
        public async Task<ActionResult<ProductResource>> GetProductsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                List<ProductResource> ProductResources = new List<ProductResource>();
                var Products = await _ProductService.GetByIdUser(IdUser);
                if (Products == null) return NotFound();
                foreach(var item in Products) { 
                var ProductRessource = _mapperService.Map<Product, ProductResource>(item);
                    var Bu = await _BuService.GetById(ProductRessource.IdBu);
                    var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);

                    ProductRessource.Bu = BusinessUnit;
                    ProductResources.Add(ProductRessource);
                }
                return Ok(ProductResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductResource>> UpdateProduct([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, SaveProductResource SaveProductResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var ProductToBeModified = await _ProductService.GetById(Id);
            if (ProductToBeModified == null) return BadRequest("Le Product n'existe pas"); //NotFound();
            var Products = _mapperService.Map<SaveProductResource, Product>(SaveProductResource);
            Products.UpdatedOn = DateTime.UtcNow;
            Products.CreatedOn = DateTime.UtcNow;
            Products.Active = 0;
            Products.Status = 0;
            Products.UpdatedBy = IdUser;
            Products.CreatedBy = Products.CreatedBy;
            //var newProduct = await _ProductService.Create(Products);
            var Bu = await _BuService.GetById(SaveProductResource.IdBu);

            if (Bu.IdBu != ProductToBeModified.IdBu)
            {
                Products.IdBu = Bu.IdBu;
                Products.Bu = Bu;
                Products.VersionBu = Bu.Version;
                Products.StatusBu = Bu.Status;

            }
            else
            {
                Products.IdBu = ProductToBeModified.IdBu;
                Products.Bu = ProductToBeModified.Bu;
                Products.VersionBu = ProductToBeModified.VersionBu;
                Products.StatusBu = ProductToBeModified.StatusBu;

            }
            await _ProductService.Update(ProductToBeModified, Products);

            var ProductUpdated = await _ProductService.GetById(Id);

            var ProductResourceUpdated = _mapperService.Map<Product, ProductResource>(ProductUpdated);
            var BuGot = await _BuService.GetById(ProductResourceUpdated.IdBu);
            var BusinessUnit = _mapperService.Map<BusinessUnit, BusinessUnitResource>(BuGot);

            ProductResourceUpdated.Bu = BusinessUnit;
            return Ok(ProductResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProduct([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
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
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, List<int> Ids)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
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
