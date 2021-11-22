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

    public class ServiceController : ControllerBase
    {
        public IList<Service> Services;

        private readonly IServiceService _ServiceService;

        private readonly IMapper _mapperService;
        public ServiceController(IServiceService ServiceService, IMapper mapper)
        {
            _ServiceService = ServiceService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResource>> CreateService(SaveServiceResource SaveServiceResource)
        {
            //*** Mappage ***
            var Service = _mapperService.Map<SaveServiceResource, Service>(SaveServiceResource);
            Service.CreatedOn = DateTime.UtcNow;
            Service.UpdatedOn = DateTime.UtcNow;
            Service.Active = 0;
            Service.Version = 0;
            Service.CreatedBy = 0;
            Service.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewService = await _ServiceService.Create(Service);
            //*** Mappage ***
            var ServiceResource = _mapperService.Map<Service, ServiceResource>(NewService);
            return Ok(ServiceResource);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResource>> GetAllServices()
        {
            try
            {
                var Employe = await _ServiceService.GetAll();
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
        public async Task<ActionResult<ServiceResource>> GetAllActifServices()
        {
            try
            {
                var Employe = await _ServiceService.GetAllActif();
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
        public async Task<ActionResult<ServiceResource>> GetAllInactifServices()
        {
            try
            {
                var Employe = await _ServiceService.GetAllInActif();
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
        public async Task<ActionResult<ServiceResource>> GetServiceById(int Id)
        {
            try
            {
                var Services = await _ServiceService.GetById(Id);
                if (Services == null) return NotFound();
                var ServiceRessource = _mapperService.Map<Service, ServiceResource>(Services);
                return Ok(ServiceRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ServiceResource>> UpdateService(int Id, SaveServiceResource SaveServiceResource)
        {

            var ServiceToBeModified = await _ServiceService.GetById(Id);
            if (ServiceToBeModified == null) return BadRequest("Le Service n'existe pas"); //NotFound();
            var Service = _mapperService.Map<SaveServiceResource, Service>(SaveServiceResource);
            //var newService = await _ServiceService.Create(Services);
            Service.UpdatedOn = DateTime.UtcNow;
            Service.CreatedOn = ServiceToBeModified.CreatedOn;
            Service.Active = 0;
            Service.Status = 0;
            Service.UpdatedBy = 0;
            Service.CreatedBy = ServiceToBeModified.CreatedBy;
            await _ServiceService.Update(ServiceToBeModified, Service);

            var ServiceUpdated = await _ServiceService.GetById(Id);

            var ServiceResourceUpdated = _mapperService.Map<Service, ServiceResource>(ServiceUpdated);

            return Ok(ServiceResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteService(int Id)
        {
            try
            {

                var sub = await _ServiceService.GetById(Id);
                if (sub == null) return BadRequest("Le Service  n'existe pas"); //NotFound();
                await _ServiceService.Delete(sub);
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
                List<Service> empty = new List<Service>();
                foreach (var item in Ids)
                {
                    var sub = await _ServiceService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Service  n'existe pas"); //NotFound();

                }
                await _ServiceService.DeleteRange(empty);
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
