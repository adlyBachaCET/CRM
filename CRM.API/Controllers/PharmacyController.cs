using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class PharmacyController : ControllerBase
    {
        public IList<Pharmacy> Pharmacys;

        private readonly IPharmacyService _PharmacyService;
        private readonly IUserService _UserService;
        private readonly IPhoneService _PhoneService;

        private readonly IMapper _mapperService;
        public PharmacyController(IPhoneService PhoneService,IUserService UserService,IPharmacyService PharmacyService, IMapper mapper)
        {
            _PhoneService = PhoneService;
               _UserService = UserService;
               _PharmacyService = PharmacyService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Pharmacy>> CreatePharmacy(SaveAddPharmacyResource SaveAddPharmacyResource)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token!="")
            {               
            var claims = _UserService.getPrincipal(token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Role").Value);

                var Exist = await _PharmacyService.Verify(SaveAddPharmacyResource);

            if (Exist.ExistPharmacyEmail==false&& Exist.ExistPharmacyFirstName == false
                && Exist.ExistPharmacyLastName== false&& Exist.ExistPharmacyName == false) {

            

             //*** Mappage ***
            var Pharmacy = _mapperService.Map<SavePharmacyResource, Pharmacy>(SaveAddPharmacyResource.SavePharmacyResource);
            Pharmacy.CreatedOn = DateTime.UtcNow;
            Pharmacy.UpdatedOn = DateTime.UtcNow;
            Pharmacy.Active = 0;
            Pharmacy.Version = 0;
            Pharmacy.CreatedBy = Id;
            Pharmacy.UpdatedBy = Id;

                    if (Role == "Manager") { 
            Pharmacy.Status = Status.Approuved;
                    }
            else if (Role == "Delegue")
                    {
                        Pharmacy.Status = Status.Pending;
                    }
                    //*** Creation dans la base de données ***
                    var NewPharmacy = await _PharmacyService.Create(Pharmacy);
            //*** Mappage ***
            var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(NewPharmacy);
                PhoneResource PhoneResourceOld = new PhoneResource();
                foreach (var item in SaveAddPharmacyResource.SavePhoneResource)
                {

                    var Phone = _mapperService.Map<SavePhoneResource, Phone>(item);
                    Phone.IdPharmacy = PharmacyResource.IdPharmacy;
                    //*** Creation dans la base de données ***
                    var NewPhone = await _PhoneService.Create(Phone);
                    //*** Mappage ***
                    var PhoneResource = _mapperService.Map<Phone, PhoneResource>(NewPhone);
                    PhoneResourceOld = PhoneResource;
                }
                var genericResult = new { Phones = PhoneResourceOld, Pharmacy = PharmacyResource };


                return Ok(genericResult);
            }
            else
            {
                var genericResult = new { Exist = "Already exists", Pharmacy = Exist };

                return Ok(genericResult);
                }

            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }

        }
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<PharmacyResource>> GetPharmacysNumber(int Number)
        {
            try
            {
                var Employe = await _PharmacyService.GetByExistantPhoneNumberActif(Number);
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("NearBy")]
        public async Task<ActionResult<PharmacyResource>> GetPharmacysNumber(Nearby Nearby)
        {
            try
            {
                var Employe = await _PharmacyService.GetByNearByActif(Nearby.Locality1, Nearby.Locality2, Nearby.CodePostal);
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Role").Value);

                var PharmacyToBeModified = await _PharmacyService.GetById(Id);
            if (PharmacyToBeModified == null) return BadRequest("Le Pharmacy n'existe pas"); //NotFound();
            var Pharmacys = _mapperService.Map<SavePharmacyResource, Pharmacy>(SavePharmacyResource);
            //var newPharmacy = await _PharmacyService.Create(Pharmacys);
           // Pharmacys.CreatedOn = SavePharmacyResource.;
            Pharmacys.UpdatedOn = DateTime.UtcNow;
                Pharmacys.UpdatedBy = IdUser;

                if (Role == "Manager")
                {
                    Pharmacys.Status = Status.Approuved;
                }
                else if (Role == "Delegue")
                {
                    Pharmacys.Status = Status.Pending;
                }
                await _PharmacyService.Update(PharmacyToBeModified, Pharmacys);

            var PharmacyUpdated = await _PharmacyService.GetById(Id);

            var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyUpdated);

            return Ok();
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<Pharmacy>> ApprouvePharmacy(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var PharmacyToBeModified = await _PharmacyService.GetById(Id);
                if (PharmacyToBeModified == null) return BadRequest("Le Pharmacy n'existe pas"); //NotFound();
                                                                                                 //var newPharmacy = await _PharmacyService.Create(Pharmacys);
                                                                                                 // Pharmacys.CreatedOn = SavePharmacyResource.;
                PharmacyToBeModified.UpdatedOn = DateTime.UtcNow;
                PharmacyToBeModified.UpdatedBy = IdUser;

                await _PharmacyService.Approuve(PharmacyToBeModified, PharmacyToBeModified);

                var PharmacyUpdated = await _PharmacyService.GetById(Id);

                var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyUpdated);

                return Ok(PharmacyResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<Pharmacy>> RejectPharmacy(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var PharmacyToBeModified = await _PharmacyService.GetById(Id);
                if (PharmacyToBeModified == null) return BadRequest("Le Pharmacy n'existe pas"); //NotFound();
                                                                                                 //var newPharmacy = await _PharmacyService.Create(Pharmacys);
                                                                                                 // Pharmacys.CreatedOn = SavePharmacyResource.;
                PharmacyToBeModified.UpdatedOn = DateTime.UtcNow;
                PharmacyToBeModified.UpdatedBy = IdUser;

                await _PharmacyService.Reject(PharmacyToBeModified, PharmacyToBeModified);

                var PharmacyUpdated = await _PharmacyService.GetById(Id);

                var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyUpdated);

                return Ok(PharmacyResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
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
