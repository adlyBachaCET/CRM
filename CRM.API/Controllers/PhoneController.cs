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

    public class PhoneController : ControllerBase
    {
        public IList<Phone> Phones;

        private readonly IPhoneService _PhoneService;
        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public PhoneController(IUserService UserService,IPhoneService PhoneService, IMapper mapper)
        {
            _UserService = UserService;
               _PhoneService = PhoneService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<PhoneResource>> CreatePhone(SavePhoneResource SavePhoneResource)
        {
            try { 
            //*** Mappage ***
            var Phone = _mapperService.Map<SavePhoneResource, Phone>(SavePhoneResource);
            Phone.CreatedOn = DateTime.UtcNow;
            Phone.UpdatedOn = DateTime.UtcNow;
            Phone.Active = 0;
            Phone.Version = 0;
            Phone.CreatedBy = 0;
            Phone.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewPhone = await _PhoneService.Create(Phone);
            //*** Mappage ***
            var PhoneResource = _mapperService.Map<Phone, PhoneResource>(NewPhone);
            return Ok(PhoneResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<PhoneResource>> GetAllPhones()
        {
            try
            {
                var Employe = await _PhoneService.GetAll();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Doctor/{Id}")]
        public async Task<ActionResult<PhoneResource>> GetAllPhonesByDoctor(int Id)
        {
            try
            {
                var Users = await _PhoneService.GetByIdDoctor(Id);
                if (Users == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
        [HttpGet("Pharmacy/{Id}")]
        public async Task<ActionResult<PhoneResource>> GetAllPhonesByPharmacy(int Id)
        {
            try
            {
                var Users = await _PhoneService.GetByIdPharmacy(Id);
                if (Users == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("WholeSaler/{Id}")]
        public async Task<ActionResult<PhoneResource>> GetAllPhonesByWholeSaler(int Id)
        {
            try
            {
                var Users = await _PhoneService.GetByIdWholeSaler(Id);
                if (Users == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<PhoneResource>> GetAllActifPhones()
        {
            try
            {
                var Employe = await _PhoneService.GetAllActif();
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
        public async Task<ActionResult<PhoneResource>> GetAllInactifPhones()
        {
            try
            {
                var Employe = await _PhoneService.GetAllInActif();
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
        public async Task<ActionResult<PhoneResource>> GetPhoneById(int Id)
        {
            try
            {
                var Phones = await _PhoneService.GetById(Id);
                if (Phones == null) return NotFound();
                var PhoneRessource = _mapperService.Map<Phone, PhoneResource>(Phones);
                return Ok(PhoneRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<PhoneResource>> UpdatePhone(int Id, SavePhoneResource SavePhoneResource)
        {
            try { 
            var PhoneToBeModified = await _PhoneService.GetById(Id);
            if (PhoneToBeModified == null) return BadRequest("Le Phone n'existe pas"); //NotFound();
            var Phones = _mapperService.Map<SavePhoneResource, Phone>(SavePhoneResource);
            //var newPhone = await _PhoneService.Create(Phones);

            await _PhoneService.Update(PhoneToBeModified, Phones);

            var PhoneUpdated = await _PhoneService.GetById(Id);

            var PhoneResourceUpdated = _mapperService.Map<Phone, PhoneResource>(PhoneUpdated);

            return Ok(PhoneResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePhone(int Id)
        {
            try
            {

                var sub = await _PhoneService.GetById(Id);
                if (sub == null) return BadRequest("Le Phone  n'existe pas"); 
                await _PhoneService.Delete(sub);              
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
                List<Phone> empty = new List<Phone>();
                foreach (var item in Ids)
                {
                    var sub = await _PhoneService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Phone  n'existe pas");
                }
                await _PhoneService.DeleteRange(empty);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
