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

    public class SpecialtyController : ControllerBase
    {
        public IList<Specialty> Specialtys;

        private readonly ISpecialtyService _SpecialtyService;

        private readonly IMapper _mapperService;
        public SpecialtyController(ISpecialtyService SpecialtyService, IMapper mapper)
        {
            _SpecialtyService = SpecialtyService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<SpecialtyResource>> CreateSpecialty(SaveSpecialtyResource SaveSpecialtyResource)
        {
            //*** Mappage ***
            var Specialty = _mapperService.Map<SaveSpecialtyResource, Specialty>(SaveSpecialtyResource);
            Specialty.CreatedOn = DateTime.UtcNow;
            Specialty.UpdatedOn = DateTime.UtcNow;
            Specialty.Active = 0;
            Specialty.Version = 0;
            Specialty.CreatedBy = 0;
            Specialty.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewSpecialty = await _SpecialtyService.Create(Specialty);
            //*** Mappage ***
            var SpecialtyResource = _mapperService.Map<Specialty, SpecialtyResource>(NewSpecialty);
            return Ok(SpecialtyResource);
        }
        [HttpGet]
        public async Task<ActionResult<SpecialtyResource>> GetAllSpecialtys()
        {
            try
            {
                var Employe = await _SpecialtyService.GetAll();
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
        public async Task<ActionResult<SpecialtyResource>> GetAllActifSpecialtys()
        {
            try
            {
                var Employe = await _SpecialtyService.GetAllActif();
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
        public async Task<ActionResult<SpecialtyResource>> GetAllInactifSpecialtys()
        {
            try
            {
                var Employe = await _SpecialtyService.GetAllInActif();
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
        public async Task<ActionResult<SpecialtyResource>> GetSpecialtyById(int Id)
        {
            try
            {
                var Specialtys = await _SpecialtyService.GetById(Id);
                if (Specialtys == null) return NotFound();
                var SpecialtyRessource = _mapperService.Map<Specialty, SpecialtyResource>(Specialtys);
                return Ok(SpecialtyRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<SpecialtyResource>> UpdateSpecialty(int Id, SaveSpecialtyResource SaveSpecialtyResource)
        {

            var SpecialtyToBeModified = await _SpecialtyService.GetById(Id);
            if (SpecialtyToBeModified == null) return BadRequest("Le Specialty n'existe pas"); //NotFound();
            var Specialtys = _mapperService.Map<SaveSpecialtyResource, Specialty>(SaveSpecialtyResource);
            //var newSpecialty = await _SpecialtyService.Create(Specialtys);

            await _SpecialtyService.Update(SpecialtyToBeModified, Specialtys);

            var SpecialtyUpdated = await _SpecialtyService.GetById(Id);

            var SpecialtyResourceUpdated = _mapperService.Map<Specialty, SpecialtyResource>(SpecialtyUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteSpecialty(int Id)
        {
            try
            {

                var sub = await _SpecialtyService.GetById(Id);
                if (sub == null) return BadRequest("Le Specialty  n'existe pas"); //NotFound();
                await _SpecialtyService.Delete(sub);
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
                List<Specialty> empty = new List<Specialty>();
                foreach (var item in Ids)
                {
                    var sub = await _SpecialtyService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Specialty  n'existe pas"); //NotFound();

                }
                await _SpecialtyService.DeleteRange(empty);
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
