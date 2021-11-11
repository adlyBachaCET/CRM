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

    public class PharmacyController : ControllerBase
    {
        public IList<Pharmacy> Pharmacys;

        private readonly IPharmacyService _PharmacyService;
        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public PharmacyController(IUserService UserService,IPharmacyService PharmacyService, IMapper mapper)
        {
            _UserService = UserService;
               _PharmacyService = PharmacyService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Pharmacy>> CreatePharmacy(SavePharmacyResource SavePharmacyResource)
        {
            //*** Mappage ***
            var Pharmacy = _mapperService.Map<SavePharmacyResource, Pharmacy>(SavePharmacyResource);
            Pharmacy.CreatedOn = DateTime.UtcNow;
            Pharmacy.UpdatedOn = DateTime.UtcNow;

            //*** Creation dans la base de données ***
            var NewPharmacy = await _PharmacyService.Create(Pharmacy);
            //*** Mappage ***
            var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(NewPharmacy);
            return Ok(PharmacyResource);
        }
        [HttpGet]
        public async Task<ActionResult<PharmacyResource>> GetAllPharmacys()
        {
            try
            {
                var Employe = await _PharmacyService.GetAll();
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
        public async Task<ActionResult<PharmacyResource>> GetAllActifPharmacys()
        {
            try
            {
                var Employe = await _PharmacyService.GetAllActif();
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
        public async Task<ActionResult<PharmacyResource>> GetAllInactifPharmacys()
        {
            try
            {
                var Employe = await _PharmacyService.GetAllInActif();
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
        public async Task<ActionResult<PharmacyResource>> GetPharmacyById(int Id)
        {
            try
            {
                var Pharmacys = await _PharmacyService.GetById(Id);
                if (Pharmacys == null) return NotFound();
                var PharmacyRessource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacys);
                return Ok(PharmacyRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Pharmacy>> UpdatePharmacy(int Id, SavePharmacyResource SavePharmacyResource)
        {

            var PharmacyToBeModified = await _PharmacyService.GetById(Id);
            if (PharmacyToBeModified == null) return BadRequest("Le Pharmacy n'existe pas"); //NotFound();
            var Pharmacys = _mapperService.Map<SavePharmacyResource, Pharmacy>(SavePharmacyResource);
            //var newPharmacy = await _PharmacyService.Create(Pharmacys);
           // Pharmacys.CreatedOn = SavePharmacyResource.;
            Pharmacys.UpdatedOn = DateTime.UtcNow;
            await _PharmacyService.Update(PharmacyToBeModified, Pharmacys);

            var PharmacyUpdated = await _PharmacyService.GetById(Id);

            var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePharmacy(int Id)
        {
            try
            {

                var sub = await _PharmacyService.GetById(Id);
                if (sub == null) return BadRequest("Le Pharmacy  n'existe pas"); //NotFound();
                await _PharmacyService.Delete(sub);
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
                List<Pharmacy> empty = new List<Pharmacy>();
                foreach (var item in Ids)
                {
                    var sub = await _PharmacyService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Pharmacy  n'existe pas"); //NotFound();

                }
                await _PharmacyService.DeleteRange(empty);
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
