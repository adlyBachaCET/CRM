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

    public class SectorInYearController : ControllerBase
    {
        public IList<SectorInYear> WeekSectorCycleInYears;

        private readonly ISectorInYearService _SectorInYearService;
        private readonly ISectorService _SectorService;
        private readonly ICycleService _CycleService;
                private readonly IWeekInYearService _WeekInYearService;

        private readonly IMapper _mapperService;
        public SectorInYearController(IWeekInYearService WeekInYearService,
            ICycleService CycleService,ISectorService SectorService,
            ISectorInYearService WeekSectorCycleInYearService, IMapper mapper)
        {
            _SectorService = SectorService;
            _CycleService = CycleService;
            _WeekInYearService = WeekInYearService;

            _SectorInYearService = WeekSectorCycleInYearService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> CreateWeekSectorCycleInYear(SaveWeekSectorCycleInYearResource SaveWeekSectorCycleInYearResource)
        {
            try { 
            //*** Mappage ***
            var SectorInYear = _mapperService.Map<SaveWeekSectorCycleInYearResource, SectorInYear>(SaveWeekSectorCycleInYearResource);
            SectorInYear.CreatedOn = DateTime.UtcNow;
            SectorInYear.UpdatedOn = DateTime.UtcNow;
            SectorInYear.Active = 0;
            SectorInYear.Version = 0;
            SectorInYear.CreatedBy = 0;
            SectorInYear.UpdatedBy = 0;
            SectorInYear.Lock =0 ;
            SectorInYear.Request = false;

            var Sector = await _SectorService.GetById(SaveWeekSectorCycleInYearResource.IdSector);
            var WeekInYear = await _WeekInYearService.GetById(SaveWeekSectorCycleInYearResource.Order, SaveWeekSectorCycleInYearResource.Year);


            SectorInYear.IdSector = Sector.IdSector;
            SectorInYear.VersionSector = Sector.Version;
            SectorInYear.StatusSector = Sector.Status;
            SectorInYear.IdSectorNavigation = Sector;

            SectorInYear.Order = WeekInYear.Order;
            SectorInYear.Year = WeekInYear.Year;
            SectorInYear.VersionWeekInYear = WeekInYear.Version;
            SectorInYear.StatusWeekInYear = WeekInYear.Status;
            SectorInYear.OrderNavigation = WeekInYear;

            //*** Creation dans la base de données ***
            var NewWeekSectorCycleInYear = await _SectorInYearService.Create(SectorInYear);
            //*** Mappage ***
            var WeekSectorCycleInYearResource = _mapperService.Map<SectorInYear, WeekSectorCycleInYearResource>(NewWeekSectorCycleInYear);
            return Ok(WeekSectorCycleInYearResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> GetAllWeekSectorCycleInYears()
        {
            try
            {
                var Employe = await _SectorInYearService.GetAll();
                if (Employe == null) return NotFound();
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
                var Employe = await _SectorInYearService.GetAllActif();
                if (Employe == null) return NotFound();
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
                var Employe = await _SectorInYearService.GetAllInActif();
                if (Employe == null) return NotFound();
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
                var WeekSectorCycleInYears = await _SectorInYearService.GetById(Id);
                if (WeekSectorCycleInYears == null) return NotFound();
                var WeekSectorCycleInYearRessource = _mapperService.Map<SectorInYear, WeekSectorCycleInYearResource>(WeekSectorCycleInYears);
                return Ok(WeekSectorCycleInYearRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Request/{Id}")]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> RequestSectorCycleInYearById(int Id)
        {
            try
            {
                await _SectorInYearService.RequestOpeningWeek(Id);
             return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Deny/{Id}")]
        public async Task<ActionResult<WeekSectorCycleInYearResource>> Deny(int Id)
        {
            try
            {
                await _SectorInYearService.DenyRequestOpeningWeek(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteWeekSectorCycleInYear(int Id)
        {
            try
            {

                var sub = await _SectorInYearService.GetById(Id);
                if (sub == null) return BadRequest("Le WeekSectorCycleInYear  n'existe pas"); 
                await _SectorInYearService.Delete(sub);
             
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
                List<SectorInYear> empty = new List<SectorInYear>();
                foreach (var item in Ids)
                {
                    var sub = await _SectorInYearService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le WeekSectorCycleInYear  n'existe pas"); 

                }
                await _SectorInYearService.DeleteRange(empty);
               
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
