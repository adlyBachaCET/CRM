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

    public class CycleController : ControllerBase
    {
        public IList<Cycle> Cycles;

        private readonly ICycleService _CycleService;
        private readonly ISectorService _SectorService;
        private readonly ISectorCycleService _SectorCycleService;

        private readonly IMapper _mapperService;
        public CycleController(ISectorCycleService SectorCycleService,ISectorService SectorService,ICycleService CycleService, IMapper mapper)
        {
            _SectorCycleService = SectorCycleService;
            _SectorService = SectorService;
            _CycleService = CycleService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Cycle>> CreateCycle(SaveCycleResource SaveCycleResource)
        {
            //*** Mappage ***
            var Cycle = _mapperService.Map<SaveCycleResource, Cycle>(SaveCycleResource);
            //*** Creation dans la base de données ***
            var NewCycle = await _CycleService.Create(Cycle);
            //*** Mappage ***
            var CycleResource = _mapperService.Map<Cycle, CycleResource>(NewCycle);
            for (int i=1;i==SaveCycleResource.NbSemaine;i++)
            {
                SaveSectorResource sectorResource = new SaveSectorResource();
                sectorResource.Name = "S" + i;
                sectorResource.Status = 0;
              
                sectorResource.Version = 0;
                var NewSector = _mapperService.Map<SaveSectorResource, Sector>(sectorResource);
                NewSector.UpdatedOn = DateTime.UtcNow;
                NewSector.CreatedOn = DateTime.UtcNow;
                await _SectorService.Create(NewSector);
                SaveSectorCycleResource Affectation = new SaveSectorCycleResource();
                Affectation.IdCycle = NewCycle.IdCycle;
                Affectation.IdSector = NewSector.IdSector;
                var AffectationCycleSector = _mapperService.Map<SaveSectorCycleResource, SectorCycle>(Affectation);
                AffectationCycleSector.UpdatedOn = DateTime.UtcNow;
                AffectationCycleSector.CreatedOn = DateTime.UtcNow;
                await _SectorCycleService.Create(AffectationCycleSector);
            }

     

            return Ok(CycleResource);
        }
        [HttpGet]
        public async Task<ActionResult<CycleResource>> GetAllCycles()
        {
            try
            {
                var Employe = await _CycleService.GetAll();
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
        public async Task<ActionResult<CycleResource>> GetAllActifCycles()
        {
            try
            {
                var Employe = await _CycleService.GetAllActif();
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
        public async Task<ActionResult<CycleResource>> GetAllInactifCycles()
        {
            try
            {
                var Employe = await _CycleService.GetAllInActif();
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
        public async Task<ActionResult<CycleResource>> GetCycleById(int Id)
        {
            try
            {
                var Cycles = await _CycleService.GetById(Id);
                if (Cycles == null) return NotFound();
                var CycleRessource = _mapperService.Map<Cycle, CycleResource>(Cycles);
                return Ok(CycleRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Cycle>> UpdateCycle(int Id, SaveCycleResource SaveCycleResource)
        {

            var CycleToBeModified = await _CycleService.GetById(Id);
            if (CycleToBeModified == null) return BadRequest("Le Cycle n'existe pas"); //NotFound();
            var Cycles = _mapperService.Map<SaveCycleResource, Cycle>(SaveCycleResource);
            //var newCycle = await _CycleService.Create(Cycles);

            await _CycleService.Update(CycleToBeModified, Cycles);

            var CycleUpdated = await _CycleService.GetById(Id);

            var CycleResourceUpdated = _mapperService.Map<Cycle, CycleResource>(CycleUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCycle(int Id)
        {
            try
            {

                var sub = await _CycleService.GetById(Id);
                if (sub == null) return BadRequest("Le Cycle  n'existe pas"); //NotFound();
                await _CycleService.Delete(sub);
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
                List<Cycle> empty = new List<Cycle>();
                foreach (var item in Ids)
                {
                    var sub = await _CycleService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Cycle  n'existe pas"); //NotFound();

                }
                await _CycleService.DeleteRange(empty);
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
