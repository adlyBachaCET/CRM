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
using System.ComponentModel.DataAnnotations;
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
        private readonly IPotentielCycleService _PotentielCycleService;
        private readonly IPotentielService _PotentielService;

        private readonly ISectorService _SectorService;
        private readonly ISectorCycleService _SectorCycleService;

        private readonly IMapper _mapperService;
        public CycleController(IUserService UserService, ICycleUserService CycleUserService, 
            IPotentielCycleService PotentielCycleService,
              IPotentielService PotentielService,
            ISectorCycleService SectorCycleService,ISectorService SectorService,
            ICycleService CycleService, IMapper mapper)
        {
            _UserService = UserService;
            _PotentielCycleService = PotentielCycleService;
            _PotentielService = PotentielService;

            _CycleUserService = CycleUserService;
               _SectorCycleService = SectorCycleService;
            _SectorService = SectorService;
            _CycleService = CycleService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<CycleResource>> CreateCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, AffectationCycleUser AffectationCycleUser)
        {
            ErrorHandling ErrorMessag = new ErrorHandling();
          
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

                //*** Mappage ***
                var Cycle = _mapperService.Map<SaveCycleResource, Cycle>(AffectationCycleUser.SaveCycleResource);
            Cycle.CreatedOn = DateTime.UtcNow;
            Cycle.UpdatedOn = DateTime.UtcNow;
            Cycle.CreatedBy = Id;
            Cycle.UpdatedBy = Id;
            //*** Creation dans la base de données ***
            var NewCycle = await _CycleService.Create(Cycle);
            //*** Mappage ***
            var CycleResource = _mapperService.Map<Cycle, CycleResource>(NewCycle);
            for (int i=0;i< AffectationCycleUser.SaveCycleResource.NbSemaine;i++)
            {
                SaveSectorResource sectorResource = new SaveSectorResource();
                sectorResource.Name = "S" + i;
              sectorResource.Description = "";

                var NewSector = _mapperService.Map<SaveSectorResource, Sector>(sectorResource);
                NewSector.UpdatedOn = DateTime.UtcNow;
                NewSector.CreatedOn = DateTime.UtcNow;
                NewSector.CreatedBy = Id;
                NewSector.UpdatedBy = Id;
                NewSector.Status = 0;
                    // sectorResource.
                NewSector.Version = 0;
                    await _SectorService.Create(NewSector);
                SaveSectorCycleResource Affectation = new SaveSectorCycleResource();
                Affectation.IdCycle = CycleResource.IdCycle;
                Affectation.IdSector = NewSector.IdSector;
                var AffectationCycleSector = _mapperService.Map<SaveSectorCycleResource, SectorCycle>(Affectation);
                AffectationCycleSector.UpdatedOn = DateTime.UtcNow;
                AffectationCycleSector.CreatedOn = DateTime.UtcNow;
                AffectationCycleSector.CreatedBy = Id;
               AffectationCycleSector.UpdatedBy = Id;
              var CreatedCycle= await _SectorCycleService.Create(AffectationCycleSector);
               
                }
            if (AffectationCycleUser.Ids.Count > 0)
            {
                foreach (var item in AffectationCycleUser.Ids)
                {
                    
                    SaveCycleUserResource SaveCycleUserResource = new SaveCycleUserResource();
                    SaveCycleUserResource.IdCycle = CycleResource.IdCycle;
                    SaveCycleUserResource.IdUser = item;
                    var CycleUser = _mapperService.Map<SaveCycleUserResource, CycleUser>(SaveCycleUserResource);
                    CycleUser.CreatedOn = DateTime.UtcNow;
                    CycleUser.UpdatedOn = DateTime.UtcNow;
                    CycleUser.CreatedBy = Id;
                    CycleUser.UpdatedBy = Id;
                    await _CycleUserService.Create(CycleUser);
                }
            }
            if (AffectationCycleUser.CyclePotentiel.Count > 0)
            {
                foreach (var item in AffectationCycleUser.CyclePotentiel)
                {
                    var Potentiel = await _PotentielService.GetById(item.IdPotentiel);
                 //   var Potentiel = await _PotentielService.GetById(item.IdPotentiel);

                    PotentielCycle SaveCycleUserResource = new PotentielCycle();

                    SaveCycleUserResource.IdCycle = CycleResource.IdCycle;
                    SaveCycleUserResource.VersionCycle = CycleResource.Version;
                    SaveCycleUserResource.StatusCycle = CycleResource.Status;
                    SaveCycleUserResource.IdCycleNavigation = NewCycle;
                    SaveCycleUserResource.IdPotentiel = Potentiel.IdPotentiel;
                    SaveCycleUserResource.VersionPotentiel = Potentiel.Version;
                    SaveCycleUserResource.StatusPotentiel = Potentiel.Status;
                    SaveCycleUserResource.IdPotentielNavigation = Potentiel;

                    SaveCycleUserResource.CreatedOn = DateTime.UtcNow;
                    SaveCycleUserResource.UpdatedOn = DateTime.UtcNow;
                    SaveCycleUserResource.CreatedBy = Id;
                    SaveCycleUserResource.UpdatedBy = Id;
                    SaveCycleUserResource.Freq = item.Frequence;
                    await _PotentielCycleService.Create(SaveCycleUserResource);
                }
            }
                return Ok(CycleResource);
         
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<CycleResource>> UpdateCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, AffectationCycleUser AffectationCycleUser)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            ErrorHandling ErrorMessag = new ErrorHandling();

            var CycleToBeModified = await _CycleService.GetById(Id);
            if (CycleToBeModified == null) return BadRequest("Le Cycle n'existe pas"); //NotFound();
            var Cycles = _mapperService.Map<SaveCycleResource, Cycle>(AffectationCycleUser.SaveCycleResource);
            if (AffectationCycleUser.SaveCycleResource.NbSemaine > CycleToBeModified.NbSemaine)
            {
                for (int i = CycleToBeModified.NbSemaine; i < AffectationCycleUser.SaveCycleResource.NbSemaine + 1; i++)
                {
                    SaveSectorResource sectorResource = new SaveSectorResource();
                    sectorResource.Name = "S" + i;
                    sectorResource.Description = "";

                    var NewSector = _mapperService.Map<SaveSectorResource, Sector>(sectorResource);
                    NewSector.UpdatedOn = DateTime.UtcNow;
                    NewSector.CreatedOn = DateTime.UtcNow;
                    NewSector.CreatedBy = IdUser;
                    NewSector.UpdatedBy = IdUser;
                    NewSector.Status = 0;
                    // sectorResource.
                    NewSector.Version = 0;
                    await _SectorService.Create(NewSector);
                    SaveSectorCycleResource Affectation = new SaveSectorCycleResource();
                    Affectation.IdCycle = Id;
                    Affectation.IdSector = NewSector.IdSector;
                    var AffectationCycleSector = _mapperService.Map<SaveSectorCycleResource, SectorCycle>(Affectation);
                    AffectationCycleSector.UpdatedOn = DateTime.UtcNow;
                    AffectationCycleSector.CreatedOn = DateTime.UtcNow;
                    AffectationCycleSector.CreatedBy = IdUser;
                    AffectationCycleSector.UpdatedBy = IdUser;
                    var CreatedCycle = await _SectorCycleService.Create(AffectationCycleSector);

                }
            }
            else
            {
                for (int i = 0; i < AffectationCycleUser.SaveCycleResource.NbSemaine; i++)
                { 
                
                }

                }
            await _CycleService.Update(CycleToBeModified, Cycles);
            var CycleUpdated = await _CycleService.GetById(Id);
            var CycleUpdatedO = await _CycleService.GetById(Id);


            foreach (var item in AffectationCycleUser.Ids)
            {
                SaveCycleUserResource SaveCycleUserResource = new SaveCycleUserResource();
                SaveCycleUserResource.IdCycle = Id;
                SaveCycleUserResource.IdUser = item;
                var CycleUser = _mapperService.Map<SaveCycleUserResource, CycleUser>(SaveCycleUserResource);
                CycleUser.CreatedOn = DateTime.UtcNow;
                CycleUser.UpdatedOn = DateTime.UtcNow;
                CycleUser.CreatedBy = IdUser;
                CycleUser.UpdatedBy = IdUser;
                var CycleUserInDB = await _CycleUserService.GetByIdCycleUser(Id, item);
                if (CycleUserInDB == null)
                {
                    await _CycleUserService.Create(CycleUser);
                }
            }
            return Ok(CycleUpdatedO);

        }


        [HttpGet]
        public async Task<ActionResult<CycleResource>> GetAllCycles([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
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
        public async Task<ActionResult<CycleResource>> GetAllActifCycles([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
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
        public async Task<ActionResult<CycleResource>> GetAllInactifCycles([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
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
        public async Task<ActionResult<CycleResource>> GetCycleById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
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
        [HttpGet("CyclesByUser/{Id}")]
        public async Task<ActionResult<List<CycleResource>>> GetCyclesByUserById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            List<CycleResource> CycleResources = new List<CycleResource>();
            if (token != "")
            {
                var Cycles = await _CycleUserService.GetByIdUser(Id);
                if (Cycles == null) return NotFound();
                foreach(var item in Cycles) { 
                var CycleResource = _mapperService.Map<Cycle, CycleResource>(item);
                    CycleResources.Add(CycleResource);
                }
                return Ok(CycleResources);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
   
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
      

                var sub = await _CycleService.GetById(Id);
                if (sub == null) return BadRequest("Le Cycle  n'existe pas"); //NotFound();
                await _CycleService.Delete(sub);
                ;
                return NoContent();
   
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, List<int> Ids)
        {
            ErrorHandling ErrorMessag = new ErrorHandling();
       
         
                var claims = _UserService.getPrincipal(Token);
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
    }
}
