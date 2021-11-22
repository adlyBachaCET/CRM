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

    public class ProductSampleController : ControllerBase
    {
        public IList<ProductSample> ProductSamples;

        private readonly IProductSampleService _ProductSampleService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IRequestRpService _RequestRpService;

        private readonly IMapper _mapperService;
        public ProductSampleController(IRequestRpService RequestRpService,
            IUserService UserService, IPharmacyService PharmacyService,
            IDoctorService DoctorService,IProductSampleService ProductSampleService, IMapper mapper)
        {
            _RequestRpService = RequestRpService;
            _UserService = UserService;
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
           _ProductSampleService = ProductSampleService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ProductSampleResource>> CreateProductSample(SaveProductSampleResource SaveProductSampleResource)
  {
              //*** Mappage ***
               var ProductSample = _mapperService.Map<SaveProductSampleResource, ProductSample>(SaveProductSampleResource);
               ProductSample.UpdatedOn = DateTime.UtcNow;
               ProductSample.CreatedOn = DateTime.UtcNow;
               ProductSample.Active = 0;
               ProductSample.Status = 0;
               ProductSample.UpdatedBy = 0;
               ProductSample.CreatedBy = 0;
            var NewProductSample = await _ProductSampleService.Create(ProductSample);

            //  *** Mappage ***
           var ProductSampleResource = _mapperService.Map<ProductSample, ProductSampleResource>(NewProductSample);
            return Ok(ProductSampleResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<ProductSampleResource>> GetAllProductSamples()
        {
            try
            {
                var Employe = await _ProductSampleService.GetAll();
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
        public async Task<ActionResult<ProductSampleResource>> GetAllActifProductSamples()
        {
            try
            {
                var Employe = await _ProductSampleService.GetAllActif();
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
        public async Task<ActionResult<ProductSampleResource>> GetAllInactifProductSamples()
        {
            try
            {
                var Employe = await _ProductSampleService.GetAllInActif();
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
        public async Task<ActionResult<ProductSampleResource>> GetProductSampleById(int Id)
        {
            try
            {
                var ProductSamples = await _ProductSampleService.GetById(Id);
                if (ProductSamples == null) return NotFound();
                var ProductSampleRessource = _mapperService.Map<ProductSample, ProductSampleResource>(ProductSamples);
                return Ok(ProductSampleRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductSampleResource>> UpdateProductSample(int Id, SaveProductSampleResource SaveProductSampleResource)
        {

            var ProductSampleToBeModified = await _ProductSampleService.GetById(Id);
            if (ProductSampleToBeModified == null) return BadRequest("Le ProductSample n'existe pas"); //NotFound();
            var ProductSamples = _mapperService.Map<SaveProductSampleResource, ProductSample>(SaveProductSampleResource);
            //var newProductSample = await _ProductSampleService.Create(ProductSamples);
            ProductSamples.UpdatedOn = DateTime.UtcNow;
            ProductSamples.CreatedOn = ProductSampleToBeModified.CreatedOn;
            ProductSamples.Active = 0;
            ProductSamples.Status = 0;
            ProductSamples.UpdatedBy = 0;
            ProductSamples.CreatedBy = ProductSampleToBeModified.CreatedBy;
            await _ProductSampleService.Update(ProductSampleToBeModified, ProductSamples);

            var ProductSampleUpdated = await _ProductSampleService.GetById(Id);

            var ProductSampleResourceUpdated = _mapperService.Map<ProductSample, ProductSampleResource>(ProductSampleUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductSample(int Id)
        {
            try
            {

                var sub = await _ProductSampleService.GetById(Id);
                if (sub == null) return BadRequest("Le ProductSample  n'existe pas"); //NotFound();
                await _ProductSampleService.Delete(sub);
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
                List<ProductSample> empty = new List<ProductSample>();
                foreach (var item in Ids)
                {
                    var sub = await _ProductSampleService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le ProductSample  n'existe pas"); //NotFound();

                }
                await _ProductSampleService.DeleteRange(empty);
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
