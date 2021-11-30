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

    public class ProductSellingObjectivesController : ControllerBase
    {
        public IList<ProductSellingObjectives> ProductSellingObjectivess;

        private readonly IProductSellingObjectivesService _ProductSellingObjectivesService;

        private readonly IMapper _mapperService;
        public ProductSellingObjectivesController(IProductSellingObjectivesService ProductSellingObjectivesService, IMapper mapper)
        {
            _ProductSellingObjectivesService = ProductSellingObjectivesService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ProductSellingObjectivesResource>> CreateProductSellingObjectives(SaveProductSellingObjectivesResource SaveProductSellingObjectivesResource)
  {     
            //*** Mappage ***
            var ProductSellingObjectives = _mapperService.Map<SaveProductSellingObjectivesResource, ProductSellingObjectives>(SaveProductSellingObjectivesResource);
            ProductSellingObjectives.CreatedOn = DateTime.UtcNow;
            ProductSellingObjectives.UpdatedOn = DateTime.UtcNow;
            ProductSellingObjectives.Active = 0;
            ProductSellingObjectives.Version = 0;
            ProductSellingObjectives.CreatedBy = 0;
            ProductSellingObjectives.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewProductSellingObjectives = await _ProductSellingObjectivesService.Create(ProductSellingObjectives);
            //*** Mappage ***
            var ProductSellingObjectivesResource = _mapperService.Map<ProductSellingObjectives, ProductSellingObjectivesResource>(NewProductSellingObjectives);
            return Ok(ProductSellingObjectivesResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<ProductSellingObjectivesResource>> GetAllProductSellingObjectivess()
        {
            try
            {
                var Employe = await _ProductSellingObjectivesService.GetAll();
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
        public async Task<ActionResult<ProductSellingObjectivesResource>> GetAllActifProductSellingObjectivess()
        {
            try
            {
                var Employe = await _ProductSellingObjectivesService.GetAllActif();
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
        public async Task<ActionResult<ProductSellingObjectivesResource>> GetAllInactifProductSellingObjectivess()
        {
            try
            {
                var Employe = await _ProductSellingObjectivesService.GetAllInActif();
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
        public async Task<ActionResult<ProductSellingObjectivesResource>> GetProductSellingObjectivesById(int Id)
        {
            try
            {
                var ProductSellingObjectivess = await _ProductSellingObjectivesService.GetById(Id);
                if (ProductSellingObjectivess == null) return NotFound();
                var ProductSellingObjectivesRessource = _mapperService.Map<ProductSellingObjectives, ProductSellingObjectivesResource>(ProductSellingObjectivess);
                return Ok(ProductSellingObjectivesRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductSellingObjectivesResource>> UpdateProductSellingObjectives(int Id, SaveProductSellingObjectivesResource SaveProductSellingObjectivesResource)
        {

            var ProductSellingObjectivesToBeModified = await _ProductSellingObjectivesService.GetById(Id);
            if (ProductSellingObjectivesToBeModified == null) return BadRequest("Le ProductSellingObjectives n'existe pas"); //NotFound();
            var ProductSellingObjectivess = _mapperService.Map<SaveProductSellingObjectivesResource, ProductSellingObjectives>(SaveProductSellingObjectivesResource);
            //var newProductSellingObjectives = await _ProductSellingObjectivesService.Create(ProductSellingObjectivess);

            await _ProductSellingObjectivesService.Update(ProductSellingObjectivesToBeModified, ProductSellingObjectivess);

            var ProductSellingObjectivesUpdated = await _ProductSellingObjectivesService.GetById(Id);

            var ProductSellingObjectivesResourceUpdated = _mapperService.Map<ProductSellingObjectives, ProductSellingObjectivesResource>(ProductSellingObjectivesUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductSellingObjectives(int Id)
        {
            try
            {

                var sub = await _ProductSellingObjectivesService.GetById(Id);
                if (sub == null) return BadRequest("Le ProductSellingObjectives  n'existe pas"); //NotFound();
                await _ProductSellingObjectivesService.Delete(sub);
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
                List<ProductSellingObjectives> empty = new List<ProductSellingObjectives>();
                foreach (var item in Ids)
                {
                    var sub = await _ProductSellingObjectivesService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le ProductSellingObjectives  n'existe pas"); //NotFound();

                }
                await _ProductSellingObjectivesService.DeleteRange(empty);
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
