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

    public class WeekSectorCycleInYearController : ControllerBase
    {
        public IList<SectorCycleInYear> WeekSectorCycleInYears;

        private readonly ISectorCycleInYearService _WeekSectorCycleInYearService;
        private readonly ISectorService _SectorService;
        private readonly ICycleService _CycleService;
                private readonly IWeekInYearService _WeekInYearService;

        private readonly IMapper _mapperService;
        public WeekSectorCycleInYearController(IWeekInYearService WeekInYearService,
            ICycleService CycleService,ISectorService SectorService,
            ISectorCycleInYearService WeekSectorCycleInYearService, IMapper mapper)
        {
            _SectorService = SectorService;
            _CycleService = CycleService;
            _WeekInYearService = WeekInYearService;

            _WeekSectorCycleInYearService = WeekSectorCycleInYearService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> CreateWeekSectorCycleInYear(SaveWeekSectorCycleInYearResource SaveWeekSectorCycleInYearResource)
        {
            //*** Mappage ***
            var WeekSectorCycleInYear = _mapperService.Map<SaveWeekSectorCycleInYearResource, SectorCycleInYear>(SaveWeekSectorCycleInYearResource);
            WeekSectorCycleInYear.CreatedOn = DateTime.UtcNow;
            WeekSectorCycleInYear.UpdatedOn = DateTime.UtcNow;
            WeekSectorCycleInYear.Active = 0;
            WeekSectorCycleInYear.Version = 0;
            WeekSectorCycleInYear.CreatedBy = 0;
            WeekSectorCycleInYear.UpdatedBy = 0;
            var Cycle = await _CycleService.GetById(SaveWeekSectorCycleInYearResource.IdCycle);
            var Sector = await _SectorService.GetById(SaveWeekSectorCycleInYearResource.IdSector);
            var WeekInYear = await _WeekInYearService.GetById(SaveWeekSectorCycleInYearResource.Order, SaveWeekSectorCycleInYearResource.Year);
            WeekSectorCycleInYear.IdCycle = Cycle.IdCycle;
            WeekSectorCycleInYear.VersionCycle = Cycle.Version;
            WeekSectorCycleInYear.StatusCycle = Cycle.Status;
            WeekSectorCycleInYear.IdCycleNavigation = Cycle;

            WeekSectorCycleInYear.IdSector = Sector.IdSector;
            WeekSectorCycleInYear.VersionSector = Sector.Version;
            WeekSectorCycleInYear.StatusSector = Sector.Status;
            WeekSectorCycleInYear.IdSectorNavigation = Sector;

            WeekSectorCycleInYear.Order = WeekInYear.Order;
            WeekSectorCycleInYear.Year = WeekInYear.Year;
            WeekSectorCycleInYear.VersionWeekInYear = WeekInYear.Version;
            WeekSectorCycleInYear.StatusWeekInYear = WeekInYear.Status;
            WeekSectorCycleInYear.OrderNavigation = WeekInYear;

            //*** Creation dans la base de données ***
            var NewWeekSectorCycleInYear = await _WeekSectorCycleInYearService.Create(WeekSectorCycleInYear);
            //*** Mappage ***
            var WeekSectorCycleInYearResource = _mapperService.Map<SectorCycleInYear, WeekSectorCycleInYearResource>(NewWeekSectorCycleInYear);
            return Ok(WeekSectorCycleInYearResource);
        }
        [HttpGet]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> GetAllWeekSectorCycleInYears()
        {
            try
            {
                var Employe = await _WeekSectorCycleInYearService.GetAll();
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
        public async Task<ActionResult<WeekSectorCycleInYearResource>> GetAllActifWeekSectorCycleInYears()
        {
            try
            {
                var Employe = await _WeekSectorCycleInYearService.GetAllActif();
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
        public async Task<ActionResult<WeekSectorCycleInYearResource>> GetAllInactifWeekSectorCycleInYears()
        {
            try
            {
                var Employe = await _WeekSectorCycleInYearService.GetAllInActif();
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
        public async Task<ActionResult<WeekSectorCycleInYearResource>> GetWeekSectorCycleInYearById(int Id)
        {
            try
            {
                var WeekSectorCycleInYears = await _WeekSectorCycleInYearService.GetById(Id);
                if (WeekSectorCycleInYears == null) return NotFound();
                var WeekSectorCycleInYearRessource = _mapperService.Map<SectorCycleInYear, WeekSectorCycleInYearResource>(WeekSectorCycleInYears);
                return Ok(WeekSectorCycleInYearRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpPut("{Id}")]
        public async Task<ActionResult<WeekSectorCycleInYear>> UpdateWeekSectorCycleInYear(int Id, SaveWeekSectorCycleInYearResource SaveWeekSectorCycleInYearResource)
        {

            var WeekSectorCycleInYearToBeModified = await _WeekSectorCycleInYearService.GetById(Id);
            if (WeekSectorCycleInYearToBeModified == null) return BadRequest("Le WeekSectorCycleInYear n'existe pas"); //NotFound();
            var WeekSectorCycleInYears = _mapperService.Map<SaveWeekSectorCycleInYearResource, WeekSectorCycleInYear>(SaveWeekSectorCycleInYearResource);
            //var newWeekSectorCycleInYear = await _WeekSectorCycleInYearService.Create(WeekSectorCycleInYears);

            await _WeekSectorCycleInYearService.Update(WeekSectorCycleInYearToBeModified, WeekSectorCycleInYears);

            var WeekSectorCycleInYearUpdated = await _WeekSectorCycleInYearService.GetById(Id);

            var WeekSectorCycleInYearResourceUpdated = _mapperService.Map<WeekSectorCycleInYear, WeekSectorCycleInYearResource>(WeekSectorCycleInYearUpdated);

            return Ok();
        }*/


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteWeekSectorCycleInYear(int Id)
        {
            try
            {

                var sub = await _WeekSectorCycleInYearService.GetById(Id);
                if (sub == null) return BadRequest("Le WeekSectorCycleInYear  n'existe pas"); //NotFound();
                await _WeekSectorCycleInYearService.Delete(sub);
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
                List<SectorCycleInYear> empty = new List<SectorCycleInYear>();
                foreach (var item in Ids)
                {
                    var sub = await _WeekSectorCycleInYearService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le WeekSectorCycleInYear  n'existe pas"); //NotFound();

                }
                await _WeekSectorCycleInYearService.DeleteRange(empty);
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
