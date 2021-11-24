using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
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
        private readonly IPotentielSectorService _PotentielSectorService;

        private readonly IPotentielService _PotentielService;
        private readonly ITargetService _TargetService;

        private readonly ISectorService _SectorService;
        private readonly ISectorCycleService _SectorCycleService;

        private readonly IMapper _mapperService;
        public CycleController(IUserService UserService, ICycleUserService CycleUserService, 
            IPotentielCycleService PotentielCycleService, ITargetService TargetService,
              IPotentielService PotentielService, IPotentielSectorService PotentielSectorService,
            ISectorCycleService SectorCycleService,ISectorService SectorService,
            ICycleService CycleService, IMapper mapper)
        {
            _UserService = UserService;
            _PotentielCycleService = PotentielCycleService;
            _PotentielService = PotentielService;
            _TargetService = TargetService;
            _PotentielSectorService = PotentielSectorService;

            _CycleUserService = CycleUserService;
               _SectorCycleService = SectorCycleService;
            _SectorService = SectorService;
            _CycleService = CycleService;
            _mapperService = mapper;
        }

        /// <summary>
        ///  This function creates a cycle.
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="AffectationCycleUser">Data of the cycle and the list of potentiel with its frequency.</param>
        /// <returns>returns the created cycle.</returns>
        [HttpPost]
        public async Task<ActionResult<CycleResource>> CreateCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, AffectationCycleUser AffectationCycleUser)
        {
            ErrorHandling ErrorMessag = new ErrorHandling();
          
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            //*** Mappage ***
            var Cycle = _mapperService.Map<SaveCycleResource, Cycle>(AffectationCycleUser.SaveCycleResource);
            Cycle.CreatedOn = DateTime.UtcNow;
            Cycle.UpdatedOn = DateTime.UtcNow;
            Cycle.CreatedBy = Id;
            Cycle.UpdatedBy = Id;
            if (Role == "Manager")
            {
                Cycle.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Cycle.Status = Status.Pending;
            }
            //*** Creation dans la base de données ***
            var NewCycle = await _CycleService.Create(Cycle);
            //*** Mappage ***
            var CycleResource = _mapperService.Map<Cycle, CycleResource>(NewCycle);
        
      
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
        /// <summary>
        ///  This function Asign Users To a Cycle
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="ListIds">Id of the cycle and the List of ids of the users to be assigned.</param>
        [HttpPost("AsignUsersToCycle")]
        public async Task<ActionResult> AsignUsersToCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, ListIds ListIds)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            ErrorHandling ErrorMessag = new ErrorHandling();

            if (ListIds.Ids.Count > 0)
            {
                foreach (var item in ListIds.Ids)
                {
                    var Cycle = await _CycleService.GetById(ListIds.Id);
                    if (Cycle != null)
                    {
                        CycleUser CycleUser = new CycleUser();
                        CycleUser.IdCycle = Cycle.IdCycle;
                        CycleUser.VersionCycle = Cycle.Version;
                        CycleUser.StatusCycle = Cycle.Status;
                        CycleUser.Cycle = Cycle;
                        var User = await _UserService.GetById(item);
                        CycleUser.IdUser = item;
                        CycleUser.VersionUser = User.Version;
                        CycleUser.StatusUser = User.Status;
                        CycleUser.User = User;
                        CycleUser.CreatedOn = DateTime.UtcNow;
                        CycleUser.UpdatedOn = DateTime.UtcNow;
                        CycleUser.CreatedBy = IdUser;
                        CycleUser.UpdatedBy = IdUser;
                        var CycleUserExist = await _CycleUserService.GetByIdCycleUser(Cycle.IdCycle, item);
                        if (CycleUserExist == null)
                        {
                            await _CycleUserService.Create(CycleUser);
                        }
                    }
                }
            }

            return Ok();

        }
        /// <summary>
        ///  This function Asign Users To a Cycle
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Ids">Id1 id of the cycle Id2 id of the User.</param>
        [HttpPost("AsignSectorsToCycle")]
        public async Task<ActionResult> AsignSectorsToCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, UniqueId Ids)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            ErrorHandling ErrorMessag = new ErrorHandling();
            List<Sector> Sectors = new List<Sector>();
            List<Sector> SectorsCreated = new List<Sector>();
            List<SectorResource> SectorResources = new List<SectorResource>();

            SectorCycle SectorCycleCreated = new SectorCycle();
            var User = await _UserService.GetById(Ids.Id2);
            var UserResource = _mapperService.Map<User, UserResource>(User);
            int? numTarget = null; ;
            var Cycle = await _CycleService.GetById(Ids.Id1);
            List<PotentielResource> PotentielResources = new List<PotentielResource>();
      
            if (Cycle == null) return NotFound();
            var CycleRessource = _mapperService.Map<Cycle, CycleResource>(Cycle);
            if (Cycle != null)
                    {
                var PotentielCycle = await _PotentielCycleService.GetPotentielsById(Cycle.IdCycle);

                foreach (var item in PotentielCycle)
                {
                    var Bu = _mapperService.Map<Potentiel, PotentielResource>(item);

                    if (Bu != null)
                    {
                        PotentielResources.Add(Bu);
                    }
                }
                for (int i = 0; i < Cycle.NbSemaine; i++)
                {
                    Sector Sector = new Sector();
                    Sector.Version = 0;
                    Sector.Status = 0;
                    Sector.CreatedOn = DateTime.UtcNow;
                    Sector.UpdatedOn = DateTime.UtcNow;
                    Sector.CreatedBy = IdUser;
                    Sector.UpdatedBy = IdUser;
                    int n = i + 1;
                    Sector.Name = "S" + n;

                    Sectors.Add(Sector);
                  
                }
                if (Sectors.Count > 0)
                {
                    SectorsCreated = await _SectorService.CreateRange(Sectors);

                }

                var Target = new Target();
                Random random = new Random();
                int num = random.Next();
                Target.NumTarget = num;
                for (int i = 0; i < SectorsCreated.Count; i++)
                {
                    SectorCycle SectorCycle = new SectorCycle();

                    SectorCycle.IdCycle = Cycle.IdCycle;
                    SectorCycle.VersionCycle = Cycle.Version;
                    SectorCycle.StatusCycle = Cycle.Status;
                    SectorCycle.IdCycleNavigation = Cycle;
                    var Sector = await _SectorService.GetById(SectorsCreated[i].IdSector);
                    SectorCycle.IdSector = Sector.IdSector;
                    SectorCycle.VersionSector = Sector .Version;
                    SectorCycle.StatusSector = Sector.Status;
                    SectorCycle.IdSectorNavigation = Sector;
                    SectorCycle.CreatedOn = DateTime.UtcNow;
                    SectorCycle.UpdatedOn = DateTime.UtcNow;
                    SectorCycle.CreatedBy = IdUser;
                    SectorCycle.UpdatedBy = IdUser;

                    SectorCycleCreated=await _SectorCycleService.Create(SectorCycle);
                    var SectorResource = _mapperService.Map<Sector, SectorResource>(SectorsCreated[i]);
                    SectorResources.Add(SectorResource);
                

                    Target.IdSector = Sector.IdSector;
                    Target.VersionSector = Sector.Version;
                    Target.StatusCycle = Sector.Status;
                    Target.IdSectorNavigation = Sector;
                    Target.IdCycle = Cycle.IdCycle;
                    Target.VersionCycle = Cycle.Version;
                    Target.StatusCycle = Cycle.Status;
                    Target.IdCycleNavigation = Cycle;
                    Target.IdUser = User.IdUser;
                    Target.VersionUser = User.Version;
                    Target.StatusCycle = User.Status;
                    Target.IdUserNavigation = User;
                    Target.IdDoctor = null;
                    Target.VersionDoctor = null;
                    Target.StatusDoctor = null;
                    Target.IdDoctorNavigation = null;
                    Target.IdPharmacy = null;
                    Target.VersionPharmacy = null;
                    Target.StatusPharmacy = null;
                    Target.IdPharmacyNavigation = null;
                    Target.CreatedOn = DateTime.UtcNow;
                    Target.UpdatedOn = DateTime.UtcNow;
                    Target.CreatedBy = IdUser;
                    Target.UpdatedBy = IdUser;
                    var TargetCreated = await _TargetService.Create(Target);
                    var TargetResource = _mapperService.Map<Target, TargetResource>(TargetCreated);
                    numTarget = TargetResource.NumTarget;
                }


            }

            return Ok(new { PotentielResources= PotentielResources ,
                CycleRessource = CycleRessource,
                UserResource= UserResource,
                SectorResources= SectorResources,
                numTarget= numTarget
            });

        }

        /// <summary>
        ///  This function is used to Update a Cycle
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the cycle.</param>
        /// <param name="AffectationCycleUser">Data of the cycle and the list of potentiel with the frequency.</param>
        [HttpPut("{Id}")]
        public async Task<ActionResult<CycleResource>> UpdateCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, AffectationCycleUser AffectationCycleUser)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            ErrorHandling ErrorMessag = new ErrorHandling();

            var CycleToBeModified = await _CycleService.GetById(Id);
            if (CycleToBeModified == null) return BadRequest("Le Cycle n'existe pas"); //NotFound();
            var Cycles = _mapperService.Map<SaveCycleResource, Cycle>(AffectationCycleUser.SaveCycleResource);
            Cycles.CreatedOn = CycleToBeModified.CreatedOn;
            Cycles.UpdatedOn = DateTime.UtcNow;
            Cycles.CreatedBy = CycleToBeModified.CreatedBy;
            Cycles.UpdatedBy = Id;
            if (Role == "Manager")
            {
                Cycles.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Cycles.Status = Status.Pending;
            }
            await _CycleService.Update(CycleToBeModified, Cycles);
            var CycleUpdated = await _CycleService.GetById(Id);
           
            if (AffectationCycleUser.CyclePotentiel.Count > 0)
            {
                foreach (var item in AffectationCycleUser.CyclePotentiel)
                {
                    var Potentiel = await _PotentielService.GetById(item.IdPotentiel);
                    //   var Potentiel = await _PotentielService.GetById(item.IdPotentiel);
                    if (Potentiel != null)
                    {
                        PotentielCycle SavePotentielCyclerResource = new PotentielCycle();

                        SavePotentielCyclerResource.IdCycle = CycleUpdated.IdCycle;
                        SavePotentielCyclerResource.VersionCycle = CycleUpdated.Version;
                        SavePotentielCyclerResource.StatusCycle = CycleUpdated.Status;
                        SavePotentielCyclerResource.IdCycleNavigation = CycleUpdated;
                        SavePotentielCyclerResource.IdPotentiel = Potentiel.IdPotentiel;
                        SavePotentielCyclerResource.VersionPotentiel = Potentiel.Version;
                        SavePotentielCyclerResource.StatusPotentiel = Potentiel.Status;
                        SavePotentielCyclerResource.IdPotentielNavigation = Potentiel;

                        SavePotentielCyclerResource.CreatedOn = DateTime.UtcNow;
                        SavePotentielCyclerResource.UpdatedOn = DateTime.UtcNow;
                        SavePotentielCyclerResource.CreatedBy = Id;
                        SavePotentielCyclerResource.UpdatedBy = Id;
                        SavePotentielCyclerResource.Freq = item.Frequence;
                        var PotentielCycleExist = await _PotentielCycleService.GetByIdPotentielCycle(item.IdPotentiel, CycleUpdated.IdCycle);
                        if (PotentielCycleExist == null)
                        {
                            await _PotentielCycleService.Create(SavePotentielCyclerResource);
                        }
                    }
                }
            }


            return Ok(CycleUpdated);

        }

        /// <summary>
        ///  This function gets the list of all cycles
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the list of all cycle.</returns>
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
        /// <summary>
        ///  This function gets the list of all actif cycles
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the list of all cycle.</returns>
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
        /// <summary>
        ///  This function gets the list of all inactif cycles
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the list of all cycle.</returns>
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

        /// <summary>
        ///  This function gets the cycle by id
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the cycle.</param>
        /// <returns>The cycle with its potentiels.</returns>
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
                var PotentielCycle = await _PotentielCycleService.GetPotentielsById(Id);
                List<PotentielResource> PotentielResources = new List<PotentielResource>();
                foreach (var item in PotentielCycle)
                {
                    var Bu = _mapperService.Map<Potentiel, PotentielResource>(item);

                    if (Bu != null)
                    {
                        PotentielResources.Add(Bu);
                    }
                }
                if (Cycles == null) return NotFound();
                var CycleRessource = _mapperService.Map<Cycle, CycleResource>(Cycles);
                return Ok(new { Potentiels= PotentielResources, Cycles= CycleRessource });
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        /// <summary>
        ///  This function gets the Cycle By User By Id
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the user.</param>
        /// <returns>The list of cycles of the user.</returns>
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
        /// <summary>
        ///  This function deactivates the cycle By Id
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the cycle.</param>
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
   }
}
