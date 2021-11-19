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
        public IList<BusinessUnit> BusinessUnits;

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
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
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
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
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
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
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
                var BusinessUnits = await _BusinessUnitService.GetById(Id);
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
            if (BusinessUnitToBeModified == null) return BadRequest("Le BusinessUnit n'existe pas"); //NotFound();
            var BusinessUnits = _mapperService.Map<SaveBusinessUnitResource, BusinessUnit>(SaveBusinessUnitResource);
            //var newBusinessUnit = await _BusinessUnitService.Create(BusinessUnits);

            await _BusinessUnitService.Update(BusinessUnitToBeModified, BusinessUnits);

            var BusinessUnitUpdated = await _BusinessUnitService.GetById(Id);

            var BusinessUnitResourceUpdated = _mapperService.Map<BusinessUnit, BusinessUnitResource>(BusinessUnitUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBusinessUnit(int Id)
        {
            try
            {

                var sub = await _BusinessUnitService.GetById(Id);
                if (sub == null) return BadRequest("Le BusinessUnit  n'existe pas"); //NotFound();
                await _BusinessUnitService.Delete(sub);
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
                List<BusinessUnit> empty = new List<BusinessUnit>();
                foreach (var item in Ids)
                {
                    var sub = await _BusinessUnitService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le BusinessUnit  n'existe pas"); //NotFound();

                }
                await _BusinessUnitService.DeleteRange(empty);
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
