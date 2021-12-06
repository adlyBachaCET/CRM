using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
using CRM.Core.Services;
using CRM_API.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableCors("AllowOrigin")]

    public class CycleController : ControllerBase
    {

        private readonly ICycleService _CycleService;
        private readonly ICycleUserService _CycleUserService;
        private readonly IUserService _UserService;
        private readonly IPotentielCycleService _PotentielCycleService;
        private readonly IPotentielSectorService _PotentielSectorService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IPotentielService _PotentielService;
        private readonly ITargetService _TargetService;

        private readonly ISectorService _SectorService;
        private readonly ISectorCycleService _SectorCycleService;
        private readonly ISectorLocalityService _SectorLocalityService;

        private readonly IMapper _mapperService;
        public CycleController(
            ISectorLocalityService SectorLocalityService,
            IUserService UserService, IPharmacyService PharmacyService,
            IDoctorService DoctorService, ICycleUserService CycleUserService,
            IPotentielCycleService PotentielCycleService, ITargetService TargetService,
              IPotentielService PotentielService, IPotentielSectorService PotentielSectorService,
            ISectorCycleService SectorCycleService, ISectorService SectorService,
            ICycleService CycleService, IMapper mapper)
        {
            _SectorLocalityService = SectorLocalityService;
               _UserService = UserService;
            _PotentielCycleService = PotentielCycleService;
            _PotentielService = PotentielService;
            _TargetService = TargetService;
            _PotentielSectorService = PotentielSectorService;
            _DoctorService = DoctorService;
            _PharmacyService = PharmacyService;

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

            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);

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

                    var SaveCycleUserResource = new PotentielCycle();

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
        string Token, ListOfItemToBeAssigned ListIds)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            var IdUser = int.Parse(claims.FindFirst("Id").Value);


            if (ListIds.Users.Count > 0)
            {
                foreach (var item in ListIds.Users)
                {
                    var Cycle = await _CycleService.GetById(ListIds.Cycle);
                    if (Cycle != null)
                    {
                        var CycleUser = new CycleUser();
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
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// <summary>
        ///  This function Asign Users To a Cycle
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Ids">Id1 id of the cycle Id2 id of the User.</param>
        [HttpPost("AsignSectorsToCycle")]
        public async Task<ActionResult> AsignSectorsToCycle([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, ItemToBeAssigned Ids)
        {
            var random = new Random();
            int num = random.Next();
            var claims = _UserService.getPrincipal(Token);
            var IdUser = int.Parse(claims.FindFirst("Id").Value);

            var Sectors = new List<Sector>();
            var SectorsCreated = new List<Sector>();
            var SectorResources = new List<SectorResource>();

            var User = await _UserService.GetById(Ids.User);
            int numTarget = 0;
            var Cycle = await _CycleService.GetById(Ids.Cycle);
            List<PotentielResource> PotentielResources = new List<PotentielResource>();
            if (Cycle == null) return NotFound();
            if (Cycle != null)
            {
                var Potentiel = await _PotentielCycleService.GetPotentielsById(Cycle.IdCycle);

                foreach (var item in Potentiel)
                {
                    var Bu = _mapperService.Map<Potentiel, PotentielResource>(item);

                    if (Bu != null)
                    {
                        PotentielResources.Add(Bu);
                    }
                }
                for (int i = 0; i < Cycle.NbSemaine; i++)
                {
                    SaveSectorResource SaveSectorResource = new SaveSectorResource();
                    int n = i + 1;
                    SaveSectorResource.Name = "S" + n;

                    var SectorResource = _mapperService.Map<SaveSectorResource, Sector>(SaveSectorResource);

                    SectorResource.Version = 0;
                    SectorResource.Status = 0;
                    SectorResource.CreatedOn = DateTime.UtcNow;
                    SectorResource.UpdatedOn = DateTime.UtcNow;
                    SectorResource.CreatedBy = IdUser;
                    SectorResource.UpdatedBy = IdUser;


                    Sectors.Add(SectorResource);

                }
                if (Sectors.Count > 0)
                {
                    SectorsCreated = await _SectorService.CreateRange(Sectors);
                }


                for (int i = 0; i < SectorsCreated.Count; i++)
                {
                    var Target = new Target();

                    Target.NumTarget = num;
                    var PotentielSector = new PotentielSector();
                    foreach (var item in Potentiel)
                    {
                        PotentielSector.IdPotentiel = item.IdPotentiel;
                        PotentielSector.VersionPotentiel = item.Version;
                        PotentielSector.StatusPotentiel = item.Status;
                        PotentielSector.IdPotentielNavigation = item;
                        PotentielSector.IdSector = SectorsCreated[i].IdSector;
                        PotentielSector.VersionSector = SectorsCreated[i].Version;
                        PotentielSector.StatusSector = SectorsCreated[i].Status;
                        PotentielSector.IdSectorNavigation = SectorsCreated[i];
                        PotentielSector.CreatedOn = DateTime.UtcNow;
                        PotentielSector.UpdatedOn = DateTime.UtcNow;
                        PotentielSector.CreatedBy = IdUser;
                        PotentielSector.UpdatedBy = IdUser;
                        PotentielSector.Active = 0;
                        PotentielSector.Status = 0;
                        PotentielSector.Freq = 0;
                        await _PotentielSectorService.Create(PotentielSector);


                    }
                    var SectorCycle = new SectorCycle();

                    SectorCycle.IdCycle = Cycle.IdCycle;
                    SectorCycle.VersionCycle = Cycle.Version;
                    SectorCycle.StatusCycle = Cycle.Status;
                    SectorCycle.IdCycleNavigation = Cycle;
                    var Sector = await _SectorService.GetById(SectorsCreated[i].IdSector);
                    SectorCycle.IdSector = Sector.IdSector;
                    SectorCycle.VersionSector = Sector.Version;
                    SectorCycle.StatusSector = Sector.Status;
                    SectorCycle.IdSectorNavigation = Sector;
                    SectorCycle.CreatedOn = DateTime.UtcNow;
                    SectorCycle.UpdatedOn = DateTime.UtcNow;
                    SectorCycle.CreatedBy = IdUser;
                    SectorCycle.UpdatedBy = IdUser;

                    await _SectorCycleService.Create(SectorCycle);
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
            var target = await GetTarget(numTarget);
            return Ok(target);

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

            var CycleToBeModified = await _CycleService.GetById(Id);
            if (CycleToBeModified == null) return BadRequest("Le Cycle n'existe pas");
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

            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {
                var claims = _UserService.getPrincipal(Token);


                var Employe = await _CycleService.GetAll();
                if (Employe == null) return NotFound();
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
            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {

                var Employe = await _CycleService.GetAllActif();
                if (Employe == null) return NotFound();
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
            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {
                var Employe = await _CycleService.GetAllInActif();
                if (Employe == null) return NotFound();
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

            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
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
                return Ok(new { Potentiels = PotentielResources, Cycles = CycleRessource });
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
        ///<param name="PassToPlanification">Id of the cycle.</param>
        /// <returns>The cycle with its potentiels.</returns>
        [HttpPost("PassToPlanification")]
        public async Task<ActionResult> c([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, PassToPlanification PassToPlanification)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            int Year = PassToPlanification.Start.Year;
            int Month = PassToPlanification.Start.Month;
            int Day = PassToPlanification.Start.Day;
            List<SectorResource> SectorResources = new List<SectorResource>();

            var currentCulture = CultureInfo.CurrentCulture;
            var WeekNo = currentCulture.Calendar.GetWeekOfYear(
            new DateTime(Year, Month, Day),
            currentCulture.DateTimeFormat.CalendarWeekRule,
            currentCulture.DateTimeFormat.FirstDayOfWeek);
            List<Sector> Sectors = new List<Sector>();
            List<Sector> SectorsCreated = new List<Sector>();
            var Target = await GetTarget(PassToPlanification.NumTarget);

            /*
                        if (PassToPlanification.Occurence != null)
                        {
                            var num = Target.Cycle.NbSemaine * PassToPlanification.Occurence;
                            //Create the new Sectors and add them to a list 
                            for (int i = Target.Cycle.NbSemaine; i < (num - Target.Cycle.NbSemaine) + 1; i++)
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


                                if (Sectors.Count > 0)
                                {
                                    SectorsCreated = await _SectorService.CreateRange(Sectors);

                                }
                            }

                            //Creation of the potentiel for each new sector and assign them to the cycle create them in the target
                            for (int i = 0; i < SectorsCreated.Count; i++)
                            {

                                PotentielSector PotentielSector = new PotentielSector();
                                foreach (var item in Target.PotentielResources)
                                {
                                    PotentielSector.IdPotentiel = item.IdPotentiel;
                                    PotentielSector.VersionPotentiel = item.Version;
                                    PotentielSector.StatusPotentiel = item.Status;
                                    var Potentiel = _mapperService.Map<PotentielResource, Potentiel>(item);

                                    PotentielSector.IdPotentielNavigation = Potentiel;
                                    PotentielSector.IdSector = SectorsCreated[i].IdSector;
                                    PotentielSector.VersionSector = SectorsCreated[i].Version;
                                    PotentielSector.StatusSector = SectorsCreated[i].Status;
                                    PotentielSector.IdSectorNavigation = SectorsCreated[i];
                                    PotentielSector.CreatedOn = DateTime.UtcNow;
                                    PotentielSector.UpdatedOn = DateTime.UtcNow;
                                    PotentielSector.CreatedBy = IdUser;
                                    PotentielSector.UpdatedBy = IdUser;
                                    PotentielSector.Active = 0;
                                    PotentielSector.Status = 0;
                                    PotentielSector.Freq = 0;

                                    var PotentielSectorCreated = await _PotentielSectorService.Create(PotentielSector);


                                }
                                SectorCycle SectorCycle = new SectorCycle();
                                var Cycle = _mapperService.Map<CycleResource, Cycle>(Target.Cycle);

                                SectorCycle.IdCycle = Target.Cycle.IdCycle;
                                SectorCycle.VersionCycle = Target.Cycle.Version;
                                SectorCycle.StatusCycle = Target.Cycle.Status;
                                SectorCycle.IdCycleNavigation = Cycle;
                                var Sector = await _SectorService.GetById(SectorsCreated[i].IdSector);
                                SectorCycle.IdSector = Sector.IdSector;
                                SectorCycle.VersionSector = Sector.Version;
                                SectorCycle.StatusSector = Sector.Status;
                                SectorCycle.IdSectorNavigation = Sector;
                                SectorCycle.CreatedOn = DateTime.UtcNow;
                                SectorCycle.UpdatedOn = DateTime.UtcNow;
                                SectorCycle.CreatedBy = IdUser;
                                SectorCycle.UpdatedBy = IdUser;

                                var SectorCycleCreated = await _SectorCycleService.Create(SectorCycle);
                                var SectorResource = _mapperService.Map<Sector, SectorResource>(SectorsCreated[i]);
                                SectorResources.Add(SectorResource);

                                Target NewTarget = new Target();
                                NewTarget.IdSector = Sector.IdSector;
                                NewTarget.VersionSector = Sector.Version;
                                NewTarget.StatusCycle = Sector.Status;
                                NewTarget.IdSectorNavigation = Sector;
                                NewTarget.IdCycle = Cycle.IdCycle;
                                NewTarget.VersionCycle = Cycle.Version;
                                NewTarget.StatusCycle = Cycle.Status;
                                NewTarget.IdCycleNavigation = Cycle;
                                NewTarget.IdUser = Target.User.IdUser;
                                NewTarget.VersionUser = Target.User.Version;
                                NewTarget.StatusCycle = Target.User.Status;
                                var User = _mapperService.Map<UserResource, User>(Target.User);

                                NewTarget.IdUserNavigation = User;
                                NewTarget.IdDoctor = null;
                                NewTarget.VersionDoctor = null;
                                NewTarget.StatusDoctor = null;
                                NewTarget.IdDoctorNavigation = null;
                                NewTarget.IdPharmacy = null;
                                NewTarget.VersionPharmacy = null;
                                NewTarget.StatusPharmacy = null;
                                NewTarget.IdPharmacyNavigation = null;
                                NewTarget.CreatedOn = DateTime.UtcNow;
                                NewTarget.UpdatedOn = DateTime.UtcNow;
                                NewTarget.CreatedBy = IdUser;
                                NewTarget.UpdatedBy = IdUser;
                                var TargetCreated = await _TargetService.Create(NewTarget);
                                var TargetResource = _mapperService.Map<Target, TargetResource>(TargetCreated);
                                //numTarget = TargetResource.NumTarget;
                            }




                        }

                        if (PassToPlanification.End != null)
                        {
                            string date = ""+ PassToPlanification.End;
                            DateTime inputDate = DateTime.Parse(date.Trim());
                            int Year1 = inputDate.Year;
                            int Month1 = inputDate.Month;
                            int Day1 = inputDate.Day;

                            var weekNo1 = currentCulture.Calendar.GetWeekOfYear(

                           new DateTime(Year1, Month1, Day1),
                           currentCulture.DateTimeFormat.CalendarWeekRule,
                           currentCulture.DateTimeFormat.FirstDayOfWeek);

                            var num = weekNo1-weekNo;
                            //Create the new Sectors and add them to a list 
                            for (int i = Target.Cycle.NbSemaine; i < (num - Target.Cycle.NbSemaine) + 1; i++)
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


                                if (Sectors.Count > 0)
                                {
                                    SectorsCreated = await _SectorService.CreateRange(Sectors);

                                }
                            }

                            //Creation of the potentiel for each new sector and assign them to the cycle create them in the target
                            for (int i = 0; i < SectorsCreated.Count; i++)
                            {

                                PotentielSector PotentielSector = new PotentielSector();
                                foreach (var item in Target.PotentielResources)
                                {
                                    PotentielSector.IdPotentiel = item.IdPotentiel;
                                    PotentielSector.VersionPotentiel = item.Version;
                                    PotentielSector.StatusPotentiel = item.Status;
                                    var Potentiel = _mapperService.Map<PotentielResource, Potentiel>(item);

                                    PotentielSector.IdPotentielNavigation = Potentiel;
                                    PotentielSector.IdSector = SectorsCreated[i].IdSector;
                                    PotentielSector.VersionSector = SectorsCreated[i].Version;
                                    PotentielSector.StatusSector = SectorsCreated[i].Status;
                                    PotentielSector.IdSectorNavigation = SectorsCreated[i];
                                    PotentielSector.CreatedOn = DateTime.UtcNow;
                                    PotentielSector.UpdatedOn = DateTime.UtcNow;
                                    PotentielSector.CreatedBy = IdUser;
                                    PotentielSector.UpdatedBy = IdUser;
                                    PotentielSector.Active = 0;
                                    PotentielSector.Status = 0;
                                    PotentielSector.Freq = 0;

                                    var PotentielSectorCreated = await _PotentielSectorService.Create(PotentielSector);


                                }
                                SectorCycle SectorCycle = new SectorCycle();
                                var Cycle = _mapperService.Map<CycleResource, Cycle>(Target.Cycle);

                                SectorCycle.IdCycle = Target.Cycle.IdCycle;
                                SectorCycle.VersionCycle = Target.Cycle.Version;
                                SectorCycle.StatusCycle = Target.Cycle.Status;
                                SectorCycle.IdCycleNavigation = Cycle;
                                var Sector = await _SectorService.GetById(SectorsCreated[i].IdSector);
                                SectorCycle.IdSector = Sector.IdSector;
                                SectorCycle.VersionSector = Sector.Version;
                                SectorCycle.StatusSector = Sector.Status;
                                SectorCycle.IdSectorNavigation = Sector;
                                SectorCycle.CreatedOn = DateTime.UtcNow;
                                SectorCycle.UpdatedOn = DateTime.UtcNow;
                                SectorCycle.CreatedBy = IdUser;
                                SectorCycle.UpdatedBy = IdUser;

                                var SectorCycleCreated = await _SectorCycleService.Create(SectorCycle);
                                var SectorResource = _mapperService.Map<Sector, SectorResource>(SectorsCreated[i]);
                                SectorResources.Add(SectorResource);

                                Target NewTarget = new Target();
                                NewTarget.IdSector = Sector.IdSector;
                                NewTarget.VersionSector = Sector.Version;
                                NewTarget.StatusCycle = Sector.Status;
                                NewTarget.IdSectorNavigation = Sector;
                                NewTarget.IdCycle = Cycle.IdCycle;
                                NewTarget.VersionCycle = Cycle.Version;
                                NewTarget.StatusCycle = Cycle.Status;
                                NewTarget.IdCycleNavigation = Cycle;
                                NewTarget.IdUser = Target.User.IdUser;
                                NewTarget.VersionUser = Target.User.Version;
                                NewTarget.StatusCycle = Target.User.Status;
                                var User = _mapperService.Map<UserResource, User>(Target.User);

                                NewTarget.IdUserNavigation = User;
                                NewTarget.IdDoctor = null;
                                NewTarget.VersionDoctor = null;
                                NewTarget.StatusDoctor = null;
                                NewTarget.IdDoctorNavigation = null;
                                NewTarget.IdPharmacy = null;
                                NewTarget.VersionPharmacy = null;
                                NewTarget.StatusPharmacy = null;
                                NewTarget.IdPharmacyNavigation = null;
                                NewTarget.CreatedOn = DateTime.UtcNow;
                                NewTarget.UpdatedOn = DateTime.UtcNow;
                                NewTarget.CreatedBy = IdUser;
                                NewTarget.UpdatedBy = IdUser;
                                var TargetCreated = await _TargetService.Create(NewTarget);
                                var TargetResource = _mapperService.Map<Target, TargetResource>(TargetCreated);
                                //numTarget = TargetResource.NumTarget;
                            }


                        }
                       */
            return Ok();

        }     /// <summary>
              ///  This function gets the cycle by id
              /// </summary>
              ///<param name="Token">Token of the connected user to be passed in the header.</param>
              ///<param name="Id">Id of the cycle.</param>
              /// <returns>The cycle with its potentiels.</returns>
        [HttpGet("Targets")]
        public async Task<ActionResult> GetTargetsById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var Targets = await _TargetService.GetTargetsByIdUser(IdUser);
            List<int> TargetIds = new List<int>();
            List<string> CycleNames = new List<string>();

            foreach (var item in Targets)
            {
                TargetIds.Add(item.NumTarget);
                var Cycle = await _CycleService.GetById(item.IdCycle);
                CycleNames.Add(Cycle.Name);

            }

            return Ok(new { TargetResources = TargetIds, CycleNames = CycleNames });

        }
        [HttpGet("AllTargets")]
        public async Task<ActionResult> AllTargets([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {

            var Targets = await _TargetService.GetTargets();
            List<int> TargetIds = new List<int>();
            List<string> CycleNames = new List<string>();

            foreach (var item in Targets)
            {
                TargetIds.Add(item.NumTarget);
                var Cycle = await _CycleService.GetById(item.IdCycle);
                CycleNames.Add(Cycle.Name);

            }

            return Ok(new { TargetResources = TargetIds, CycleNames = CycleNames });

        }
        /// <summary>
        ///  This function gets the cycle by id
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Num">Id of the cycle.</param>
        /// <returns>The cycle with its potentiels.</returns>
        [HttpGet("Target/{Num}")]
        public async Task<ActionResult<CycleResource>> GetTargetByNum([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Num)
        {

            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {
                return Ok(await GetTarget(Num));
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        /// <summary>
        ///  This function Swaps  
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="WeekSwap">NumTarget of the Target.</param>
        [HttpPost("SwapWeeks")]
        public async Task<ActionResult<CycleResource>> SwapWeeks([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, WeekSwap WeekSwap)
        {

            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {
                await _TargetService.WeekSwap(WeekSwap);
                return Ok();
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        /// <summary>
        ///  This function Swaps  
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="WeekDeletion">NumTarget of the Target.</param>
        [HttpPost("WeekDeletion")]
        public async Task<ActionResult<CycleResource>> WeekDeletion([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, WeekDeletion WeekDeletion)
        {

            ErrorHandling ErrorMessag = new ErrorHandling();
            if (Token != "")
            {
                await _TargetService.DeleteWeek(WeekDeletion);
                var Plan = GetTarget(WeekDeletion.NumTarget);
                return Ok(Plan);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }

        /// <summary>
        ///  This function update week 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="WeekUpdate">NumTarget of the Target.</param>
        [HttpPost("UpdateWeek")]
        public async Task<ActionResult<CycleResource>> UpdateWeek([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, WeekUpdate WeekUpdate)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            List<Target> Targets = new List<Target>();
            int NumTarget = WeekUpdate.NumTarget;
            ErrorHandling ErrorMessag = new ErrorHandling();
            var Target = await GetTarget(WeekUpdate.NumTarget);

            if (Token != "")
            {
                WeekDeletion WeekDeletion = new WeekDeletion();
                WeekDeletion.IdSector1 = WeekUpdate.NewContent.IdSector;
                WeekDeletion.NumTarget = WeekUpdate.NumTarget;
                await _TargetService.DeleteWeek(WeekDeletion);

                var Sector = await _SectorService.GetById(WeekUpdate.NewContent.IdSector);
                var User = await _UserService.GetById(Target.User.IdUser);
                var Cycle = await _CycleService.GetById(Target.Cycle.IdCycle);

                foreach (var j in WeekUpdate.NewContent.IdDoctor)
                {
                    var Doctor = await _DoctorService.GetById(j);
                    if (Doctor != null)
                    {
                        var NewTarget = new Target();

                        NewTarget.NumTarget = WeekUpdate.NumTarget;
                        NewTarget.IdSector = Sector.IdSector;
                        NewTarget.VersionSector = Sector.Version;
                        NewTarget.StatusCycle = Sector.Status;
                        NewTarget.IdSectorNavigation = Sector;
                        NewTarget.IdCycle = Cycle.IdCycle;
                        NewTarget.VersionCycle = Cycle.Version;
                        NewTarget.StatusCycle = Cycle.Status;
                        NewTarget.IdCycleNavigation = Cycle;
                        NewTarget.IdUser = User.IdUser;
                        NewTarget.VersionUser = User.Version;
                        NewTarget.StatusCycle = User.Status;
                        NewTarget.IdUserNavigation = User;
                        NewTarget.IdDoctor = Doctor.IdDoctor;
                        NewTarget.VersionDoctor = Doctor.Version;
                        NewTarget.StatusDoctor = Doctor.Status;
                        NewTarget.IdDoctorNavigation = Doctor;
                        NewTarget.IdPharmacy = null;
                        NewTarget.VersionPharmacy = null;
                        NewTarget.StatusPharmacy = null;
                        NewTarget.IdPharmacyNavigation = null;
                        NewTarget.CreatedOn = DateTime.UtcNow;
                        NewTarget.UpdatedOn = DateTime.UtcNow;
                        NewTarget.CreatedBy = IdUser;
                        NewTarget.UpdatedBy = IdUser;
                        var TargetCreated = await _TargetService.Create(NewTarget);

                        Targets.Add(TargetCreated);
                    }
                }
                foreach (var j in WeekUpdate.NewContent.IdPharmacy)
                {
                    var Pharmacy = await _PharmacyService.GetById(j);
                    if (Pharmacy != null)
                    {
                        var NewTarget = new Target();

                        NewTarget.NumTarget = WeekUpdate.NumTarget;
                        NewTarget.IdSector = Sector.IdSector;
                        NewTarget.VersionSector = Sector.Version;
                        NewTarget.StatusCycle = Sector.Status;
                        NewTarget.IdSectorNavigation = Sector;
                        NewTarget.IdCycle = Cycle.IdCycle;
                        NewTarget.VersionCycle = Cycle.Version;
                        NewTarget.StatusCycle = Cycle.Status;
                        NewTarget.IdCycleNavigation = Cycle;
                        NewTarget.IdUser = User.IdUser;
                        NewTarget.VersionUser = User.Version;
                        NewTarget.StatusCycle = User.Status;
                        NewTarget.IdUserNavigation = User;
                        NewTarget.IdDoctor = null;
                        NewTarget.VersionDoctor = null;
                        NewTarget.StatusDoctor = null;
                        NewTarget.IdDoctorNavigation = null;
                        NewTarget.IdPharmacy = Pharmacy.Id;
                        NewTarget.VersionPharmacy = Pharmacy.Version;
                        NewTarget.StatusPharmacy = Pharmacy.Status;
                        NewTarget.IdPharmacyNavigation = Pharmacy;
                        NewTarget.CreatedOn = DateTime.UtcNow;
                        NewTarget.UpdatedOn = DateTime.UtcNow;
                        NewTarget.CreatedBy = IdUser;
                        NewTarget.UpdatedBy = IdUser;
                        var TargetCreated = await _TargetService.Create(NewTarget);

                        Targets.Add(TargetCreated);
                    }








                    //var TargetCreatedResource = _mapperService.Map<Target, TargetResource>(TargetCreated);







                }
                // var Plan = getTarget(WeekDeletion.NumTarget);
                return Ok();
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Week> GetTarget(int Num)
        {
            Week Plan = new Week();
            TargetByNumber TargetByNumber = new TargetByNumber();
            var Target = await _TargetService.GetEmptyTargetByNumTarget(Num);
            //var FullTarget = await _TargetService.GetFuget(Num);

            List<WeekTarget> WeekTarget = new List<WeekTarget>();
            List<TargetResource> TargetResources = new List<TargetResource>();
            List<PotentielResource> PotentielResources = new List<PotentielResource>();
            User User = new User();
            UserResource UserResource = new UserResource();


            List<Sector> Sectors = new List<Sector>();
            List<SectorResource> SectorResources = new List<SectorResource>();
            Cycle Cycle = await _TargetService.GetCycleByNumTarget(Num);
            CycleResource CycleResource = _mapperService.Map<Cycle, CycleResource>(Cycle);
            User = await _TargetService.GetUserByNumTarget(Num);
            UserResource = _mapperService.Map<User, UserResource>(User);
            var PotentielCycle = await _PotentielCycleService.GetPotentielsById(Cycle.IdCycle);
            foreach (var o in PotentielCycle)
            {
                var PotentielResource = _mapperService.Map<Potentiel, PotentielResource>(o);

                if (PotentielResource != null)
                {
                    PotentielResources.Add(PotentielResource);
                }
            }

            var SectorsByTarget = await _TargetService.GetFullSectorsByNumTarget(Num);
            foreach (var item in Target)
            {
                WeekTarget target = new WeekTarget();
                Plan.User = UserResource;
                Plan.Cycle = CycleResource;
                var SectorResource = _mapperService.Map<Sector, SectorResource>(item.IdSectorNavigation);
                Plan.PotentielResourceCycle = PotentielResources;
                target.Sector = SectorResource;
                List<PharmacyResource> Pharmacys = new List<PharmacyResource>();
                List<DoctorResource> Doctors = new List<DoctorResource>();

                var PotentielSector =item.IdSectorNavigation.PotentielSector;
                List<PotentielResource> PotSector = new List<PotentielResource>();
                foreach (var o in PotentielSector)
                {
                    var PotentielResource = _mapperService.Map<Potentiel, PotentielResource>(o.IdPotentielNavigation);

                    if (PotentielResource != null)
                    {
                        PotSector.Add(PotentielResource);
                    }
                }
                List<PotentielResource> PotentielsResources = PotSector;
                PotentielTotal PotentielTotal = new PotentielTotal();
                PotentielTotal.PotentielResourceSector = PotSector;
                target.PotentielTotal = PotentielTotal;
                var ListPharmacy = await _TargetService.GetPharmacysByNumTarget(Num, item.IdSector);
                foreach (var p in ListPharmacy)
                {
                    var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(p);
                    Pharmacys.Add(PharmacyResource);
                }
                target.Pharmacys = Pharmacys;
                var ListDoctor = await _TargetService.GetDoctorsByNumTarget(Num, item.IdSector);
                foreach (var p in ListDoctor)
                {
                    var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(p);
                    Doctors.Add(DoctorResource);
                }
                target.Doctors = Doctors;
                WeekTarget.Add(target);
            }
            Plan.NumTarget = Num;
            Plan.WeekTarget = WeekTarget;
            return Plan;

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

            ErrorHandling ErrorMessag = new ErrorHandling();
            List<CycleResource> CycleResources = new List<CycleResource>();
            if (Token != "")
            {
                var Cycles = await _CycleUserService.GetByIdUser(Id);
                if (Cycles == null) return NotFound();
                foreach (var item in Cycles)
                {
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

            return NoContent();

        }
        /// <summary>
        ///  This function deactivates the cycle By Id
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the cycle.</param>
        [HttpDelete("RemoveAll")]
        public async Task<ActionResult> DeleteAllTargets([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {


            await _TargetService.RemoveAll();

            return NoContent();

        }
        /// <summary>
        ///  This function Create a target
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Target">IdUser idSector idCycle and either {idDoctor or idPharmacy} ifyou choose one the other must be set to null.</param>
        [HttpPost("CreateTarget")]
        public async Task<ActionResult> CreateTarget([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, SaveTargetResource Target)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);

            List<SectorResource> SectorResources = new List<SectorResource>();

            var User = await _UserService.GetById(Target.IdUser);
            var UserResource = _mapperService.Map<User, UserResource>(User);
            int? numTarget = null;
            var Cycle = await _CycleService.GetById(Target.IdCycle);
            List<PotentielResource> PotentielResources = new List<PotentielResource>();
            if (Cycle == null) return NotFound();
            var CycleRessource = _mapperService.Map<Cycle, CycleResource>(Cycle);
            var Doctor = await _DoctorService.GetById(Target.IdDoctor);
            var Pharmacy = await _PharmacyService.GetById(Target.IdPharmacy);
            var Sector = await _SectorService.GetById(Target.IdSector);

            if (Cycle != null)
            {

                var PotentielInDB = await _PotentielCycleService.GetPotentielsById(Cycle.IdCycle);

                foreach (var item in PotentielInDB)
                {
                    var Potentiel = _mapperService.Map<Potentiel, PotentielResource>(item);

                    if (Potentiel != null)
                    {
                        PotentielResources.Add(Potentiel);
                    }
                }


                PotentielSector PotentielSector = new PotentielSector();
                foreach (var item1 in PotentielInDB)
                {
                    PotentielSector.IdPotentiel = item1.IdPotentiel;
                    PotentielSector.VersionPotentiel = item1.Version;
                    PotentielSector.StatusPotentiel = item1.Status;
                    PotentielSector.IdPotentielNavigation = item1;
                    PotentielSector.IdSector = Sector.IdSector;
                    PotentielSector.VersionSector = Sector.Version;
                    PotentielSector.StatusSector = Sector.Status;
                    PotentielSector.IdSectorNavigation = Sector;
                    PotentielSector.CreatedOn = DateTime.UtcNow;
                    PotentielSector.UpdatedOn = DateTime.UtcNow;
                    PotentielSector.CreatedBy = IdUser;
                    PotentielSector.UpdatedBy = IdUser;


                }
                var SectorCycle = new SectorCycle();

                SectorCycle.IdCycle = Cycle.IdCycle;
                SectorCycle.VersionCycle = Cycle.Version;
                SectorCycle.StatusCycle = Cycle.Status;
                SectorCycle.IdCycleNavigation = Cycle;
                SectorCycle.IdSector = Sector.IdSector;
                SectorCycle.VersionSector = Sector.Version;
                SectorCycle.StatusSector = Sector.Status;
                SectorCycle.IdSectorNavigation = Sector;
                SectorCycle.CreatedOn = DateTime.UtcNow;
                SectorCycle.UpdatedOn = DateTime.UtcNow;
                SectorCycle.CreatedBy = IdUser;
                SectorCycle.UpdatedBy = IdUser;

                var SectorResource = _mapperService.Map<Sector, SectorResource>(Sector);
                SectorResources.Add(SectorResource);
                var TargetResource = _mapperService.Map<SaveTargetResource, TargetResource>(Target);

                var NewTarget = _mapperService.Map<TargetResource, Target>(TargetResource);

                NewTarget.IdSector = Sector.IdSector;
                NewTarget.VersionSector = Sector.Version;
                NewTarget.StatusCycle = Sector.Status;
                NewTarget.IdSectorNavigation = Sector;
                NewTarget.IdCycle = Cycle.IdCycle;
                NewTarget.VersionCycle = Cycle.Version;
                NewTarget.StatusCycle = Cycle.Status;
                NewTarget.IdCycleNavigation = Cycle;
                NewTarget.IdUser = User.IdUser;
                NewTarget.VersionUser = User.Version;
                NewTarget.StatusCycle = User.Status;
                NewTarget.IdUserNavigation = User;
                if (Doctor != null)
                {
                    NewTarget.IdDoctor = Doctor.IdDoctor;
                    NewTarget.VersionDoctor = Doctor.Version;
                    NewTarget.StatusDoctor = Doctor.Status;
                    NewTarget.IdDoctorNavigation = Doctor;
                }
                else if (Doctor == null)
                {
                    NewTarget.IdDoctor = null;
                    NewTarget.VersionDoctor = null;
                    NewTarget.StatusDoctor = null;
                    NewTarget.IdDoctorNavigation = null;
                }
                if (Pharmacy != null)
                {
                    NewTarget.IdPharmacy = Pharmacy.Id;
                    NewTarget.VersionPharmacy = Pharmacy.Version;
                    NewTarget.StatusPharmacy = Pharmacy.Status;
                    NewTarget.IdPharmacyNavigation = Pharmacy;

                }
                else if (Pharmacy == null)
                {
                    NewTarget.IdPharmacy = null;
                    NewTarget.VersionPharmacy = null;
                    NewTarget.StatusPharmacy = null;
                    NewTarget.IdPharmacyNavigation = null;
                }
                NewTarget.CreatedOn = DateTime.UtcNow;
                NewTarget.UpdatedOn = DateTime.UtcNow;
                NewTarget.CreatedBy = IdUser;
                NewTarget.UpdatedBy = IdUser;

                var TargetCreated = await _TargetService.Create(NewTarget);
                var TargetCreatedResource = _mapperService.Map<Target, TargetResource>(TargetCreated);
                numTarget = TargetResource.NumTarget;
            }

            return Ok(new
            {
                PotentielOfCycle = PotentielResources,
                Cycle = CycleRessource,
                User = UserResource,
                Sector = SectorResources,
                numTarget = numTarget
            });

        }
        /// <summary>
        ///  This function Create a target
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Target">IdUser idSector idCycle and either {idDoctor or idPharmacy} ifyou choose one the other must be set to null.</param>
        [HttpPost("CreateTargetList")]
        public async Task<ActionResult> CreateTargetList([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, TargetList TargetList)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var Targets = new List<Target>();
            int NumTarget = TargetList.NumTarget;
            var Target = await GetTarget(TargetList.NumTarget);

            if (Target != null)
            {


                foreach (var item in TargetList.WeekContents)
                {
                    List<Doctor> Doctors = new List<Doctor>();
                    var Sector = await _SectorService.GetById(item.IdSector);
                    var User = await _UserService.GetById(Target.User.IdUser);
                    var Cycle = await _CycleService.GetById(Target.Cycle.IdCycle);
                    foreach (var j in item.IdDoctor)
                    {
                        var Doctor = await _DoctorService.GetById(j);
                        if (Doctor != null)
                        {
                            var NewTarget = new Target();

                            NewTarget.NumTarget = TargetList.NumTarget;
                            NewTarget.IdSector = Sector.IdSector;
                            NewTarget.VersionSector = Sector.Version;
                            NewTarget.StatusCycle = Sector.Status;
                            NewTarget.IdSectorNavigation = Sector;
                            NewTarget.IdCycle = Cycle.IdCycle;
                            NewTarget.VersionCycle = Cycle.Version;
                            NewTarget.StatusCycle = Cycle.Status;
                            NewTarget.IdCycleNavigation = Cycle;
                            NewTarget.IdUser = User.IdUser;
                            NewTarget.VersionUser = User.Version;
                            NewTarget.StatusCycle = User.Status;
                            NewTarget.IdUserNavigation = User;
                            NewTarget.IdDoctor = Doctor.IdDoctor;
                            NewTarget.VersionDoctor = Doctor.Version;
                            NewTarget.StatusDoctor = Doctor.Status;
                            NewTarget.IdDoctorNavigation = Doctor;
                            NewTarget.IdPharmacy = null;
                            NewTarget.VersionPharmacy = null;
                            NewTarget.StatusPharmacy = null;
                            NewTarget.IdPharmacyNavigation = null;
                            NewTarget.CreatedOn = DateTime.UtcNow;
                            NewTarget.UpdatedOn = DateTime.UtcNow;
                            NewTarget.CreatedBy = IdUser;
                            NewTarget.UpdatedBy = IdUser;  
                            Doctors.Add(Doctor);

                            var TargetCreated = await _TargetService.Create(NewTarget);
                            Targets.Add(TargetCreated);
                        }


                    }
                    var LocalityOfDoctors = await _DoctorService.GetLocalitiesFromDoctors(Doctors);
                    List<SectorLocality> SectorLocalityList = new List<SectorLocality>();
                    foreach(var Locality in LocalityOfDoctors)
                    {
                        var SectorLocality = new SectorLocality();
                        SectorLocality.Version =0;
                        SectorLocality.Status = 0;
                        SectorLocality.CreatedOn = DateTime.UtcNow;
                        SectorLocality.UpdatedOn = DateTime.UtcNow;
                        SectorLocality.CreatedBy = IdUser;
                        SectorLocality.UpdatedBy = IdUser;
                        SectorLocality.IdSector =Sector.IdSector;
                        SectorLocality.VersionSector = Sector.Version;
                        SectorLocality.StatusSector = Sector.Status;
                        SectorLocality.IdSectorNavigation = Sector;
                        SectorLocality.IdLocality = Locality.IdLocality;
                        SectorLocality.VersionLocality = Locality.Version;
                        SectorLocality.StatusLocality = Locality.Status;
                        SectorLocality.IdLocalityNavigation = Locality;
                        if (!SectorLocalityList.Contains(SectorLocality))
                        {
                            SectorLocalityList.Add(SectorLocality);
                        }
                    }

                    var SectorLocalityListCreated = await _SectorLocalityService.CreateRange(SectorLocalityList);

                    foreach (var j in item.IdPharmacy)
                    {
                        var Pharmacy = await _PharmacyService.GetById(j);
                        if (Pharmacy != null)
                        {
                            var NewTarget = new Target();

                            NewTarget.NumTarget = TargetList.NumTarget;
                            NewTarget.IdSector = Sector.IdSector;
                            NewTarget.VersionSector = Sector.Version;
                            NewTarget.StatusCycle = Sector.Status;
                            NewTarget.IdSectorNavigation = Sector;
                            NewTarget.IdCycle = Cycle.IdCycle;
                            NewTarget.VersionCycle = Cycle.Version;
                            NewTarget.StatusCycle = Cycle.Status;
                            NewTarget.IdCycleNavigation = Cycle;
                            NewTarget.IdUser = User.IdUser;
                            NewTarget.VersionUser = User.Version;
                            NewTarget.StatusCycle = User.Status;
                            NewTarget.IdUserNavigation = User;
                            NewTarget.IdDoctor = null;
                            NewTarget.VersionDoctor = null;
                            NewTarget.StatusDoctor = null;
                            NewTarget.IdDoctorNavigation = null;
                            NewTarget.IdPharmacy = Pharmacy.Id;
                            NewTarget.VersionPharmacy = Pharmacy.Version;
                            NewTarget.StatusPharmacy = Pharmacy.Status;
                            NewTarget.IdPharmacyNavigation = Pharmacy;
                            NewTarget.CreatedOn = DateTime.UtcNow;
                            NewTarget.UpdatedOn = DateTime.UtcNow;
                            NewTarget.CreatedBy = IdUser;
                            NewTarget.UpdatedBy = IdUser;
                            var TargetCreated = await _TargetService.Create(NewTarget);

                            Targets.Add(TargetCreated);
                        }
                    }
                    foreach (var Locality in LocalityOfDoctors)
                    {
                        var SectorLocality = new SectorLocality();
                        SectorLocality.Version = 0;
                        SectorLocality.Status = 0;
                        SectorLocality.CreatedOn = DateTime.UtcNow;
                        SectorLocality.UpdatedOn = DateTime.UtcNow;
                        SectorLocality.CreatedBy = IdUser;
                        SectorLocality.UpdatedBy = IdUser;
                        SectorLocality.IdSector = Sector.IdSector;
                        SectorLocality.VersionSector = Sector.Version;
                        SectorLocality.StatusSector = Sector.Status;
                        SectorLocality.IdSectorNavigation = Sector;
                        SectorLocality.IdLocality = Locality.IdLocality;
                        SectorLocality.VersionLocality = Locality.Version;
                        SectorLocality.StatusLocality = Locality.Status;
                        SectorLocality.IdLocalityNavigation = Locality;
                        if (!SectorLocalityList.Contains(SectorLocality))
                        { 
                            SectorLocalityList.Add(SectorLocality); }
                    }
                }
            }


            var Plan = await GetTarget(NumTarget);
            return Ok(Plan);
        }
    }
}
