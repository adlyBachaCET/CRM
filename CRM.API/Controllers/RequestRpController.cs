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

    public class RequestRpController : ControllerBase
    {
        public IList<RequestRp> RequestRps;

        private readonly IRequestRpService _RequestRpService;
        private readonly IDoctorService _DoctorService; 
        private readonly IUserService _UserService;


        private readonly IMapper _mapperService;
        public RequestRpController(IUserService UserService, IDoctorService DoctorService,IRequestRpService RequestRpService, IMapper mapper)
        {
            _UserService = UserService;
            _DoctorService = DoctorService;

            _RequestRpService = RequestRpService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<RequestRpResource>> CreateRequestRp(SaveRequestRpResource SaveRequestRpResource)
  {     
            //*** Mappage ***
            var RequestRp = _mapperService.Map<SaveRequestRpResource, RequestRp>(SaveRequestRpResource);
            RequestRp.UpdatedOn = DateTime.UtcNow;
            RequestRp.CreatedOn = DateTime.UtcNow;
            RequestRp.Active = 0;
            RequestRp.Status = 0;
            RequestRp.CreatedBy = 0;
            RequestRp.UpdatedBy = 0;

            //*** Creation dans la base de données ***
            var NewRequestRp = await _RequestRpService.Create(RequestRp);
            //*** Mappage ***
            var RequestRpResource = _mapperService.Map<RequestRp, RequestRpResource>(NewRequestRp);
            return Ok(RequestRpResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<RequestRpResource>> GetAllRequestRps()
        {
            try
            {
                var Employe = await _RequestRpService.GetAll();
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
        public async Task<ActionResult<RequestRpResource>> GetAllActifRequestRps()
        {
            try
            {
                var Employe = await _RequestRpService.GetAllActif();
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
        public async Task<ActionResult<RequestRpResource>> GetAllInactifRequestRps()
        {
            try
            {
                var Employe = await _RequestRpService.GetAllInActif();
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
        public async Task<ActionResult<RequestRpResource>> GetRequestRpById(int Id)
        {
            try
            {
                var RequestRps = await _RequestRpService.GetById(Id);
                if (RequestRps == null) return NotFound();
                var RequestRpRessource = _mapperService.Map<RequestRp, RequestRpResource>(RequestRps);
                return Ok(RequestRpRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<RequestRpResource>> UpdateRequestRp(int Id, SaveRequestRpResource SaveRequestRpResource)
        {

            var RequestRpToBeModified = await _RequestRpService.GetById(Id);
            if (RequestRpToBeModified == null) return BadRequest("Le RequestRp n'existe pas"); //NotFound();
            var RequestRp = _mapperService.Map<SaveRequestRpResource, RequestRp>(SaveRequestRpResource);
            //var newRequestRp = await _RequestRpService.Create(RequestRps);
            RequestRp.UpdatedOn = DateTime.UtcNow;
            RequestRp.CreatedOn = RequestRpToBeModified.CreatedOn;
            RequestRp.Active = 0;
            RequestRp.Status = 0;
            RequestRp.UpdatedBy = 0;
            RequestRp.CreatedBy = RequestRpToBeModified.CreatedBy;
            await _RequestRpService.Update(RequestRpToBeModified, RequestRp);

            var RequestRpUpdated = await _RequestRpService.GetById(Id);

            var RequestRpResourceUpdated = _mapperService.Map<RequestRp, RequestRpResource>(RequestRpUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteRequestRp(int Id)
        {
            try
            {

                var sub = await _RequestRpService.GetById(Id);
                if (sub == null) return BadRequest("Le RequestRp  n'existe pas"); //NotFound();
                await _RequestRpService.Delete(sub);
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
                List<RequestRp> empty = new List<RequestRp>();
                foreach (var item in Ids)
                {
                    var sub = await _RequestRpService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le RequestRp  n'existe pas"); //NotFound();

                }
                await _RequestRpService.DeleteRange(empty);
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
