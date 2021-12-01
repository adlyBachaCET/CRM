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
        private readonly ILocalityService _LocalityService;
        private readonly IBrickService _BrickService;
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
             ILocalityService LocalityService,
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
            _LocationService = LocationService;
            _LocalityService = LocalityService;
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
        /// <summary>
        ///  This function creates a cycle.
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="VerifyDoctor">Data of the Doctor to be verified.</param>
        /// <returns>returns the created cycle.</returns>
        [HttpPost("Verify")]
        public async Task<ActionResult<DoctorResource>> Verify([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,VerifyDoctor VerifyDoctor)
        {
            DoctorExiste DoctorExiste = new DoctorExiste();
            var CheckDoctor = await _DoctorService.GetExist(VerifyDoctor.FirstName, VerifyDoctor.LastName, VerifyDoctor.Email);
            var FirstLast = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.FirstLast);
            var LastFirst = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.LastFirst);
            var DoctorEmail = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.DoctorEmail);

            DoctorExisteResource DoctorExisteResource = new DoctorExisteResource();
            DoctorExisteResource.DoctorEmail = DoctorEmail;
            DoctorExisteResource.LastFirst = LastFirst;
            DoctorExisteResource.FirstLast = FirstLast;
            if (FirstLast != null)
            {
                DoctorExisteResource.FirstLastExist = true;
            }
            else
            {
                DoctorExisteResource.FirstLastExist = false;
            }
            if (DoctorEmail != null)
            {
                DoctorExisteResource.ExistDoctorEmail = true;
            }
            else
            {
                DoctorExisteResource.ExistDoctorEmail = false;

            }
            if (LastFirst != null)
            {
                DoctorExisteResource.LastFirstExist = true;
            }
            else
            {
                DoctorExisteResource.LastFirstExist = false;

            }
            return Ok(DoctorExisteResource);

        }
        /// <summary>
        ///  This function creates a Doctor.
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="SaveDoctorResource">Data of the Doctor.</param>
        /// <returns>returns the created Doctor.</returns>
        [HttpPost]
        public async Task<ActionResult<DoctorListObject>> CreateDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
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

                if (SaveDoctorResource.Cabinet != null)
                {
                 //*** Mappage ***
                        var Location = _mapperService.Map<LocationAdd, Location>(SaveDoctorResource.Cabinet);
                        Location.UpdatedOn = DateTime.UtcNow;
                        Location.CreatedOn = DateTime.UtcNow;
                        Location.Version = 0;
                        Location.Active = 0;

                        if (Role == "Manager")
                        {
                            Location.Status = Status.Approuved;
                        }
                        else if (Role == "Delegue")
                        {
                            Location.Status = Status.Pending;
                        }
                        var Locality1 = await _LocalityService.GetById(SaveDoctorResource.Cabinet.IdLocality1);
                        Location.NameLocality1 = Locality1.Name;
                        Location.VersionLocality1 = Locality1.Version;
                        Location.StatusLocality1 = Locality1.Status;
                        Location.IdLocality1 = Locality1.IdLocality;
                        var Locality2 = await _LocalityService.GetById(SaveDoctorResource.Cabinet.IdLocality2);
                        Location.NameLocality2 = Locality2.Name;
                        Location.VersionLocality2 = Locality2.Version;
                        Location.StatusLocality2 = Locality2.Status;
                        Location.IdLocality2 = Locality1.IdLocality;
                        var NewLocationType = await _LocationTypeService.GetById(SaveDoctorResource.Cabinet.IdLocationType);
                        Location.NameLocationType = NewLocationType.Name;
                        Location.StatusLocationType = NewLocationType.Status;
                        Location.VersionLocationType = NewLocationType.Version;
                        Location.TypeLocationType = NewLocationType.Type;
                        Location.CreatedBy = Id;
                        Location.UpdatedBy = Id;
                        if (SaveDoctorResource.Cabinet.IdBrick1 != 0)
                        {
                            var Brick1 = await _BrickService.GetByIdActif(SaveDoctorResource.Cabinet.IdBrick1);
                            if (Brick1 != null)
                            {
                                Location.IdBrick1 = Brick1.IdBrick;
                                Location.VersionBrick1 = Brick1.Version;
                                Location.StatusBrick1 = Brick1.Status;
                                Location.NameBrick1 = Brick1.Name;
                                Location.NumBrick1 = Brick1.NumSystemBrick;
                                // Pharmacy.Brick1 = Brick1;
                            }
                            else
                            {
                                Location.IdBrick1 = null;
                                Location.VersionBrick1 = null;
                                Location.StatusBrick1 = null;
                                Location.NameBrick1 = "";
                                Location.NumBrick1 = 0;
                            }
                        }
                    if (SaveDoctorResource.Cabinet.IdBrick2 != 0)
                    {
                        var Brick2 = await _BrickService.GetByIdActif(SaveDoctorResource.Cabinet.IdBrick2);
                      
                            if (Brick2 != null)
                            {
                                Location.IdBrick2 = Brick2.IdBrick;
                                Location.VersionBrick2 = Brick2.Version;
                                Location.StatusBrick2 = Brick2.Status;
                                Location.NameBrick2 = Brick2.Name;
                                Location.NumBrick2 = Brick2.NumSystemBrick;
                                // Pharmacy.Brick2 = Brick2;
                            }
                            else
                            {
                                Location.IdBrick2 = null;
                                Location.VersionBrick2 = null;
                                Location.StatusBrick2 = null;
                                Location.NameBrick2 = "";
                                Location.NumBrick2 = 0;
                            }
                        }
                        var NewCabinet = await _LocationService.Create(Location);

                        //*** Mappage ***
                        var CabinetResource = _mapperService.Map<Location, LocationResource>(NewCabinet);
                    LocationDoctor LocationDoctor = new LocationDoctor();
                    LocationDoctor.IdLocation = CabinetResource.IdLocation;


                    LocationDoctor.IdDoctor = NewDoctor.IdDoctor;


              
                    LocationDoctor.IdService = 0;

                  

                    LocationDoctor.Status = Status.Approuved;
                    LocationDoctor.Version = 0;
                    LocationDoctor.Active = 0;
                    LocationDoctor.CreatedOn = DateTime.UtcNow;
                    LocationDoctor.UpdatedOn = DateTime.UtcNow;
                    LocationDoctor.CreatedBy = Id;
                    LocationDoctor.UpdatedBy = Id;
                    await _LocationDoctorService.Create(LocationDoctor);
                }
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
                            LocationDoctor LocationDoctor = new LocationDoctor();
                            var OldLocationDoctor = await _LocationDoctorService.GetByIdActif(NewDoctor.IdDoctor, location.IdLocation);
                            if (OldLocationDoctor != null)
                            {
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
                if (SaveDoctorResource.IdSpecialty1 != 0)
                {
                    var Specialty1 = await _SpecialtyService.GetById(SaveDoctorResource.IdSpecialty1);
                    NewDoctor.IdSpecialty1 = Specialty1.IdSpecialty;
                    NewDoctor.NameSpecialty1 = Specialty1.Name;

                }
                if (SaveDoctorResource.IdSpecialty2 != 0)
                {
                    var Specialty2 = await _SpecialtyService.GetById(SaveDoctorResource.IdSpecialty2);
                    NewDoctor.IdSpecialty2 = Specialty2.IdSpecialty;
                    NewDoctor.NameSpecialty2 = Specialty2.Name;

                }
                if (SaveDoctorResource.IdPotentiel != 0)
                {
                    var Potentiel = await _PotentielService.GetById(SaveDoctorResource.IdPotentiel);
                    NewDoctor.IdPotentiel = Potentiel.IdPotentiel;
                    NewDoctor.NamePotentiel = Potentiel.Name;

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
                                Tags.Add(newTag);


                            }
                            else
                            {
                                Tags.Add(TagExist);

                            }



                        }
                        foreach (var item in Tags) {
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
                            Phones.Add(Phone);
                        }
                        if (Phones.Count > 0)
                        {
                            await _PhoneService.CreateRange(Phones);
                        }
                    }
                var DoctorResourceInfo = await GetById(DoctorResource.IdDoctor);

                return Ok(DoctorResourceInfo); 
            
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }

        }
        /// <summary>
        ///  This function gets the list of the doctor by number
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Number">Number to be searched.</param>
        /// <returns>the doctor if found.</returns>
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<DoctorResource>> GetDoctorsNumber([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Number)
        {
            try
            {
          
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetByExistantPhoneNumberActif(Number);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the doctor Without Appointment
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet("GetDoctorsWithoutAppointment")]
        public async Task<ActionResult<DoctorResource>> GetDoctorsWithoutAppointment([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var Role = claims.FindFirst("Role").Value;
      
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetMyDoctorsWithoutAppointment(Id);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the doctors 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet]
        public async Task<ActionResult<DoctorResource>> GetAllDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetAll();
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
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
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetAllDoctorsByBu(Id);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the actif doctors 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet("Actif")]
        public async Task<ActionResult<DoctorListObject>> GetAllActifDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetAllActif();
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the inactif doctors 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet("InActif")]
        public async Task<ActionResult<DoctorListObject>> GetAllInactifDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetAllInActif();
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the assingned doctors 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet("Assigned")]
        public async Task<ActionResult<DoctorListObject>> GetAllAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetDoctorsAssigned();
                if (Employe == null) return NotFound();
                foreach(var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the unassingned doctors 
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <returns>the doctors if found.</returns>
        [HttpGet("NotAssigned")]
        public async Task<ActionResult<DoctorListObject>> GetAllNotAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetDoctorsNotAssigned();
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the assingned doctors by businessUnit
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the businessUnit.</param>

        /// <returns>the doctors if found.</returns>
        [HttpGet("Assigned/{Id}")]
        public async Task<ActionResult<DoctorListObject>> GetAllAssignedDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetDoctorsAssignedByBu(Id);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the assingned doctors by businessUnit
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the businessUnit.</param>

        /// <returns>the doctors if found.</returns>
        [HttpGet("NotAssignedByBu")]
        public async Task<ActionResult<DoctorResource>> GetAllNotAssignedDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();

                var Employe = await _DoctorService.GetDoctorsNotAssignedByBu(IdUser);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(DoctorListObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the doctor.</param>
        /// <returns>the doctor if found.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<DoctorListObject> GetById( int Id)
        {
            DoctorListObject DoctorListObject = new DoctorListObject();
        
                var BuDoctor = (List<BuDoctor>)await _BuDoctorService.GetByIdDoctor(Id);
                List<string> Names = new List<string>();
                List<BusinessUnit> BusinessUnits = new List<BusinessUnit>();

                //Get the Business Unit Of the Doctor
                foreach (var item in BuDoctor)
                {
                    if (Names.Contains(item.NameBu))
                    { }
                    else
                    {
                        Names.Add(item.NameBu);
                    }
                }
                foreach (var item in Names)
                {
                    var Bu = await _BusinessUnitService.GetByNames(item);
                    if (Bu != null)
                    {
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
                DoctorListObject.BusinessUnit = BusinessUnitResources;


                var Doctors = await _DoctorService.GetById(Id);
            if (Doctors != null)
            {

                if (Doctors.Active == 0)
                {
                    var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctors);
                    var Potentiel = await _PotentielService.GetById(Doctors.IdPotentiel);
                    PotentielDoctor PotentielDoctor = new PotentielDoctor();
                    if (Potentiel != null)
                    {
                        PotentielDoctor.IdPotentiel = Potentiel.IdPotentiel;
                        PotentielDoctor.NamePotentiel = Potentiel.Name;
                        DoctorResource.Potentiel = PotentielDoctor;
                    }
                    List<Specialities> SpecialitiesList = new List<Specialities>();

                    var Specialty1 = await _SpecialtyService.GetById(Doctors.IdSpecialty1);
                    Specialities Specialities1 = new Specialities();
                    if (Specialty1 != null)
                    {
                        Specialities1.IdSpecialty = Specialty1.IdSpecialty;
                        Specialities1.NameSpecialty = Specialty1.Name;
                        Specialities1.Abr = Specialty1.Abreviation;

                    }
                    Specialities Specialities2 = new Specialities();

                    var Specialty2 = await _SpecialtyService.GetById(Doctors.IdSpecialty2);
                    if (Specialty2 != null) { 
                    Specialities2.IdSpecialty = Specialty2.IdSpecialty;
                    Specialities2.NameSpecialty = Specialty2.Name;
                    Specialities2.Abr = Specialty2.Abreviation;
                        SpecialitiesList.Add(Specialities2);

                    }
                    SpecialitiesList.Add(Specialities1);
                    DoctorResource.Specialities = SpecialitiesList;
                    DoctorListObject.Doctor = DoctorResource;
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
                    DoctorListObject.Tags = TagsResources;




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
                    DoctorListObject.Phone = PhoneResources;
                    var DoctorLocation = await _LocationDoctorService.GetAllAcceptedActif(Id);
                    List<LocationDoctorResource> LocationDoctorResources = new List<LocationDoctorResource>();
                    //List<LocationResource> LocationResource = new List<LocationResource>();
                    
                    foreach (var item in DoctorLocation)
                    {
                        var Bu = _mapperService.Map<LocationDoctor, LocationDoctorResource>(item);
                        if (Bu != null)
                        {
                            var Location = await _LocationService.GetById(item.IdLocation);
                            var LocationResource = _mapperService.Map<Location, LocationResource>(Location);
                            Bu.Location = LocationResource;
                            var Service = await _ServiceService.GetById(item.IdService);
                            var ServiceResource = _mapperService.Map<Service, ServiceResource>(Service);
                            Bu.Service = ServiceResource;
                            LocationDoctorResources.Add(Bu);
                        }
                    }

                    DoctorListObject.LocationDoctor = LocationDoctorResources;
        



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


                }
            }
            return DoctorListObject;
        
        }

        /// <summary>
        ///  This function gets the doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Id">Id of the doctor.</param>
        /// <returns>the doctor if found.</returns>
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

                var Potentiel = await _PotentielService.GetById(Doctors.IdPotentiel);
    
                PotentielDoctor PotentielDoctor = new PotentielDoctor();
                PotentielDoctor.IdPotentiel = Potentiel.IdPotentiel;
                PotentielDoctor.NamePotentiel = Potentiel.Name;
                DoctorResource.Potentiel = PotentielDoctor;
                var Specialty1 = await _SpecialtyService.GetById(Doctors.IdSpecialty1);
                Specialities Specialities1 = new Specialities();
                Specialities1.IdSpecialty = Specialty1.IdSpecialty;
                Specialities1.NameSpecialty = Specialty1.Name;
                Specialities1.Abr = Specialty1.Abreviation;
                Specialities Specialities2 = new Specialities();

                var Specialty2 = await _SpecialtyService.GetById(Doctors.IdSpecialty2);
                Specialities2.IdSpecialty = Specialty2.IdSpecialty;
                Specialities2.NameSpecialty = Specialty2.Name;
                Specialities2.Abr = Specialty2.Abreviation;
                List<Specialities> SpecialitiesList = new List<Specialities>();
                SpecialitiesList.Add(Specialities1);
                SpecialitiesList.Add(Specialities2);
                DoctorResource.Specialities = SpecialitiesList;
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
                        var Location = await _LocationService.GetById(item.IdLocation);
                        var LocationResource = _mapperService.Map<Location, LocationResource>(Location);
                        Bu.Location = LocationResource;
                        var Service = await _ServiceService.GetById(item.IdService);
                        var ServiceResource = _mapperService.Map<Service, ServiceResource>(Service);
                        Bu.Service = ServiceResource;

                        LocationDoctorResources.Add(Bu);
                    }
                }
                DoctorProfile.LocationDoctor = LocationDoctorResources;
        
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
        /// <summary>
        ///  This function is used to Approuve a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the Doctor.</param>

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
        /// <summary>
        ///  This function is used to Reject a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the Doctor.</param>
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
        /// <summary>
        ///  This function is used to Update a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the doctor.</param>

        [HttpPut("{Id}")]
        public async Task<ActionResult<DoctorListObject>> UpdateDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token,
            int Id, SaveDoctorResourceUpdate SaveDoctorResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var Role = claims.FindFirst("Role").Value;
            var DoctorToBeModified = await _DoctorService.GetById(Id);
            if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
            var Doctor = _mapperService.Map<SaveDoctorResourceUpdate, Doctor>(SaveDoctorResource);
            //var newDoctor = await _DoctorService.Create(Doctors);
            if (Role == "Manager")
            {
                Doctor.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Doctor.Status = Status.Pending;
            }

            Doctor.CreatedBy = DoctorToBeModified.CreatedBy ;
            Doctor.UpdatedBy = IdUser;
            Doctor.CreatedOn = DoctorToBeModified.CreatedOn;
            Doctor.UpdatedOn = DateTime.UtcNow;
            Doctor.Version = DoctorToBeModified.Version+1;
            Doctor.Active = 0;
            Doctor.IdDoctor = Id;
            await _DoctorService.Update(DoctorToBeModified, Doctor);
            var DoctorUpdated = await _DoctorService.GetById(Id);
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
                        var LocationDoctorExist = await _LocationDoctorService.GetByIdActif(DoctorUpdated.IdDoctor, location.IdLocation);
                        if (LocationDoctorExist != null) 
                        { 
                            await _LocationDoctorService.Delete(LocationDoctorExist);
                        }

                        LocationDoctor LocationDoctor = new LocationDoctor();
                        LocationDoctor.IdLocation = location.IdLocation;
                        LocationDoctor.IdDoctor = DoctorUpdated.IdDoctor;
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
                        LocationDoctor.CreatedBy = IdUser;
                        LocationDoctor.UpdatedBy = IdUser;
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
                    if (Bu != null) { 
                    var BuDoctorExist = await _BuDoctorService.GetById(Bu.IdBu,DoctorUpdated.IdDoctor);
                        if (BuDoctorExist != null) { 
                    BuDoctor BuDoctor = new BuDoctor();
                    BuDoctor.IdBu = Bu.IdBu;
                    BuDoctor.Version = Bu.Version;
                    BuDoctor.Status = Bu.Status;
                    BuDoctor.NameBu = Bu.Name;
                    BuDoctor.IdBuNavigation = Bu;
                    BuDoctor.CreatedBy = IdUser;
                    BuDoctor.UpdatedBy = IdUser;
                    BuDoctor.IdDoctorNavigation = DoctorUpdated;
                    BuDoctor.IdDoctor = DoctorUpdated.IdDoctor;
                    BuDoctor.VersionDoctor = DoctorUpdated.Version;
                    BuDoctor.StatusDoctor = DoctorUpdated.Status;

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
                        NewTag.CreatedBy = IdUser;
                        NewTag.UpdatedBy = IdUser;
                        var Tag = await _TagsService.GetBy(item.Name);
                        if (Tag == null)
                        {
                        newTag = await _TagsService.Create(NewTag);

                        var TagResource = _mapperService.Map<Tags, TagsResource>(newTag);
                    
                        Tags.Add(newTag);
                        }

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
                    TagsDoctor.VersionDoctor = DoctorUpdated.Version;
                    TagsDoctor.StatusDoctor = DoctorUpdated.Status;
                    TagsDoctor.IdDoctor = DoctorUpdated.IdDoctor;
                    TagsDoctor.CreatedBy = IdUser;
                    TagsDoctor.UpdatedBy = IdUser;
                    TagsDoctor.IdDoctorNavigation = DoctorUpdated;

                    TagsDoctor.IdTags = item.IdTags;
                    TagsDoctor.StatusTags = item.Status;
                    TagsDoctor.VersionTags = item.Version;
                    TagsDoctor.IdTagsNavigation = item;
                    var TagsDoctorExist = await _TagsDoctorService.GetByIdActif(TagsDoctor.IdDoctor, TagsDoctor.IdTags);
                    if (TagsDoctorExist == null) { 
                        await _TagsDoctorService.Create(TagsDoctor);
                }
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
                    Info.IdDoctor = DoctorUpdated.IdDoctor;
                    Info.IdDoctorNavigation = DoctorUpdated;
                    Info.CreatedOn = DateTime.UtcNow;
                    Info.UpdatedOn = DateTime.UtcNow;
                    Info.Active = 0;
                    Info.Version = 0;
                    Info.Status = Status.Approuved;
                    Info.CreatedBy = IdUser;
                    Info.UpdatedBy = IdUser;
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
                    
                    Phone.IdDoctor = DoctorUpdated.IdDoctor;
                    Phone.IdPharmacy = null;
                    Phone.CreatedOn = DateTime.UtcNow;
                    Phone.UpdatedOn = DateTime.UtcNow;
                    Phone.Active = 0;
                    Phone.Version = 0;
                    Phone.CreatedBy = IdUser;
                    Phone.UpdatedBy = IdUser;
                    Phone.Status = Status.Approuved;
                    var oldPhone = await _PhoneService.GetByIdDoctor(item.PhoneNumber,DoctorUpdated.IdDoctor);
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

       


            var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);
            var DoctorResourceInfo = await GetById(DoctorResourceUpdated.IdDoctor);

            return Ok(DoctorResourceInfo);
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
        /// <summary>
        ///  This function is used to delete a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the doctor.</param>

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
        /// <summary>
        ///  This function is used to delete a  list Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Ids">List of the Ids of the doctor.</param>

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
