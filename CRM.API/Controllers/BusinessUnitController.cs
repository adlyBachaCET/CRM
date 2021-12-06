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

    public class BusinessUnitController : ControllerBase
    {
#pragma warning disable S1104 // Fields should not have public accessibility
        public IList<BusinessUnit> BusinessUnits;
#pragma warning restore S1104 // Fields should not have public accessibility

        private readonly IBusinessUnitService _BusinessUnitService;

        private readonly IMapper _mapperService;
        public BusinessUnitController(IBusinessUnitService BusinessUnitService, IMapper mapper)
        {
            _BusinessUnitService = BusinessUnitService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BusinessUnitResource>> CreateBusinessUnit(SaveBusinessUnitResource SaveBusinessUnitResource)
        {
            //*** Mappage ***
            var BusinessUnit = _mapperService.Map<SaveBusinessUnitResource, BusinessUnit>(SaveBusinessUnitResource);
            BusinessUnit.UpdatedOn = DateTime.UtcNow;
            BusinessUnit.CreatedOn = DateTime.UtcNow;
            BusinessUnit.Active = 0;
            BusinessUnit.Status = Status.Approuved;
            BusinessUnit.UpdatedBy = 0;
            BusinessUnit.CreatedBy = 0;
            //*** Creation dans la base de données ***
            var NewBusinessUnit = await _BusinessUnitService.Create(BusinessUnit);
            //*** Mappage ***
            var BusinessUnitResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(NewBusinessUnit);
            return Ok(BusinessUnitResource);
        }
        [HttpGet]
        public async Task<ActionResult<BusinessUnitResource>> GetAllBusinessUnits()
        {
            try
            {
                var Employe = await _BusinessUnitService.GetAll();
                if (Employe == null) return NotFound();
                
#pragma warning disable S125 // Sections of code should not be commented out
// var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
#pragma warning restore S125 // Sections of code should not be commented out
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<BusinessUnitResource>> GetAllActifBusinessUnits()
        {
            try
            {
                var Employe = await _BusinessUnitService.GetAllActif();
                if (Employe == null) return NotFound();
                
#pragma warning disable S125 // Sections of code should not be commented out
// var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
#pragma warning restore S125 // Sections of code should not be commented out
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<BusinessUnitResource>> GetAllInactifBusinessUnits()
        {
            try
            {
                var Employe = await _BusinessUnitService.GetAllInActif();
                if (Employe == null) return NotFound();
                
#pragma warning disable S125 // Sections of code should not be commented out
// var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
#pragma warning restore S125 // Sections of code should not be commented out
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BusinessUnitResource>> GetBusinessUnitById(int Id)
        {
            try
            {
#pragma warning disable S1117 // Local variables should not shadow class fields
                var BusinessUnits = await _BusinessUnitService.GetById(Id);
#pragma warning restore S1117 // Local variables should not shadow class fields
                if (BusinessUnits == null) return NotFound();
                var BusinessUnitRessource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(BusinessUnits);
                return Ok(BusinessUnitRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<BusinessUnitResource>> UpdateBusinessUnit(int Id, SaveBusinessUnitResource SaveBusinessUnitResource)
        {

            var BusinessUnitToBeModified = await _BusinessUnitService.GetById(Id);
#pragma warning disable S125 // Sections of code should not be commented out
            if (BusinessUnitToBeModified == null) return BadRequest("Le BusinessUnit n'existe pas"); //NotFound();
#pragma warning restore S125 // Sections of code should not be commented out
#pragma warning disable S1117 // Local variables should not shadow class fields
            var BusinessUnits = _mapperService.Map<SaveBusinessUnitResource, BusinessUnit>(SaveBusinessUnitResource);
#pragma warning restore S1117 // Local variables should not shadow class fields
            
#pragma warning disable S125 // Sections of code should not be commented out
//var newBusinessUnit = await _BusinessUnitService.Create(BusinessUnits);

            await _BusinessUnitService.Update(BusinessUnitToBeModified, BusinessUnits);
#pragma warning restore S125 // Sections of code should not be commented out

            var BusinessUnitUpdated = await _BusinessUnitService.GetById(Id);

            var BusinessUnitResourceUpdated = _mapperService.Map<BusinessUnit, BusinessUnitResource>(BusinessUnitUpdated);

            return Ok(BusinessUnitResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBusinessUnit(int Id)
        {
            try
            {

                var sub = await _BusinessUnitService.GetById(Id);
#pragma warning disable S125 // Sections of code should not be commented out
                if (sub == null) return BadRequest("Le BusinessUnit  n'existe pas"); //NotFound();
#pragma warning restore S125 // Sections of code should not be commented out
                await _BusinessUnitService.Delete(sub);
#pragma warning disable S1116 // Empty statements should be removed
                ;
#pragma warning restore S1116 // Empty statements should be removed
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
                List<BusinessUnit> empty = new List<BusinessUnit>();
                foreach (var item in Ids)
                {
                    var sub = await _BusinessUnitService.GetById(item);
                    empty.Add(sub);
#pragma warning disable S125 // Sections of code should not be commented out
                    if (sub == null) return BadRequest("Le BusinessUnit  n'existe pas"); //NotFound();
#pragma warning restore S125 // Sections of code should not be commented out

                }
                await _BusinessUnitService.DeleteRange(empty);
#pragma warning disable S1116 // Empty statements should be removed
                ;
#pragma warning restore S1116 // Empty statements should be removed
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
