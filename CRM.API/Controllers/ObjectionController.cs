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

    public class ObjectionController : ControllerBase
    {
        public IList<Objection> Objections;

        private readonly IObjectionService _ObjectionService;
        private readonly IDoctorService _DoctorService; 
        private readonly IUserService _UserService;


        private readonly IMapper _mapperService;
        public ObjectionController(IUserService UserService, IDoctorService DoctorService,IObjectionService ObjectionService, IMapper mapper)
        {
            _UserService = UserService;
            _DoctorService = DoctorService;

            _ObjectionService = ObjectionService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ObjectionResource>> CreateObjection(SaveObjectionResource SaveObjectionResource)
  {     
            //*** Mappage ***
            var Objection = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            Objection.UpdatedOn = DateTime.UtcNow;
            Objection.CreatedOn = DateTime.UtcNow;
            Objection.Active = 0;
            Objection.Status = 0;
            var Doctor = await _DoctorService.GetById(SaveObjectionResource.IdDoctor);
            Objection.Doctor = Doctor;
            Objection.VersionDoctor = Doctor.Version;
            Objection.StatusDoctor = Doctor.Status;
            var User = await _UserService.GetById(SaveObjectionResource.IdUser);
            Objection.User = User;
            Objection.VersionUser = User.Version;
            Objection.StatusUser = User.Status;
            //*** Creation dans la base de données ***
            var NewObjection = await _ObjectionService.Create(Objection);
            //*** Mappage ***
            var ObjectionResource = _mapperService.Map<Objection, ObjectionResource>(NewObjection);
            return Ok(ObjectionResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<ObjectionResource>> GetAllObjections()
        {
            try
            {
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
        public async Task<ActionResult<ObjectionResource>> GetAllActifObjections()
        {
            try
            {
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
        public async Task<ActionResult<ObjectionResource>> GetAllInactifObjections()
        {
            try
            {
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
        public async Task<ActionResult<ObjectionResource>> GetObjectionById(int Id)
        {
            try
            {
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
        public async Task<ActionResult<ObjectionResource>> UpdateObjection(int Id, SaveObjectionResource SaveObjectionResource)
        {

            var ObjectionToBeModified = await _ObjectionService.GetById(Id);
            if (ObjectionToBeModified == null) return BadRequest("Le Objection n'existe pas"); //NotFound();
            var Objections = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            //var newObjection = await _ObjectionService.Create(Objections);

            await _ObjectionService.Update(ObjectionToBeModified, Objections);

            var ObjectionUpdated = await _ObjectionService.GetById(Id);

            var ObjectionResourceUpdated = _mapperService.Map<Objection, ObjectionResource>(ObjectionUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteObjection(int Id)
        {
            try
            {

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
        public async Task<ActionResult> DeleteRange(List<int> Ids)
        {
            try
            {
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
