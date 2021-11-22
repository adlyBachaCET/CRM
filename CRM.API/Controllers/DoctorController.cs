using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
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
    [EnableCors("AllowOrigin")]

    public class DoctorController : ControllerBase
    {
        public IList<Doctor> Doctors;

        private readonly IDoctorService _DoctorService;
        private readonly IVisitReportService _VisitReportService;

        private readonly IBusinessUnitService _BusinessUnitService;
        private readonly IRequestRpService _RequestRpService;
        private readonly ICommandeService _CommandeService;

        private readonly IBuDoctorService _BuDoctorService;
        private readonly IServiceService _ServiceService;
        private readonly IParticipantService _ParticipantService;

        private readonly ILocationDoctorService _LocationDoctorService;
        private readonly ILocationService _LocationService;
        private readonly IPhoneService _PhoneService;
        private readonly IUserService _UserService;
        private readonly ILocationTypeService _LocationTypeService;

        private readonly ITagsDoctorService _TagsDoctorService;
        private readonly ITagsService _TagsService;
        private readonly IObjectionService _ObjectionService;
        private readonly IRequestDoctorService _RequestDoctorService;

        private readonly IInfoService _InfoService;
        private readonly IPotentielService _PotentielService;
        private readonly ISpecialtyService _SpecialtyService;
        private readonly IMapper _mapperService;
        public DoctorController(ILocationDoctorService LocationDoctorService,
            IPhoneService PhoneService,
            IUserService UserService,

            ILocationService LocationService,
                        ILocationTypeService LocationTypeService,

            IServiceService ServiceService,
            IDoctorService DoctorService, IRequestDoctorService RequestDoctorService,
            IPotentielService PotentielService, IObjectionService ObjectionService,
            ISpecialtyService SpecialtyService,
            IBusinessUnitService BusinessUnitService,
                        IParticipantService ParticipantService,
                                    IRequestRpService RequestRpService,
  ICommandeService CommandeService,
            IInfoService InfoService,
            ITagsService TagsService,
            ITagsDoctorService TagsDoctorService,
            IVisitReportService VisitReportService,
            IBuDoctorService BuDoctorService, IMapper mapper)
        {
            _VisitReportService = VisitReportService;
            _TagsDoctorService = TagsDoctorService;
            _LocationDoctorService = LocationDoctorService;
            _LocationService = LocationService;
            _TagsService = TagsService;
            _PotentielService = PotentielService;
            _RequestDoctorService = RequestDoctorService;
            _LocationTypeService = LocationTypeService;

            _ObjectionService = ObjectionService;
            _BuDoctorService = BuDoctorService;
            _DoctorService = DoctorService;
            _ServiceService = ServiceService;
            _SpecialtyService=SpecialtyService;
            _PhoneService = PhoneService;
            _UserService = UserService;
            _ParticipantService = ParticipantService;
            _RequestRpService = RequestRpService;
            _CommandeService = CommandeService;

            _BusinessUnitService = BusinessUnitService;
           _InfoService = InfoService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<DoctorResource>> CreateDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
            SaveDoctorResource SaveDoctorResource)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (Token != "")
            {
            var claims = _UserService.getPrincipal(Token);
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var Role = claims.FindFirst("Role").Value;
        
                //*** Mappage ***
            var Doctor = _mapperService.Map<SaveDoctorResource, Doctor>(SaveDoctorResource);
                if (Role == "Manager")
                {
                    Doctor.Status = Status.Approuved;
                }
                else if (Role == "Delegue")
                {
                    Doctor.Status = Status.Pending;
                }
            Doctor.Version = 0;
            Doctor.Active = 0;
            Doctor.CreatedOn = DateTime.UtcNow;
            Doctor.UpdatedOn = DateTime.UtcNow;
            Doctor.CreatedBy = Id;
            Doctor.UpdatedBy = Id;
                var NewDoctor = await _DoctorService.Create(Doctor);
            var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(NewDoctor);

            if (SaveDoctorResource.Location!=null)
            {
                foreach(var item in SaveDoctorResource.Location)
                { 
                var location = await _LocationService.GetById(item.IdLocation);
                    if(location==null)
                    {
                       
                    }
                    else
                    {
                        LocationDoctor LocationDoctor = new LocationDoctor();
                        LocationDoctor.IdLocation = location.IdLocation;
          

                        LocationDoctor.IdDoctor = NewDoctor.IdDoctor;
    

                        if (item.IdService != 0) { 
                        var NewService = await _ServiceService.GetById(item.IdService);
                        LocationDoctor.IdService = NewService.IdService;
         
                        }
                        else
                        {
                            LocationDoctor.IdService =0;
              
                        }

                        LocationDoctor.Status = Status.Approuved;
                        LocationDoctor.Version = 0;
                        LocationDoctor.Active = 0;
                        LocationDoctor.CreatedOn = DateTime.UtcNow;
                        LocationDoctor.UpdatedOn = DateTime.UtcNow;
                       LocationDoctor.CreatedBy = Id;
                       LocationDoctor.UpdatedBy = Id;
                     await _LocationDoctorService.Create(LocationDoctor);
                    }
                }
            }

            //Assign business units  The new Doctor

            if (SaveDoctorResource.BusinessUnits != null)
            {


                foreach (var item in SaveDoctorResource.BusinessUnits)
                {
                    var Bu = await _BusinessUnitService.GetById(item);

                    BuDoctor BuDoctor = new BuDoctor();
                    BuDoctor.IdBu = Bu.IdBu;
                    BuDoctor.Version = Bu.Version;
                    BuDoctor.Status = Bu.Status;
                    BuDoctor.NameBu = Bu.Name;
                    BuDoctor.IdBuNavigation= Bu;
                    BuDoctor.CreatedBy = Id;
                    BuDoctor.UpdatedBy = Id;
                    BuDoctor.IdDoctorNavigation = NewDoctor;
                    BuDoctor.IdDoctor = NewDoctor.IdDoctor;
                    BuDoctor.VersionDoctor = NewDoctor.Version;
                    BuDoctor.StatusDoctor = NewDoctor.Status;

                    BuDoctor.Status = Status.Approuved;
                    BuDoctor.Version = 0;
                    BuDoctor.Active = 0;
                    BuDoctor.CreatedOn = DateTime.UtcNow;
                    BuDoctor.UpdatedOn = DateTime.UtcNow;
                    await _BuDoctorService.Create(BuDoctor);

                }
            }
            //Add Tags to database and assign them to The new Doctor
            List<Tags> Tags = new List<Tags>();
            List<TagsDoctor> TagsDoctors = new List<TagsDoctor>();
            Tags newTag = new Tags(); 
            Tags TagExist = new Tags();

            if (SaveDoctorResource.Tags != null)
            {
                foreach(var item in SaveDoctorResource.Tags) 
                {
                    TagExist = await _TagsService.GetByExistantActif(item.Name);
                    if(TagExist == null)
                    {
                   var NewTag = _mapperService.Map<SaveTagsResource, Tags>(item);
                   NewTag.Status = Status.Approuved;
                   NewTag.Version = 0;
                   NewTag.Active = 0;
                   NewTag.CreatedOn = DateTime.UtcNow;
                   NewTag.UpdatedOn = DateTime.UtcNow;
                            NewTag.CreatedBy = Id;
                            NewTag.UpdatedBy = Id;
                            newTag = await _TagsService.Create(NewTag);

                var TagResource = _mapperService.Map<Tags, TagsResource>(newTag);
                        Tags.Add(newTag);


                    }
                    else
                    {
                        Tags.Add(TagExist);
            
                    }

                   

                }
                foreach(var item in Tags) { 
                TagsDoctor TagsDoctor = new TagsDoctor();
                TagsDoctor.Status = Status.Approuved;
                TagsDoctor.Version = 0;
                TagsDoctor.Active = 0;
                TagsDoctor.CreatedOn = DateTime.UtcNow;
                TagsDoctor.UpdatedOn = DateTime.UtcNow;
                TagsDoctor.VersionDoctor = NewDoctor.Version;
                TagsDoctor.StatusDoctor = NewDoctor.Status;
                TagsDoctor.IdDoctor = NewDoctor.IdDoctor;
                TagsDoctor.CreatedBy = Id;
                TagsDoctor.UpdatedBy = Id;
                TagsDoctor.IdDoctorNavigation = NewDoctor;

                TagsDoctor.IdTags = item.IdTags;
                TagsDoctor.StatusTags = item.Status;
                TagsDoctor.VersionTags = item.Version;
                TagsDoctor.IdTagsNavigation = item;

                await _TagsDoctorService.Create(TagsDoctor);
                }

            }


            //Add Infos to database with the Id The new Doctor

            if (SaveDoctorResource.Infos != null)
            {
                var Infos = _mapperService.Map<List<SaveInfoResource>, List<Info>>(SaveDoctorResource.Infos);
                foreach (var item in SaveDoctorResource.Infos)
                { 
                    var Info = _mapperService.Map<SaveInfoResource,Info>(item);
                    Info.IdDoctor = NewDoctor.IdDoctor;
                    Info.IdDoctorNavigation = NewDoctor;
                    Info.CreatedOn = DateTime.UtcNow;
                    Info.UpdatedOn = DateTime.UtcNow;
                    Info.Active = 0;
                    Info.Version = 0;
                    Info.Status = Status.Approuved;
                    Info.CreatedBy = Id;
                    Info.UpdatedBy = Id;
                        var InfoCreated = await _InfoService.Create(Info);
                }
      
            }

            //Add Phones to database with the Id The new Doctor
            if (SaveDoctorResource.Phones != null)
            {
             var Phones = _mapperService.Map<List<SavePhoneResource>, List<Phone>>(SaveDoctorResource.Phones);
             foreach(var item in SaveDoctorResource.Phones)
                {
                    var Phone = _mapperService.Map<SavePhoneResource, Phone>(item);

                    Phone.IdDoctor = NewDoctor.IdDoctor;
                    Phone.IdPharmacy = null;
                    Phone.CreatedOn = DateTime.UtcNow;
                    Phone.UpdatedOn = DateTime.UtcNow;
                    Phone.Active = 0;
                    Phone.Version = 0;
                    Phone.CreatedBy = Id;
                    Phone.UpdatedBy = Id;
                    Phone.Status = Status.Approuved;
                    Phones.Add(Phone);
                }
                if (Phones.Count > 0)
                {
                    await _PhoneService.CreateRange(Phones);
                }
            }
        
            return Ok(DoctorResource);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<DoctorResource>> GetDoctorsNumber([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Number)
        {
            try
            {
                var Employe = await _DoctorService.GetByExistantPhoneNumberActif(Number);
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<DoctorResource>> GetAllDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _DoctorService.GetAll();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);

                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>This method returns the list of All the doctors of the same BusinessUnit .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpGet("AllDoctorsByBu/{Id}")]
        public async Task<ActionResult<DoctorResource>> GetAllDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {
                var Employe = await _DoctorService.GetAllDoctorsByBu(Id);
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
        public async Task<ActionResult<DoctorResource>> GetAllActifDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _DoctorService.GetAllActif();
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
        public async Task<ActionResult<DoctorResource>> GetAllInactifDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _DoctorService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Assigned")]
        public async Task<ActionResult<DoctorResource>> GetAllAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _DoctorService.GetDoctorsAssigned();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("NotAssigned")]
        public async Task<ActionResult<DoctorResource>> GetAllNotAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _DoctorService.GetDoctorsAssigned();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Assigned/{Id}")]
        public async Task<ActionResult<DoctorResource>> GetAllAssignedDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {
                var Employe = await _DoctorService.GetDoctorsAssignedByBu(Id);
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("NotAssigned/{Id}")]
        public async Task<ActionResult<DoctorResource>> GetAllNotAssignedDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {
                var Employe = await _DoctorService.GetDoctorsNotAssignedByBu(Id);
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
        public async Task<ActionResult<DoctorProfile>> GetDoctorById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            DoctorProfile DoctorProfile = new DoctorProfile();
            try
            {
                var BuDoctor = (List<BuDoctor>)await _BuDoctorService.GetByIdDoctor(Id);
                List<string> Names = new List<string>();
                List<BusinessUnit> BusinessUnits = new List<BusinessUnit>();
                //Get the Business Unit Of the Doctor
               foreach (var item in BuDoctor)
                {
                   if(Names.Contains(item.NameBu))
                    { }
                    else {
                        Names.Add(item.NameBu);
                    }
                }
                foreach(var item in Names)
                {
                    var Bu = await _BusinessUnitService.GetByNames(item);
                    if (Bu != null) {
                    BusinessUnits.Add(Bu);
                    }
                }
                List<BusinessUnitResource> BusinessUnitResources = new List<BusinessUnitResource>();
                foreach (var item in BusinessUnits)
                {
                    var Bu = _mapperService.Map<BusinessUnit, BusinessUnitResource>(item);

                    if (Bu != null)
                    {
                        BusinessUnitResources.Add(Bu);
                    }
                }
                DoctorProfile.BusinessUnit = BusinessUnitResources;


                var Doctors = await _DoctorService.GetById(Id);

                var DoctorResource = _mapperService.Map<Doctor,DoctorResource> (Doctors);

                DoctorProfile.Doctor = DoctorResource;
                var TagsDoctor = await _TagsDoctorService.GetByIdActif(Id);
                List<Tags> Tags = new List<Tags>();

                foreach (var item in TagsDoctor)
                {
                    var Tag = await _TagsService.GetById(item.IdTags);
                    Tags.Add(Tag);
                }

                List<TagsResource> TagsResources = new List<TagsResource>();
                foreach (var item in Tags)
                {
                    var Bu = _mapperService.Map<Tags, TagsResource>(item);

                    if (Bu != null)
                    {
                        TagsResources.Add(Bu);
                    }
                }
                DoctorProfile.Tags = TagsResources;

                

                if (Doctors == null) return NotFound();
                var Info = await _InfoService.GetByIdDoctor(Id);
                List<InfoResource> InfoResources = new List<InfoResource>();

                foreach (var item in Info)
                {
                    var Bu = _mapperService.Map<Info, InfoResource>(item);

                    if (Bu != null)
                    {
                        InfoResources.Add(Bu);
                    }
                }
                DoctorProfile.Infos = InfoResources;
                var Phone = await _PhoneService.GetAllById(Id);
                List<PhoneResource> PhoneResources = new List<PhoneResource>();

                foreach (var item in Phone)
                {
                    var Bu = _mapperService.Map<Phone, PhoneResource>(item);

                    if (Bu != null)
                    {
                        PhoneResources.Add(Bu);
                    }
                }
                DoctorProfile.Phone = PhoneResources;
                var DoctorLocation = await _LocationDoctorService.GetAllAcceptedActif(Id);
                List<LocationDoctorResource> LocationDoctorResources = new List<LocationDoctorResource>();
                //
                foreach (var item in DoctorLocation)
                {
                     var Bu = _mapperService.Map<LocationDoctor, LocationDoctorResource>(item);
                    if (Bu != null)
                    {
                        LocationDoctorResources.Add(Bu);
                    }
                }
                DoctorProfile.LocationDoctor = LocationDoctorResources;
                List<LocationLocalityService> LocationLocalityServiceList = new List<LocationLocalityService>();

                foreach (var item in DoctorLocation)
                {
                    LocationLocalityService LocationLocalityService = new LocationLocalityService();
                    var Location = await _LocationService.GetById(item.IdLocation);
                    var Service = await _ServiceService.GetById(item.IdService);
                    LocationType LocationType = new LocationType();
                    if (Location!=null)
                    {
                        LocationType = await _LocationTypeService.GetById(Location.IdLocationType);
                    }
                    var LocationResource = _mapperService.Map<Location, LocationResource>(Location);
                    var LocationTypeResource = _mapperService.Map<LocationType, LocationTypeResource>(LocationType);
                    if (Service != null)
                    {
                        var ServiceResource = _mapperService.Map<Service, ServiceResource>(Service);
                        LocationLocalityService.ServiceResource = ServiceResource;
                    }
                    else
                    {
                        LocationLocalityService.ServiceResource = null;

                    }
                    LocationLocalityService.LocationResource = LocationResource;
                    LocationLocalityServiceList.Add(LocationLocalityService);
                }
                DoctorProfile.LocationLocalityService = LocationLocalityServiceList;

                var DoctorVisit = await _VisitReportService.GetByIdDoctor(Id);
                List<VisitReportResource> VisitReportResources = new List<VisitReportResource>();

                foreach (var item in DoctorVisit)
                {
                    var Bu = _mapperService.Map<VisitReport, VisitReportResource>(item);

                    if (Bu != null)
                    {
                        VisitReportResources.Add(Bu);
                    }
                }

                DoctorProfile.VisitReports = VisitReportResources;

                var Objections = await _ObjectionService.GetByIdActifDoctor(Id);

                List<ObjectionResource> ObjectionResources = new List<ObjectionResource>();

                foreach (var item in Objections)
                {
                    var Bu = _mapperService.Map<Objection, ObjectionResource>(item);

                    if (Bu != null)
                    {
                        ObjectionResources.Add(Bu);
                    }
                }
                DoctorProfile.Objection = ObjectionResources;

                var RequestDoctors = await _RequestDoctorService.GetByIdActifDoctor(Id);
                List<RequestDoctorResource> RequestDoctorResources = new List<RequestDoctorResource>();

                foreach (var item in RequestDoctors)
                {
                    var Bu = _mapperService.Map<RequestDoctor, RequestDoctorResource>(item);

                    if (Bu != null)
                    {
                        RequestDoctorResources.Add(Bu);
                    }
                }

                DoctorProfile.RequestDoctors = RequestDoctorResources;
                var Participant = await _ParticipantService.GetAllByIdDoctor(Id);
                List<RequestRp> RequestRpList = new List<RequestRp>();
                foreach (var item in Participant)
                {
                    var RequestRp = await _RequestRpService.GetById(item.IdRequestRp);
                    RequestRpList.Add(RequestRp);
                }
                List<RequestRpResource> RequestRpResources = new List<RequestRpResource>();

                foreach (var item in RequestRpList)
                {
                    var Bu = _mapperService.Map<RequestRp, RequestRpResource>(item);

                    if (Bu != null)
                    {
                        RequestRpResources.Add(Bu);
                    }
                }
                DoctorProfile.RequestRp = RequestRpResources;
                
             
                var Commande = await _CommandeService.GetByIdActifDoctor(Id);
                List<CommandeResource> CommandeResources = new List<CommandeResource>();

                foreach (var item in Commande)
                {
                    var Bu = _mapperService.Map<Commande, CommandeResource>(item);

                    if (Bu != null)
                    {
                        CommandeResources.Add(Bu);
                    }
                }
                DoctorProfile.Commande = CommandeResources;

                return Ok(DoctorProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<DoctorResource>> ApprouveDoctor(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var DoctorToBeModified = await _DoctorService.GetById(Id);
                if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
                                                                                                 //var newDoctor = await _DoctorService.Create(Doctors);
                                                                                                 // Doctors.CreatedOn = SaveDoctorResource.;
                DoctorToBeModified.UpdatedOn = DateTime.UtcNow;
                DoctorToBeModified.UpdatedBy = IdUser;

                await _DoctorService.Approuve(DoctorToBeModified, DoctorToBeModified);

                var DoctorUpdated = await _DoctorService.GetById(Id);

                var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);

                return Ok(DoctorResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<DoctorResource>> RejectDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var DoctorToBeModified = await _DoctorService.GetById(Id);
                if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
                                                                                                 //var newDoctor = await _DoctorService.Create(Doctors);
                                                                                                 // Doctors.CreatedOn = SaveDoctorResource.;
                DoctorToBeModified.UpdatedOn = DateTime.UtcNow;
                DoctorToBeModified.UpdatedBy = IdUser;

                await _DoctorService.Reject(DoctorToBeModified, DoctorToBeModified);

                var DoctorUpdated = await _DoctorService.GetById(Id);

                var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);

                return Ok(DoctorResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<DoctorResource>> UpdateDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token,
            int Id,SaveDoctorResource SaveDoctorResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Role = claims.FindFirst("Role").Value;
            var DoctorToBeModified = await _DoctorService.GetById(Id);
            if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
            var Doctor = _mapperService.Map<SaveDoctorResource, Doctor>(SaveDoctorResource);
            //var newDoctor = await _DoctorService.Create(Doctors);
            if (Role == "Manager")
            {
                Doctor.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Doctor.Status = Status.Pending;
            }
 
            Doctor.CreatedBy = IdUser;
            Doctor.UpdatedBy = DoctorToBeModified.UpdatedBy;
            var NewDoctor = await _DoctorService.Create(Doctor);
            var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(NewDoctor);

            if (SaveDoctorResource.Location != null)
            {
                foreach (var item in SaveDoctorResource.Location)
                {
                    var location = await _LocationService.GetById(item.IdLocation);

                    if (location == null)
                    {

                    }
                    else
                    {
                        var LocationDoctorExist = await _LocationDoctorService.GetById(NewDoctor.IdDoctor, location.IdLocation);
                        if (LocationDoctorExist != null) {
                        
                        }
                        else {
                        LocationDoctor LocationDoctor = new LocationDoctor();
                        LocationDoctor.IdLocation = location.IdLocation;
                        LocationDoctor.IdDoctor = NewDoctor.IdDoctor;
                        if (item.IdService != 0)
                        {
                            var NewService = await _ServiceService.GetById(item.IdService);
                            LocationDoctor.IdService = NewService.IdService;
                        }
                        else
                        {
                            LocationDoctor.IdService = 0;
                        }
                        LocationDoctor.Status = Status.Approuved;
                        LocationDoctor.Version = 0;
                        LocationDoctor.Active = 0;
                        LocationDoctor.CreatedOn = DateTime.UtcNow;
                        LocationDoctor.UpdatedOn = DateTime.UtcNow;
                        LocationDoctor.CreatedBy = Id;
                        LocationDoctor.UpdatedBy = Id;
                        await _LocationDoctorService.Create(LocationDoctor);
                        }
                    }
                }
            }

            //Assign business units  The new Doctor

            if (SaveDoctorResource.BusinessUnits != null)
            {
                foreach (var item in SaveDoctorResource.BusinessUnits)
                {
                    var Bu = await _BusinessUnitService.GetById(item);
                    if (Bu != null) { 
                    var BuDoctorExist = await _BuDoctorService.GetById(Bu.IdBu,NewDoctor.IdDoctor);
                        if (BuDoctorExist != null) { 
                    BuDoctor BuDoctor = new BuDoctor();
                    BuDoctor.IdBu = Bu.IdBu;
                    BuDoctor.Version = Bu.Version;
                    BuDoctor.Status = Bu.Status;
                    BuDoctor.NameBu = Bu.Name;
                    BuDoctor.IdBuNavigation = Bu;
                    BuDoctor.CreatedBy = Id;
                    BuDoctor.UpdatedBy = Id;
                    BuDoctor.IdDoctorNavigation = NewDoctor;
                    BuDoctor.IdDoctor = NewDoctor.IdDoctor;
                    BuDoctor.VersionDoctor = NewDoctor.Version;
                    BuDoctor.StatusDoctor = NewDoctor.Status;

                    BuDoctor.Status = Status.Approuved;
                    BuDoctor.Version = 0;
                    BuDoctor.Active = 0;
                    BuDoctor.CreatedOn = DateTime.UtcNow;
                    BuDoctor.UpdatedOn = DateTime.UtcNow;
                    await _BuDoctorService.Create(BuDoctor);
                    }
                    }
                }
            }
            //Add Tags to database and assign them to The new Doctor
            List<Tags> Tags = new List<Tags>();
            List<TagsDoctor> TagsDoctors = new List<TagsDoctor>();
            Tags newTag = new Tags();
            Tags TagExist = new Tags();

            if (SaveDoctorResource.Tags != null)
            {
                foreach (var item in SaveDoctorResource.Tags)
                {
                    TagExist = await _TagsService.GetByExistantActif(item.Name);
                    if (TagExist == null)
                    {
                        var NewTag = _mapperService.Map<SaveTagsResource, Tags>(item);
                        NewTag.Status = Status.Approuved;
                        NewTag.Version = 0;
                        NewTag.Active = 0;
                        NewTag.CreatedOn = DateTime.UtcNow;
                        NewTag.UpdatedOn = DateTime.UtcNow;
                        NewTag.CreatedBy = Id;
                        NewTag.UpdatedBy = Id;
                        newTag = await _TagsService.Create(NewTag);

                        var TagResource = _mapperService.Map<Tags, TagsResource>(newTag);
                        var Tag = await _TagsService.GetBy(item.Name);
                        if (Tag == null) { 
                        Tags.Add(newTag);
                        }

                    }
                    else
                    {
                        Tags.Add(TagExist);

                    }



                }
                foreach (var item in Tags)
                {
                    TagsDoctor TagsDoctor = new TagsDoctor();
                    TagsDoctor.Status = Status.Approuved;
                    TagsDoctor.Version = 0;
                    TagsDoctor.Active = 0;
                    TagsDoctor.CreatedOn = DateTime.UtcNow;
                    TagsDoctor.UpdatedOn = DateTime.UtcNow;
                    TagsDoctor.VersionDoctor = NewDoctor.Version;
                    TagsDoctor.StatusDoctor = NewDoctor.Status;
                    TagsDoctor.IdDoctor = NewDoctor.IdDoctor;
                    TagsDoctor.CreatedBy = Id;
                    TagsDoctor.UpdatedBy = Id;
                    TagsDoctor.IdDoctorNavigation = NewDoctor;

                    TagsDoctor.IdTags = item.IdTags;
                    TagsDoctor.StatusTags = item.Status;
                    TagsDoctor.VersionTags = item.Version;
                    TagsDoctor.IdTagsNavigation = item;

                    await _TagsDoctorService.Create(TagsDoctor);
                }

            }


            //Add Infos to database with the Id The new Doctor

            if (SaveDoctorResource.Infos != null)
            {
                var Infos = _mapperService.Map<List<SaveInfoResource>, List<Info>>(SaveDoctorResource.Infos);
                foreach (var item in SaveDoctorResource.Infos)
                {
                    var Oldinfo = await _InfoService.GetBy(item.Datatype, item.Data);
                    if (Oldinfo == null) { 
                    var Info = _mapperService.Map<SaveInfoResource, Info>(item);
                    Info.IdDoctor = NewDoctor.IdDoctor;
                    Info.IdDoctorNavigation = NewDoctor;
                    Info.CreatedOn = DateTime.UtcNow;
                    Info.UpdatedOn = DateTime.UtcNow;
                    Info.Active = 0;
                    Info.Version = 0;
                    Info.Status = Status.Approuved;
                    Info.CreatedBy = Id;
                    Info.UpdatedBy = Id;
                    var InfoCreated = await _InfoService.Create(Info);
                    }
                }

            }

            //Add Phones to database with the Id The new Doctor
            if (SaveDoctorResource.Phones != null)
            {
                var Phones = _mapperService.Map<List<SavePhoneResource>, List<Phone>>(SaveDoctorResource.Phones);
                foreach (var item in SaveDoctorResource.Phones)
                {
                    var Phone = _mapperService.Map<SavePhoneResource, Phone>(item);
                    
                    Phone.IdDoctor = NewDoctor.IdDoctor;
                    Phone.IdPharmacy = null;
                    Phone.CreatedOn = DateTime.UtcNow;
                    Phone.UpdatedOn = DateTime.UtcNow;
                    Phone.Active = 0;
                    Phone.Version = 0;
                    Phone.CreatedBy = Id;
                    Phone.UpdatedBy = Id;
                    Phone.Status = Status.Approuved;
                    var oldPhone = await _PhoneService.GetByIdDoctor(item.PhoneNumber,NewDoctor.IdDoctor);
                    if (oldPhone ==null) { 
                    Phones.Add(Phone);
                    }
                }
                if (Phones.Count > 0)
                {
                    //var oldPhone = await _PhoneService.GetByIdDoctor(NewDoctor.IdDoctor);
                    await _PhoneService.CreateRange(Phones);
                }
            }

            await _DoctorService.Update(DoctorToBeModified, Doctor);

            var DoctorUpdated = await _DoctorService.GetById(Id);

            var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);

            return Ok(DoctorResourceUpdated);
        }

        /* [HttpPut("Link")]
         public async Task<ActionResult<DoctorResource>> Link(Link Link)
         {

             StringValues token = "";
             ErrorHandling ErrorMessag = new ErrorHandling();
             Request.Headers.TryGetValue("token", out token);
             if (token != "")
             {

                 var claims = _UserService.getPrincipal(token);
                 var Role = claims.FindFirst("Role").Value;
                 var IdUser = int.Parse(claims.FindFirst("Id").Value);
                 var Parent = await _DoctorService.GetById(Link.Parent);

                 foreach (var item in Link.Childs)
                 {
                     var DoctorToBeModified = await _DoctorService.GetById(item);
                     if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
                                                                                                  //var newDoctor = await _DoctorService.Create(Doctors);
                                                                                                  // Doctors.CreatedOn = SaveDoctorResource.;
                     DoctorToBeModified.UpdatedOn = DateTime.UtcNow;
                     DoctorToBeModified.UpdatedBy = IdUser;

                     await _DoctorService.Update(DoctorToBeModified, DoctorToBeModified);

                     var DoctorUpdated = await _DoctorService.GetById(item);

                     var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);
                 }
                 return Ok();
             }
             else
             {
                 ErrorMessag.ErrorMessage = "Empty Token";
                 ErrorMessag.StatusCode = 400;
                 return Ok(ErrorMessag);

             }
         }*/
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {

                var sub = await _DoctorService.GetById(Id);
                if (sub == null) return BadRequest("Le Doctor  n'existe pas"); //NotFound();
                await _DoctorService.Delete(sub);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,List<int> Ids)
        {
            try
            {
                List<Doctor> empty = new List<Doctor>();
                foreach (var item in Ids)
                {
                    var sub = await _DoctorService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Doctor  n'existe pas"); //NotFound();

                }
                await _DoctorService.DeleteRange(empty);
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
