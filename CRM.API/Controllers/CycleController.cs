using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableCors("AllowOrigin")]

    public class CycleController : ControllerBase
    {
        public IList<Cycle> Cycles;

        private readonly ICycleService _CycleService;
        private readonly ICycleUserService _CycleUserService;
        private readonly IUserService _UserService;

        private readonly ISectorService _SectorService;
        private readonly ISectorCycleService _SectorCycleService;

        private readonly IMapper _mapperService;
        public CycleController(IUserService UserService, ICycleUserService CycleUserService,ISectorCycleService SectorCycleService,ISectorService SectorService,ICycleService CycleService, IMapper mapper)
        {
            _UserService = UserService;

            _CycleUserService = CycleUserService;
               _SectorCycleService = SectorCycleService;
            _SectorService = SectorService;
            _CycleService = CycleService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<CycleResource>> CreateCycle(AffectationCycleUser AffectationCycleUser)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

                //*** Mappage ***
                var Cycle = _mapperService.Map<SaveCycleResource, Cycle>(AffectationCycleUser.SaveCycleResource);
            //*** Creation dans la base de données ***
            var NewCycle = await _CycleService.Create(Cycle);
            //*** Mappage ***
            var CycleResource = _mapperService.Map<Cycle, CycleResource>(NewCycle);

            for (int i=1;i== AffectationCycleUser.SaveCycleResource.NbSemaine;i++)
            {
                SaveSectorResource sectorResource = new SaveSectorResource();
                sectorResource.Name = "S" + i;
                sectorResource.Status = 0;
              
                sectorResource.Version = 0;
                var NewSector = _mapperService.Map<SaveSectorResource, Sector>(sectorResource);
                NewSector.UpdatedOn = DateTime.UtcNow;
                NewSector.CreatedOn = DateTime.UtcNow;
                NewSector.CreatedBy = Id;
                NewSector.UpdatedBy = Id;
                await _SectorService.Create(NewSector);
                SaveSectorCycleResource Affectation = new SaveSectorCycleResource();
                Affectation.IdCycle = CycleResource.IdCycle;
                Affectation.IdSector = NewSector.IdSector;
                var AffectationCycleSector = _mapperService.Map<SaveSectorCycleResource, SectorCycle>(Affectation);
                AffectationCycleSector.UpdatedOn = DateTime.UtcNow;
                AffectationCycleSector.CreatedOn = DateTime.UtcNow;
                await _SectorCycleService.Create(AffectationCycleSector);
                foreach (var item in AffectationCycleUser.Ids)
                {
                    SaveCycleUserResource SaveCycleUserResource = new SaveCycleUserResource();
                    SaveCycleUserResource.IdCycle = CycleResource.IdCycle;
                    SaveCycleUserResource.IdUser = item;
                    var CycleUser = _mapperService.Map<SaveCycleUserResource, CycleUser>(SaveCycleUserResource);
                    CycleUser.CreatedOn = DateTime.UtcNow;
                    CycleUser.UpdatedOn = DateTime.UtcNow;
                    await _CycleUserService.Create(CycleUser);
            }
            }
            return Ok(CycleResource);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpGet]
        public async Task<ActionResult<CycleResource>> GetAllCycles()
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);


                var Employe = await _CycleService.GetAll();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
            }
        [HttpGet("Actif")]
        public async Task<ActionResult<CycleResource>> GetAllActifCycles()
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

                var Employe = await _CycleService.GetAllActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<CycleResource>> GetAllInactifCycles()
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var Employe = await _CycleService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CycleResource>> GetCycleById(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var Cycles = await _CycleService.GetById(Id);
                if (Cycles == null) return NotFound();
                var CycleRessource = _mapperService.Map<Cycle, CycleResource>(Cycles);
                return Ok(CycleRessource);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<CycleResource>> UpdateCycle(int Id, SaveCycleResource SaveCycleResource)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var CycleToBeModified = await _CycleService.GetById(Id);
            if (CycleToBeModified == null) return BadRequest("Le Cycle n'existe pas"); //NotFound();
            var Cycles = _mapperService.Map<SaveCycleResource, Cycle>(SaveCycleResource);
            //var newCycle = await _CycleService.Create(Cycles);

            await _CycleService.Update(CycleToBeModified, Cycles);

            var CycleUpdated = await _CycleService.GetById(Id);

            var CycleResourceUpdated = _mapperService.Map<Cycle, CycleResource>(CycleUpdated);

            return Ok(CycleResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

    }
}


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCycle(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {

                var sub = await _CycleService.GetById(Id);
                if (sub == null) return BadRequest("Le Cycle  n'existe pas"); //NotFound();
                await _CycleService.Delete(sub);
                ;
                return NoContent();
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange(List<int> Ids)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

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
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
    }
}
