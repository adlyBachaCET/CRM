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

    public class WeekSectorCycleController : ControllerBase
    {
        public IList<SectorCycle> WeekSectorCycles;

        private readonly ISectorCycleService _WeekSectorCycleService;

        private readonly IMapper _mapperService;
        public WeekSectorCycleController(ISectorCycleService WeekSectorCycleService, IMapper mapper)
        {
            _WeekSectorCycleService = WeekSectorCycleService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<WeekSectorCycleResource>> CreateWeekSectorCycle(SaveWeekSectorCycleResource SaveWeekSectorCycleResource)
        {
            //*** Mappage ***
            var WeekSectorCycle = _mapperService.Map<SaveWeekSectorCycleResource, SectorCycle>(SaveWeekSectorCycleResource);
            WeekSectorCycle.CreatedOn = DateTime.UtcNow;
            WeekSectorCycle.UpdatedOn = DateTime.UtcNow;
            WeekSectorCycle.Active = 0;
            WeekSectorCycle.Version = 0;
            WeekSectorCycle.CreatedBy = 0;
            WeekSectorCycle.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewWeekSectorCycle = await _WeekSectorCycleService.Create(WeekSectorCycle);
            //*** Mappage ***
            var WeekSectorCycleResource = _mapperService.Map<SectorCycle, WeekSectorCycleResource>(NewWeekSectorCycle);
            return Ok(WeekSectorCycleResource);
        }
        [HttpGet]
        public async Task<ActionResult<WeekSectorCycleResource>> GetAllWeekSectorCycles()
        {
            try
            {
                var Employe = await _WeekSectorCycleService.GetAll();
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
        public async Task<ActionResult<WeekSectorCycleResource>> GetAllActifWeekSectorCycles()
        {
            try
            {
                var Employe = await _WeekSectorCycleService.GetAllActif();
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
        public async Task<ActionResult<WeekSectorCycleResource>> GetAllInactifWeekSectorCycles()
        {
            try
            {
                var Employe = await _WeekSectorCycleService.GetAllInActif();
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
        public async Task<ActionResult<WeekSectorCycleResource>> GetWeekSectorCycleById(int Id)
        {
            try
            {
                var WeekSectorCycles = await _WeekSectorCycleService.GetById(Id);
                if (WeekSectorCycles == null) return NotFound();
                var WeekSectorCycleRessource = _mapperService.Map<SectorCycle, WeekSectorCycleResource>(WeekSectorCycles);
                return Ok(WeekSectorCycleRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpPut("{Id}")]
        public async Task<ActionResult<WeekSectorCycle>> UpdateWeekSectorCycle(int Id, SaveWeekSectorCycleResource SaveWeekSectorCycleResource)
        {

            var WeekSectorCycleToBeModified = await _WeekSectorCycleService.GetById(Id);
            if (WeekSectorCycleToBeModified == null) return BadRequest("Le WeekSectorCycle n'existe pas"); //NotFound();
            var WeekSectorCycles = _mapperService.Map<SaveWeekSectorCycleResource, WeekSectorCycle>(SaveWeekSectorCycleResource);
            //var newWeekSectorCycle = await _WeekSectorCycleService.Create(WeekSectorCycles);

            await _WeekSectorCycleService.Update(WeekSectorCycleToBeModified, WeekSectorCycles);

            var WeekSectorCycleUpdated = await _WeekSectorCycleService.GetById(Id);

            var WeekSectorCycleResourceUpdated = _mapperService.Map<WeekSectorCycle, WeekSectorCycleResource>(WeekSectorCycleUpdated);

            return Ok();
        }*/


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteWeekSectorCycle(int Id)
        {
            try
            {

                var sub = await _WeekSectorCycleService.GetById(Id);
                if (sub == null) return BadRequest("Le WeekSectorCycle  n'existe pas"); //NotFound();
                await _WeekSectorCycleService.Delete(sub);
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
                List<SectorCycle> empty = new List<SectorCycle>();
                foreach (var item in Ids)
                {
                    var sub = await _WeekSectorCycleService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le WeekSectorCycle  n'existe pas"); //NotFound();

                }
                await _WeekSectorCycleService.DeleteRange(empty);
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
