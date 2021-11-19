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

    public class RequestDoctorController : ControllerBase
    {
        public IList<RequestDoctor> RequestDoctors;

        private readonly IRequestDoctorService _RequestDoctorService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;


        private readonly IMapper _mapperService;
        public RequestDoctorController(IUserService UserService, IDoctorService DoctorService,IRequestDoctorService RequestDoctorService, IMapper mapper)
        {
            _UserService = UserService;

            _DoctorService = DoctorService;
           _RequestDoctorService = RequestDoctorService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<RequestDoctorResource>> CreateRequestDoctor(SaveRequestDoctorResource SaveRequestDoctorResource)
  {     
            //*** Mappage ***
            var RequestDoctor = _mapperService.Map<SaveRequestDoctorResource, RequestDoctor>(SaveRequestDoctorResource);
            RequestDoctor.UpdatedOn = DateTime.UtcNow;
            RequestDoctor.CreatedOn = DateTime.UtcNow;
            RequestDoctor.Active = 0;
            RequestDoctor.Status = 0;
            var Doctor = await _DoctorService.GetById(SaveRequestDoctorResource.IdDoctor);
            RequestDoctor.Doctor = Doctor;
            RequestDoctor.VersionDoctor = Doctor.Version;
            RequestDoctor.StatusDoctor = Doctor.Status;

            var User = await _UserService.GetById(SaveRequestDoctorResource.IdUser);
            RequestDoctor.User = User;
            RequestDoctor.VersionUser = User.Version;
            RequestDoctor.StatusUser = User.Status;
            //*** Creation dans la base de données ***
            var NewRequestDoctor = await _RequestDoctorService.Create(RequestDoctor);
            //*** Mappage ***
            var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(NewRequestDoctor);
            return Ok(RequestDoctorResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<RequestDoctorResource>> GetAllRequestDoctors()
        {
            try
            {
                var Employe = await _RequestDoctorService.GetAll();
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
        public async Task<ActionResult<RequestDoctorResource>> GetAllActifRequestDoctors()
        {
            try
            {
                var Employe = await _RequestDoctorService.GetAllActif();
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
        public async Task<ActionResult<RequestDoctorResource>> GetAllInactifRequestDoctors()
        {
            try
            {
                var Employe = await _RequestDoctorService.GetAllInActif();
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
        public async Task<ActionResult<RequestDoctorResource>> GetRequestDoctorById(int Id)
        {
            try
            {
                var RequestDoctors = await _RequestDoctorService.GetById(Id);
                if (RequestDoctors == null) return NotFound();
                var RequestDoctorRessource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(RequestDoctors);
                return Ok(RequestDoctorRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<RequestDoctorResource>> UpdateRequestDoctor(int Id, SaveRequestDoctorResource SaveRequestDoctorResource)
        {

            var RequestDoctorToBeModified = await _RequestDoctorService.GetById(Id);
            if (RequestDoctorToBeModified == null) return BadRequest("Le RequestDoctor n'existe pas"); //NotFound();
            var RequestDoctors = _mapperService.Map<SaveRequestDoctorResource, RequestDoctor>(SaveRequestDoctorResource);
            //var newRequestDoctor = await _RequestDoctorService.Create(RequestDoctors);

            await _RequestDoctorService.Update(RequestDoctorToBeModified, RequestDoctors);

            var RequestDoctorUpdated = await _RequestDoctorService.GetById(Id);

            var RequestDoctorResourceUpdated = _mapperService.Map<RequestDoctor, RequestDoctorResource>(RequestDoctorUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteRequestDoctor(int Id)
        {
            try
            {

                var sub = await _RequestDoctorService.GetById(Id);
                if (sub == null) return BadRequest("Le RequestDoctor  n'existe pas"); //NotFound();
                await _RequestDoctorService.Delete(sub);
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
                List<RequestDoctor> empty = new List<RequestDoctor>();
                foreach (var item in Ids)
                {
                    var sub = await _RequestDoctorService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le RequestDoctor  n'existe pas"); //NotFound();

                }
                await _RequestDoctorService.DeleteRange(empty);
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
