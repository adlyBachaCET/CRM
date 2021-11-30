using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
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

    public class ObjectionController : ControllerBase
    {
        public IList<Objection> Objections;

        private readonly IObjectionService _ObjectionService;
        private readonly IDoctorService _DoctorService; 
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;
        private readonly IProductService _ProductService;
        private readonly IBusinessUnitService _BusinessUnitService;


        private readonly IMapper _mapperService;
        public ObjectionController(IBusinessUnitService BusinessUnitService, IProductService ProductService, IUserService UserService, IPharmacyService PharmacyService, IDoctorService DoctorService,IObjectionService ObjectionService, IMapper mapper)
        {
            _ProductService = ProductService;
            _BusinessUnitService = BusinessUnitService;

            _UserService = UserService;
            _PharmacyService = PharmacyService;
            _DoctorService = DoctorService;

            _ObjectionService = ObjectionService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ObjectionResource>> CreateObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, SaveObjectionResource SaveObjectionResource)
  {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            //*** Mappage ***
            var Objection = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            Objection.UpdatedOn = DateTime.UtcNow;
            Objection.CreatedOn = DateTime.UtcNow;
            Objection.Active = 0;
            Objection.Status = 0;
            Objection.CreatedBy = Id;
            Objection.UpdatedBy = Id;
            var Doctor = await _DoctorService.GetById(SaveObjectionResource.IdDoctor);
        
            var Pharmacy = await _PharmacyService.GetById(SaveObjectionResource.IdPharmacy);

            if (Pharmacy != null)
            {
                Objection.Name = Pharmacy.Name;
                Objection.Pharmacy = Pharmacy;
                Objection.VersionPharmacy = Pharmacy.Version;
                Objection.StatusPharmacy = Pharmacy.Status;
                Objection.Doctor = null;
                Objection.VersionDoctor = null;
                Objection.StatusDoctor = null;
            }
            if (Doctor != null)
            {
                Objection.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                Objection.Doctor = Doctor;
                Objection.VersionDoctor = Doctor.Version;
                Objection.StatusDoctor = Doctor.Status;

                Objection.Pharmacy = null;
                Objection.VersionPharmacy = null;
                Objection.StatusPharmacy = null;
            }
            var User = await _UserService.GetById(SaveObjectionResource.IdUser);
            Objection.User = User;
            Objection.VersionUser = User.Version;
            Objection.StatusUser = User.Status;

            var Product = await _ProductService.GetById(SaveObjectionResource.IdProduct);
            Objection.Product = Product;
            Objection.VersionProduct = Product.Version;
            Objection.StatusProduct = Product.Status;
            //*** Creation dans la base de données ***
            var NewObjection = await _ObjectionService.Create(Objection);
            //*** Mappage ***
            var ObjectionResource = _mapperService.Map<Objection, ObjectionResource>(NewObjection);
            return Ok(ObjectionResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<ObjectionResource>> GetAllObjections([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Employe = await _ObjectionService.GetAll();
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
        public async Task<ActionResult<ObjectionResource>> GetAllActifObjections([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Employe = await _ObjectionService.GetAllActif();
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
        public async Task<ActionResult<ObjectionResource>> GetAllInactifObjections([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Employe = await _ObjectionService.GetAllInActif();
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
        public async Task<ActionResult<ObjectionResource>> GetObjectionById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Objections = await _ObjectionService.GetById(Id);
                if (Objections == null) return NotFound();
                var ObjectionRessource = _mapperService.Map<Objection, ObjectionResource>(Objections);
                return Ok(ObjectionRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ObjectionResource>> UpdateObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, SaveObjectionResource SaveObjectionResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var ObjectionToBeModified = await _ObjectionService.GetById(Id);
            if (ObjectionToBeModified == null) return BadRequest("Le Objection n'existe pas"); //NotFound();
            var Objection = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            //var newObjection = await _ObjectionService.Create(Objections);
            Objection.UpdatedOn = DateTime.UtcNow;
            Objection.CreatedOn = ObjectionToBeModified.CreatedOn;
            Objection.Active = 0;
            Objection.Status = 0;
            Objection.UpdatedBy = IdUser;
            Objection.CreatedBy = ObjectionToBeModified.CreatedBy;
            var Doctor = await _DoctorService.GetById(SaveObjectionResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveObjectionResource.IdPharmacy);

            if (Pharmacy.IdPharmacy != Objection.IdPharmacy)
            {
                Objection.Name = Pharmacy.Name;
                Objection.Pharmacy = Pharmacy;
                Objection.VersionPharmacy = Pharmacy.Version;
                Objection.StatusPharmacy = Pharmacy.Status;
                Objection.Doctor = null;
                Objection.VersionDoctor = null;
                Objection.StatusDoctor = null;
            }
            else
            {
                Objection.Name = Objection.Name;
                Objection.Pharmacy = Objection.Pharmacy;
                Objection.VersionPharmacy = Objection.VersionPharmacy;
                Objection.StatusPharmacy = Objection.StatusPharmacy;
                Objection.Doctor = null;
                Objection.VersionDoctor = null;
                Objection.StatusDoctor = null;
            }
            if (Doctor.IdDoctor != Objection.IdDoctor)
            {
                Objection.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                Objection.Doctor = Doctor;
                Objection.VersionDoctor = Doctor.Version;
                Objection.StatusDoctor = Doctor.Status;

                Objection.Pharmacy = null;
                Objection.VersionPharmacy = null;
                Objection.StatusPharmacy = null;
            }
            else
            {
                {
                    Objection.Name = Objection.Name;
                    Objection.Doctor = Objection.Doctor;
                    Objection.VersionDoctor = Objection.VersionDoctor;
                    Objection.StatusDoctor = Objection.StatusDoctor;

                    Objection.Pharmacy = null;
                    Objection.VersionPharmacy = null;
                    Objection.StatusPharmacy = null;
                }
            }
            await _ObjectionService.Update(ObjectionToBeModified, Objection);

            var ObjectionUpdated = await _ObjectionService.GetById(Id);

            var ObjectionResourceUpdated = _mapperService.Map<Objection, ObjectionResource>(ObjectionUpdated);

            return Ok(ObjectionResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var ObjectionToBeModified = await _ObjectionService.GetById(Id);
                var sub = await _ObjectionService.GetById(Id);
                if (sub == null) return BadRequest("Le Objection  n'existe pas"); //NotFound();
                await _ObjectionService.Delete(sub);
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
                List<Objection> empty = new List<Objection>();
                foreach (var item in Ids)
                {
                    var sub = await _ObjectionService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Objection  n'existe pas"); //NotFound();

                }
                await _ObjectionService.DeleteRange(empty);
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
