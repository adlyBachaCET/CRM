

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

    public class WeekInYearController : ControllerBase
    {
        public IList<WeekInYear> WeekInYears;

        private readonly IWeekInYearService _WeekInYearService;

        private readonly IMapper _mapperService;
        public WeekInYearController(IWeekInYearService WeekInYearService, IMapper mapper)
        {
            _WeekInYearService = WeekInYearService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<WeekInYearResource>> CreateWeekInYear(SaveWeekInYearResource SaveWeekInYearResource)
        {
            //*** Mappage ***
            var WeekInYear = _mapperService.Map<SaveWeekInYearResource, WeekInYear>(SaveWeekInYearResource);
            WeekInYear.CreatedOn = DateTime.UtcNow;
            WeekInYear.UpdatedOn = DateTime.UtcNow;
            WeekInYear.Active = 0;
            WeekInYear.Version = 0;
            WeekInYear.CreatedBy = 0;
            WeekInYear.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewWeekInYear = await _WeekInYearService.Create(WeekInYear);
            //*** Mappage ***
            var WeekInYearResource = _mapperService.Map<WeekInYear, WeekInYearResource>(NewWeekInYear);
            return Ok(WeekInYearResource);
        }
        [HttpGet]
        public async Task<ActionResult<WeekInYearResource>> GetAllWeekInYears()
        {
            try
            {
                var Employe = await _WeekInYearService.GetAll();
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
        public async Task<ActionResult<WeekInYearResource>> GetAllActifWeekInYears()
        {
            try
            {
                var Employe = await _WeekInYearService.GetAllActif();
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
        public async Task<ActionResult<WeekInYearResource>> GetAllInactifWeekInYears()
        {
            try
            {
                var Employe = await _WeekInYearService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Order}/{Year}")]
        public async Task<ActionResult<WeekInYearResource>> GetWeekInYearById(int Order,int Year)
        {
            try
            {
                var WeekInYears = await _WeekInYearService.GetById(Order,Year);
                if (WeekInYears == null) return NotFound();
                var WeekInYearRessource = _mapperService.Map<WeekInYear, WeekInYearResource>(WeekInYears);
                return Ok(WeekInYearRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Order}/{Year}")]
        public async Task<ActionResult<WeekInYearResource>> UpdateWeekInYear(int Order, int Year, SaveWeekInYearResource SaveWeekInYearResource)
        {

            var WeekInYearToBeModified = await _WeekInYearService.GetById(Order, Year);
            if (WeekInYearToBeModified == null) return BadRequest("Le WeekInYear n'existe pas"); //NotFound();
            var WeekInYear = _mapperService.Map<SaveWeekInYearResource, WeekInYear>(SaveWeekInYearResource);
            //var newWeekInYear = await _WeekInYearService.Create(WeekInYears);
            WeekInYear.CreatedOn = DateTime.UtcNow;
            WeekInYear.UpdatedOn = DateTime.UtcNow;
            WeekInYear.Active = 0;
            WeekInYear.Version = 0;
            WeekInYear.CreatedBy = 0;
            WeekInYear.UpdatedBy = 0;
            await _WeekInYearService.Update(WeekInYearToBeModified, WeekInYear);

            var WeekInYearUpdated = await _WeekInYearService.GetById(Order, Year);

            var WeekInYearResourceUpdated = _mapperService.Map<WeekInYear, WeekInYearResource>(WeekInYearUpdated);

            return Ok(WeekInYearResourceUpdated);
        }


        [HttpDelete("{Order}/{Year}")]
        public async Task<ActionResult> DeleteWeekInYear(int Order, int Year)
        {
            try
            {

                var sub = await _WeekInYearService.GetById(Order, Year);
                if (sub == null) return BadRequest("Le WeekInYear  n'existe pas"); //NotFound();
                await _WeekInYearService.Delete(sub);
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
