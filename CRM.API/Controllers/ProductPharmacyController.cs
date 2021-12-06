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

    public class ProductPharmacyController : ControllerBase
    {
        public IList<ProductPharmacy> ProductPharmacys;

        private readonly IProductPharmacyService _ProductPharmacyService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IRequestRpService _RequestRpService;

        private readonly IMapper _mapperService;
        public ProductPharmacyController(IRequestRpService RequestRpService,
            IUserService UserService, IPharmacyService PharmacyService,
            IDoctorService DoctorService,IProductPharmacyService ProductPharmacyService, IMapper mapper)
        {
            _RequestRpService = RequestRpService;
            _UserService = UserService;
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
           _ProductPharmacyService = ProductPharmacyService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ProductPharmacyResource>> CreateProductPharmacy(SaveProductPharmacyResource SaveProductPharmacyResource)
  {
            var ProductPharmacy = _mapperService.Map<SaveProductPharmacyResource, ProductPharmacy>(SaveProductPharmacyResource);
            ProductPharmacy.UpdatedOn = DateTime.UtcNow;
            ProductPharmacy.CreatedOn = DateTime.UtcNow;
            ProductPharmacy.Active = 0;
            ProductPharmacy.Status = 0;
            ProductPharmacy.UpdatedBy = 0;
            ProductPharmacy.CreatedBy = 0;
            var Pharmacy = await _PharmacyService.GetById(SaveProductPharmacyResource.IdPharmacy);

            if (Pharmacy != null)
            {
                ProductPharmacy.Name = Pharmacy.Name;
                ProductPharmacy.Pharmacy = Pharmacy;
                ProductPharmacy.VersionPharmacy = Pharmacy.Version;
                ProductPharmacy.StatusPharmacy = Pharmacy.Status;
       
            }

            var NewProductPharmacy = await _ProductPharmacyService.Create(ProductPharmacy);

            //  *** Mappage ***
             var ProductPharmacyResource = _mapperService.Map<ProductPharmacy, ProductPharmacyResource>(NewProductPharmacy);
            return Ok(ProductPharmacyResource);

        }
        [HttpGet]
        public async Task<ActionResult<ProductPharmacyResource>> GetAllProductPharmacys()
        {
            try
            {
                var Employe = await _ProductPharmacyService.GetAll();
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
        public async Task<ActionResult<ProductPharmacyResource>> GetAllActifProductPharmacys()
        {
            try
            {
                var Employe = await _ProductPharmacyService.GetAllActif();
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
        public async Task<ActionResult<ProductPharmacyResource>> GetAllInactifProductPharmacys()
        {
            try
            {
                var Employe = await _ProductPharmacyService.GetAllInActif();
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
        public async Task<ActionResult<ProductPharmacyResource>> GetProductPharmacyById(int Id)
        {
            try
            {
                var ProductPharmacys = await _ProductPharmacyService.GetById(Id);
                if (ProductPharmacys == null) return NotFound();
                var ProductPharmacyRessource = _mapperService.Map<ProductPharmacy, ProductPharmacyResource>(ProductPharmacys);
                return Ok(ProductPharmacyRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductPharmacyResource>> UpdateProductPharmacy(int Id, SaveProductPharmacyResource SaveProductPharmacyResource)
        {

            var ProductPharmacyToBeModified = await _ProductPharmacyService.GetById(Id);
            if (ProductPharmacyToBeModified == null) return BadRequest("Le ProductPharmacy n'existe pas"); //NotFound();
            var ProductPharmacys = _mapperService.Map<SaveProductPharmacyResource, ProductPharmacy>(SaveProductPharmacyResource);
            //var newProductPharmacy = await _ProductPharmacyService.Create(ProductPharmacys);

            await _ProductPharmacyService.Update(ProductPharmacyToBeModified, ProductPharmacys);

            var ProductPharmacyUpdated = await _ProductPharmacyService.GetById(Id);

            var ProductPharmacyResourceUpdated = _mapperService.Map<ProductPharmacy, ProductPharmacyResource>(ProductPharmacyUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductPharmacy(int Id)
        {
            try
            {

                var sub = await _ProductPharmacyService.GetById(Id);
                if (sub == null) return BadRequest("Le ProductPharmacy  n'existe pas"); //NotFound();
                await _ProductPharmacyService.Delete(sub);
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
                List<ProductPharmacy> empty = new List<ProductPharmacy>();
                foreach (var item in Ids)
                {
                    var sub = await _ProductPharmacyService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le ProductPharmacy  n'existe pas"); //NotFound();

                }
                await _ProductPharmacyService.DeleteRange(empty);
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
