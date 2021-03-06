using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class DoctorController : ControllerBase
    {
        private readonly ISpecialtyDoctorService _SpecialtyDoctorService;

        private readonly IDoctorService _DoctorService;
        private readonly IVisitReportService _VisitReportService;
        private readonly ILocalityService _LocalityService;
        private readonly IBrickService _BrickService;
        private readonly IBusinessUnitService _BusinessUnitService;
        private readonly IRequestRpService _RequestRpService;

        private readonly IBuDoctorService _BuDoctorService;
        private readonly IServiceService _ServiceService;

        private readonly ILocationDoctorService _LocationDoctorService;
        private readonly ILocationService _LocationService;
        private readonly IPhoneService _PhoneService;
        private readonly IUserService _UserService;
        private readonly ILocationTypeService _LocationTypeService;

        private readonly ITagsDoctorService _TagsDoctorService;
        private readonly ITagsService _TagsService;
        private readonly IObjectionService _ObjectionService;

        private readonly IInfoService _InfoService;
        private readonly IPotentielService _PotentielService;
        private readonly ISpecialtyService _SpecialtyService;
        private readonly IMapper _mapperService;
        public DoctorController(ILocationDoctorService LocationDoctorService,
            IPhoneService PhoneService,
            IUserService UserService,
                        IBrickService BrickService,

            ISpecialtyDoctorService SpecialtyDoctorService,
            ILocationService LocationService,
                        ILocationTypeService LocationTypeService,
             ILocalityService LocalityService,
            IServiceService ServiceService,
            IDoctorService DoctorService,
            IPotentielService PotentielService, IObjectionService ObjectionService,
            ISpecialtyService SpecialtyService,
            IBusinessUnitService BusinessUnitService,
            IRequestRpService RequestRpService,
            IInfoService InfoService,
            ITagsService TagsService,
            ITagsDoctorService TagsDoctorService,
            IVisitReportService VisitReportService,
            IBuDoctorService BuDoctorService, IMapper mapper)
        {
            _BrickService = BrickService;

            _SpecialtyDoctorService = SpecialtyDoctorService;
            _LocationService = LocationService;
            _LocalityService = LocalityService;
            _VisitReportService = VisitReportService;
            _TagsDoctorService = TagsDoctorService;
            _LocationDoctorService = LocationDoctorService;
            _LocationService = LocationService;
            _TagsService = TagsService;
            _PotentielService = PotentielService;
            _LocationTypeService = LocationTypeService;
            _ObjectionService = ObjectionService;
            _BuDoctorService = BuDoctorService;
            _DoctorService = DoctorService;
            _ServiceService = ServiceService;
            _SpecialtyService = SpecialtyService;
            _PhoneService = PhoneService;
            _UserService = UserService;
            _RequestRpService = RequestRpService;
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
        public async Task<ActionResult<DoctorResource>> Verify([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
            , VerifyDoctor VerifyDoctor)
        {
            try
            {
                var CheckDoctor = await _DoctorService.GetExist(VerifyDoctor.FirstName, VerifyDoctor.LastName, VerifyDoctor.Email);
                var FirstLast = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.FirstLast);
                var LastFirst = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.LastFirst);
                var DoctorEmail = _mapperService.Map<Doctor, DoctorResource>(CheckDoctor.DoctorEmail);

                var DoctorExisteResource = new DoctorExisteResource();
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                ErrorHandling ErrorMessag = new ErrorHandling();
                if (Token != "")
                {

                    var claims = _UserService.getPrincipal(Token);
                    if (claims == null)
                    {
                        throw new InvalidOperationException("Invalid Token = " + Token);
                    }
                    var Id = int.Parse(claims.FindFirst("Id").Value);
                    var Role = claims.FindFirst("Role").Value;


                    //*** Mappage ***
                    var Doctor = _mapperService.Map<SaveDoctorResource, Doctor>(SaveDoctorResource);
                    if (Role == "Manager")
                    {
                        Doctor.Status = Status.Approuved;
                        Doctor.ManagerApprouved = Id;

                    }
                    else if (Role == "Delegue")
                    {
                        Doctor.Status = Status.Pending;
                        Doctor.ManagerApprouved = Id;

                    }
                    Doctor.VersionLink = 0;
                    Doctor.StatusLink = 0;
                    Doctor.LinkedId = null;
                    Doctor.Version = 0;
                    Doctor.Active = 0;
                    Doctor.CreatedOn = DateTime.UtcNow;
                    Doctor.UpdatedOn = DateTime.UtcNow;
                    Doctor.CreatedBy = Id;
                    Doctor.UpdatedBy = Id;

                    var Potentiel = await _PotentielService.GetById(SaveDoctorResource.IdPotentiel);
                    if (Potentiel == null)
                    {
                        throw new InvalidOperationException("No Potentiel found with Id " + SaveDoctorResource.IdPotentiel);
                    }
                    if (SaveDoctorResource.IdPotentiel != 0)
                    {
                        Doctor.IdPotentiel = Potentiel.IdPotentiel;
                        Doctor.NamePotentiel = Potentiel.Name;
                        Doctor.StatusPotentiel = Potentiel.Status;
                        Doctor.VersionPotentiel = Potentiel.Version;


                    }



                    var NewDoctor = await _DoctorService.Create(Doctor);
                    if (NewDoctor == null)
                    {
                        throw new InvalidOperationException("Doctor Not Created " + NewDoctor);
                    }
                    var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(NewDoctor);

                    foreach (var item in SaveDoctorResource.SpecialtyList)
                    {
                        if (item != 0)
                        {
                            var Specialty = await _SpecialtyService.GetById(item);
                            if (Specialty == null)
                            {
                                throw new InvalidOperationException("No Specilty not found with Id " + item);
                            }
                            SpecialtyDoctor SpecialtyDoctor = new SpecialtyDoctor();

                            SpecialtyDoctor.IdSpecialty = Specialty.IdSpecialty;
                            SpecialtyDoctor.VersionSpecialty = Specialty.Version;
                            SpecialtyDoctor.StatusSpecialty = Specialty.Status;
                            SpecialtyDoctor.Specialty = Specialty;
                            SpecialtyDoctor.CreatedBy = Id;
                            SpecialtyDoctor.UpdatedBy = Id;
                            SpecialtyDoctor.IdDoctorNavigation = NewDoctor;
                            SpecialtyDoctor.IdDoctor = NewDoctor.IdDoctor;
                            SpecialtyDoctor.VersionDoctor = NewDoctor.Version;
                            SpecialtyDoctor.StatusDoctor = NewDoctor.Status;

                            SpecialtyDoctor.Status = Status.Approuved;
                            SpecialtyDoctor.Version = 0;
                            SpecialtyDoctor.Active = 0;
                            SpecialtyDoctor.CreatedOn = DateTime.UtcNow;
                            SpecialtyDoctor.UpdatedOn = DateTime.UtcNow;
                            var SpeciltyDoctor = await _SpecialtyDoctorService.Create(SpecialtyDoctor);
                            if (NewDoctor == null)
                            {
                                throw new InvalidOperationException("Doctor Not Created " + NewDoctor);
                            }
                        }

                    }
                    if (SaveDoctorResource.Cabinets != null)
                    {
                        foreach (var item in SaveDoctorResource.Cabinets)
                        {
                            if (item.Cabinet != null)
                            {
                                //*** Mappage ***
                                var Location = _mapperService.Map<LocationAdd, Location>(item.Cabinet);
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
                                var Locality1 = await _LocalityService.GetById(item.Cabinet.IdLocality1);
                                if (Locality1 == null)
                                {
                                    throw new InvalidOperationException("No Locality1 found with Id " + item.Cabinet.IdLocality1);
                                }
                                Location.NameLocality1 = Locality1.Name;
                                Location.VersionLocality1 = Locality1.Version;
                                Location.StatusLocality1 = Locality1.Status;
                                Location.IdLocality1 = Locality1.IdLocality;
                                var Locality2 = await _LocalityService.GetById(item.Cabinet.IdLocality2);
                                if (Locality2 == null)
                                {
                                    throw new InvalidOperationException("No Locality2 found with Id " + item.Cabinet.IdLocality2);
                                }
                                Location.NameLocality2 = Locality2.Name;
                                Location.VersionLocality2 = Locality2.Version;
                                Location.StatusLocality2 = Locality2.Status;
                                Location.IdLocality2 = Locality1.IdLocality;
                                var NewLocationType = await _LocationTypeService.GetById(item.Cabinet.IdLocationType);
                                if (NewLocationType == null)
                                {
                                    throw new InvalidOperationException("No LocationType found with Id " + item.Cabinet.IdLocationType);
                                }
                                Location.NameLocationType = NewLocationType.Name;
                                Location.StatusLocationType = NewLocationType.Status;
                                Location.VersionLocationType = NewLocationType.Version;
                                Location.TypeLocationType = NewLocationType.Type;
                                Location.CreatedBy = Id;
                                Location.UpdatedBy = Id;
                                if (item.Cabinet.IdBrick1 != 0)
                                {
                                    var Brick1 = await _BrickService.GetByIdActif(item.Cabinet.IdBrick1);
                                    if (Brick1 == null)
                                    {
                                        throw new InvalidOperationException("No Brick 1 found with Id " + item.Cabinet.IdBrick1);
                                    }
                                    if (Brick1 != null)
                                    {
                                        Location.IdBrick1 = Brick1.IdBrick;
                                        Location.VersionBrick1 = Brick1.Version;
                                        Location.StatusBrick1 = Brick1.Status;
                                        Location.NameBrick1 = Brick1.Name;
                                        Location.NumBrick1 = Brick1.NumSystemBrick;
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
                                if (item.Cabinet.IdBrick2 != 0)
                                {
                                    var Brick2 = await _BrickService.GetByIdActif(item.Cabinet.IdBrick2);
                                    if (Brick2 == null)
                                    {
                                        throw new InvalidOperationException("No Brick 2 found with Id " + item.Cabinet.IdBrick2);
                                    }
                                    if (Brick2 != null)
                                    {
                                        Location.IdBrick2 = Brick2.IdBrick;
                                        Location.VersionBrick2 = Brick2.Version;
                                        Location.StatusBrick2 = Brick2.Status;
                                        Location.NameBrick2 = Brick2.Name;
                                        Location.NumBrick2 = Brick2.NumSystemBrick;
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
                                if (NewCabinet == null)
                                {
                                    throw new InvalidOperationException("Cabinet Not Created " + NewCabinet);
                                }
                                //*** Mappage ***
                                var CabinetResource = _mapperService.Map<Location, LocationResource>(NewCabinet);
                                LocationDoctor LocationDoctor = new LocationDoctor();
                                LocationDoctor.IdLocation = CabinetResource.IdLocation;


                                LocationDoctor.IdDoctor = NewDoctor.IdDoctor;



                                LocationDoctor.IdService = null;
                                LocationDoctor.Order = item.Order;

                                LocationDoctor.Primary = item.Primary;


                                LocationDoctor.Status = Status.Approuved;
                                LocationDoctor.Version = 0;
                                LocationDoctor.Active = 0;
                                LocationDoctor.CreatedOn = DateTime.UtcNow;
                                LocationDoctor.UpdatedOn = DateTime.UtcNow;
                                LocationDoctor.CreatedBy = Id;
                                LocationDoctor.UpdatedBy = Id;
                              var NewLocationDoctor =  await _LocationDoctorService.Create(LocationDoctor);
                                if (NewLocationDoctor == null)
                                {
                                    throw new InvalidOperationException("Location Doctor Not Created " + NewLocationDoctor);
                                }
                            }
                        }
                    }
                    if (SaveDoctorResource.Location != null)
                    {

                        foreach (var item in SaveDoctorResource.Location)
                        {
                            if (item != null)
                            {
                                var location = await _LocationService.GetById(item.IdLocation);
                                if (location == null)
                                {
                                    throw new InvalidOperationException("No Brick found with Id " + item.IdLocation);
                                }
                                if (location != null)
                                {
                                    LocationDoctor LocationDoctor = new LocationDoctor();

                                    LocationDoctor.IdLocation = location.IdLocation;


                                    LocationDoctor.IdDoctor = NewDoctor.IdDoctor;


                                    if (item.IdService != 0)
                                    {
                                        var NewService = await _ServiceService.GetById(item.IdService);
                                        if (NewService == null)
                                        {
                                            throw new InvalidOperationException("No Service found with Id " + item.IdService);
                                        }
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
                                   var NewLocationDoctor= await _LocationDoctorService.Create(LocationDoctor);
                                    if (NewLocationDoctor == null)
                                    {
                                        throw new InvalidOperationException("Location Doctor Not Created " + NewLocationDoctor);
                                    }
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
                            if (Bu == null)
                            {
                                throw new InvalidOperationException("No Business Unit found with Id " + item);
                            }
                            BuDoctor BuDoctor = new BuDoctor();
                            BuDoctor.IdBu = Bu.IdBu;
                            BuDoctor.Version = Bu.Version;
                            BuDoctor.Status = Bu.Status;
                            BuDoctor.NameBu = Bu.Name;
                            BuDoctor.Bu = Bu;
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
                           var NewBuDoctor= await _BuDoctorService.Create(BuDoctor);
                            if (NewBuDoctor == null)
                            {
                                throw new InvalidOperationException("Bu Doctor Not Created " + NewBuDoctor);
                            }
                        }
                    }


                    //Add Tags to database and assign them to The new Doctor
                    List<Tags> Tags = new List<Tags>();


                    if (SaveDoctorResource.Tags != null)
                    {
                        foreach (var item in SaveDoctorResource.Tags)
                        {
                            var TagExist = await _TagsService.GetByExistantActif(item.Name);
                        
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
                                var newTag = await _TagsService.Create(NewTag);

                                Tags.Add(newTag);


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
                            TagsDoctor.Tags = item;

                            var TagDoctor=await _TagsDoctorService.Create(TagsDoctor);
                            if (TagDoctor == null)
                            {
                                throw new InvalidOperationException("Tag Doctor Not Created " + TagDoctor);
                            }
                        }

                    }


                    //Add Infos to database with the Id The new Doctor

                    if (SaveDoctorResource.Infos != null)
                    {
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
                            var InfoCreated=await _InfoService.Create(Info);
                            if (InfoCreated == null)
                            {
                                throw new InvalidOperationException("Info Not Created " + InfoCreated);
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
                            Phones.Add(Phone);
                        }
                        if (Phones.Count > 0)
                        {
                           var PhonesCreated= await _PhoneService.CreateRange(Phones);
                            if (PhonesCreated == null)
                            {
                                throw new InvalidOperationException("Phones Not Created " + PhonesCreated);
                            }
                        }
                    }
                    var DoctorResourceInfo = await GetById(DoctorResource.IdDoctor);
                    if (DoctorResourceInfo == null)
                    {
                        throw new InvalidOperationException("No Doctor found with Id " + DoctorResource.IdDoctor);
                    }
                    return Ok(DoctorResourceInfo);

                }
                else
                {
                    ErrorMessag.ErrorMessage = "Empty Token";
                    ErrorMessag.StatusCode = 400;
                    return Ok(ErrorMessag);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function gets the list of the doctor by number
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        ///<param name="Number">Number to be searched.</param>
        /// <returns>the doctor if found.</returns>
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<DoctorResource>> GetDoctorsNumber([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Number)
        {
            try
            {

                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Docotrs = await _DoctorService.GetByExistantPhoneNumberActif(Number);
                if (Docotrs == null)
                {
                    throw new InvalidOperationException("No Phone found with number " + Number);
                }
                foreach (var item in Docotrs)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }
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
                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token " + Token);
                }
                var Id = int.Parse(claims.FindFirst("Id").Value);

                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Employe = await _DoctorService.GetMyDoctorsWithoutAppointment(Id);
                if (Employe == null) return NotFound();
                foreach (var item in Employe)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }
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
                var Docotrs = await _DoctorService.GetAll();
                if (Docotrs == null)
                {
                    throw new InvalidOperationException("No doctors found");
                }
                foreach (var item in Docotrs)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }
                return Ok(DoctorListObject);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>This method returns the list of All the doctors of the same BusinessUnit .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpGet("AllDoctorsByBu/{Id}")]
        public async Task<ActionResult<DoctorResource>> GetAllDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Doctors = await _DoctorService.GetAllDoctorsByBu(Id);
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                if (Doctors == null) return NotFound();
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }
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
                var Doctors = await _DoctorService.GetAllActif();
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                if (Doctors == null) return NotFound();
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }


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
                var Doctors = await _DoctorService.GetAllInActif();
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                if (Doctors == null) return NotFound();
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }


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
        [HttpGet("AllAssigned")]
        public async Task<ActionResult<DoctorListObject>> GetAllAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Doctors = await _DoctorService.GetDoctorsAssigned();
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }

                    DoctorListObject.Add(Doctor);
                }


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
        [HttpGet("AllNotAssigned")]
        public async Task<ActionResult<DoctorListObject>> GetAllNotAssignedDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Doctors = await _DoctorService.GetDoctorsNotAssigned();
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No doctor found with Id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }


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
        [HttpGet("AssignedByBu")]
        public async Task<ActionResult<DoctorListObject>> GetAllAssignedDoctorsByBu([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token");
                }
                var IdBu = int.Parse(claims.FindFirst("IdBu").Value);
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();
                var Doctors = await _DoctorService.GetDoctorsAssignedByBu(IdBu);

                if (Doctors == null)
                {
                    throw new InvalidOperationException("No doctors found with");
                }
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    DoctorListObject.Add(Doctor);
                }


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
                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token");
                }
                var IdBu = int.Parse(claims.FindFirst("IdBu").Value);
                List<DoctorListObject> DoctorListObject = new List<DoctorListObject>();

                var Doctors = await _DoctorService.GetDoctorsNotAssignedByBu(IdBu);
                if (Doctors == null)
                {
                    throw new InvalidOperationException("No Doctors Found in the business Unit "+ IdBu);
                }
                foreach (var item in Doctors)
                {
                    var Doctor = await GetById(item.IdDoctor);
                    if (Doctor == null)
                    {
                        throw new InvalidOperationException("No Doctor Found with the id " + item.IdDoctor);
                    }
                    DoctorListObject.Add(Doctor);
                }


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
        public async Task<DoctorListObject> GetById(int Id)
        {
            DoctorListObject DoctorListObject = new DoctorListObject();



            var Doctor = await _DoctorService.GetById(Id);
            if (Doctor == null)
            {
                throw new InvalidOperationException("No Doctor Found with the id " + Id);
            }
            if (Doctor != null)
            {
                List<BusinessUnitResource> BusinessUnitResources = new List<BusinessUnitResource>();
                foreach (var item in Doctor.BuDoctor)
                {
                    var Bu = _mapperService.Map<BusinessUnit, BusinessUnitResource>(item.Bu);

                    if (Bu != null)
                    {
                        BusinessUnitResources.Add(Bu);
                    }
                }
                DoctorListObject.BusinessUnit = BusinessUnitResources;


                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);
                var Potentiel = await _PotentielService.GetById(Doctor.IdPotentiel);
                var PotentielResource = _mapperService.Map<Potentiel, PotentielResource>(Doctor.Potentiel);

                if (Doctor.Potentiel != null)
                {
                    PotentielResource.IdPotentiel = Doctor.Potentiel.IdPotentiel;
                    PotentielResource.Name = Doctor.Potentiel.Name;
                    PotentielResource.Status = Doctor.Potentiel.Status;
                    PotentielResource.Version = Doctor.Potentiel.Version;
                    // DoctorResource.Potentiel = Doctor.Potentiel;
                }
                List<Specialities> SpecialitiesList = new List<Specialities>();
                var Specialty = await _DoctorService.GetByIdDoctor(DoctorResource.IdDoctor);
                foreach (var item in Specialty)
                {
                    Specialities Specialities = new Specialities();
                    Specialities.Abr = item.Abreviation;
                    Specialities.IdSpecialty = item.IdSpecialty;
                    Specialities.NameSpecialty = item.Name;
                    SpecialitiesList.Add(Specialities);
                }
                DoctorResource.Specialities = SpecialitiesList;
                DoctorListObject.Doctor = DoctorResource;
                var TagsDoctor = Doctor.TagsDoctor.Select(i => i.Tags);
                List<Tags> Tags = new List<Tags>();

                List<TagsResource> TagsResources = new List<TagsResource>();
                foreach (var item in TagsDoctor)
                {
                    var Tag = _mapperService.Map<Tags, TagsResource>(item);

                    if (Tag != null)
                    {
                        TagsResources.Add(Tag);
                    }
                }
                DoctorListObject.Tags = TagsResources;




                List<PhoneResource> PhoneResources = new List<PhoneResource>();

                foreach (var item in Doctor.Phones)
                {
                    var Phone = _mapperService.Map<Phone, PhoneResource>(item);

                    if (Phone != null)
                    {
                        PhoneResources.Add(Phone);
                    }
                }
                DoctorListObject.Phone = PhoneResources;
                List<LocationDoctorResource> LocationDoctorResources = new List<LocationDoctorResource>();


                foreach (var item in Doctor.LocationDoctor)
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
            try
            {
                DoctorProfile DoctorProfile = new DoctorProfile();

                var BuDoctor = (List<BuDoctor>)await _BuDoctorService.GetByIdDoctor(Id);
                List<string> Names = new List<string>();
                List<BusinessUnit> BusinessUnits = new List<BusinessUnit>();
                //Get the Business Unit Of the Doctor




                var Doctor = await _DoctorService.GetById(Id);
                if (Doctor == null)
                {
                    throw new InvalidOperationException("No Doctor Found with the id " + Id);
                }
                List<BusinessUnitResource> BusinessUnitResources = new List<BusinessUnitResource>();
                foreach (var item in Doctor.BuDoctor)
                {
                    var Bu = _mapperService.Map<BusinessUnit, BusinessUnitResource>(item.Bu);

                    if (Bu != null)
                    {
                        BusinessUnitResources.Add(Bu);
                    }
                }
                DoctorProfile.BusinessUnit = BusinessUnitResources;
                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                var Potentiel = await _PotentielService.GetById(Doctor.IdPotentiel);

                var PotentielResource = _mapperService.Map<Potentiel, PotentielResource>(Potentiel);

                if (Doctor.Potentiel != null)
                {
                    PotentielResource.IdPotentiel = Potentiel.IdPotentiel;
                    PotentielResource.Name = Potentiel.Name;
                    PotentielResource.Status = Potentiel.Status;
                    PotentielResource.Version = Potentiel.Version;
                    DoctorResource.Potentiel = PotentielResource;
                }

                List<Specialities> SpecialitiesList = new List<Specialities>();


                foreach (var item in Doctor.SpecialtyDoctor)
                {
                    var SpecialtyDoctor = item;
                    var Specialty = SpecialtyDoctor.Specialty;
                    var Specialities = new Specialities();
                    Specialities.Abr = Specialty.Abreviation;
                    Specialities.IdSpecialty = Specialty.IdSpecialty;
                    Specialities.NameSpecialty = Specialty.Name;
                    SpecialitiesList.Add(Specialities);
                }
                DoctorResource.Specialities = SpecialitiesList;
                DoctorProfile.Doctor = DoctorResource;
                List<Tags> Tags = new List<Tags>();

                foreach (var item in Doctor.TagsDoctor)
                {
                    var Tag = await _TagsService.GetById(item.IdTags);
                    Tags.Add(Tag);
                }

                List<TagsResource> TagsResources = new List<TagsResource>();
                foreach (var item in Tags)
                {
                    var Tag = _mapperService.Map<Tags, TagsResource>(item);

                    if (Tag != null)
                    {
                        TagsResources.Add(Tag);
                    }
                }
                DoctorProfile.Tags = TagsResources;



                if (Doctor == null) return NotFound();


                List<InfoResource> InfoResources = new List<InfoResource>();

                foreach (var item in Doctor.Info)
                {
                    var Info = _mapperService.Map<Info, InfoResource>(item);

                    if (Info != null)
                    {
                        InfoResources.Add(Info);
                    }
                }
                DoctorProfile.Infos = InfoResources;

                ;
                List<PhoneResource> PhoneResources = new List<PhoneResource>();

                foreach (var item in Doctor.Phones)
                {
                    var Phone = _mapperService.Map<Phone, PhoneResource>(item);

                    if (Phone != null)
                    {
                        PhoneResources.Add(Phone);
                    }
                }
                DoctorProfile.Phone = PhoneResources;
                List<LocationDoctorResource> LocationDoctorResources = new List<LocationDoctorResource>();

                foreach (var item in Doctor.LocationDoctor)
                {
                    var LocationDoctor = _mapperService.Map<LocationDoctor, LocationDoctorResource>(item);
                    if (LocationDoctor != null)
                    {
                        var Location = await _LocationService.GetById(item.IdLocation);
                        var LocationResource = _mapperService.Map<Location, LocationResource>(Location);
                        LocationDoctor.Location = LocationResource;
                        var Service = await _ServiceService.GetById(item.IdService);
                        var ServiceResource = _mapperService.Map<Service, ServiceResource>(Service);
                        LocationDoctor.Service = ServiceResource;

                        LocationDoctorResources.Add(LocationDoctor);
                    }
                }
                DoctorProfile.LocationDoctor = LocationDoctorResources;

                var DoctorVisit = await _VisitReportService.GetByIdDoctor(Id);
                List<VisitReportResource> VisitReportResources = new List<VisitReportResource>();

                foreach (var item in DoctorVisit)
                {
                    var Visit = _mapperService.Map<VisitReport, VisitReportResource>(item);

                    if (Visit != null)
                    {
                        VisitReportResources.Add(Visit);
                    }
                }

                DoctorProfile.VisitReports = VisitReportResources;




                List<ObjectionResource> ObjectionResources = new List<ObjectionResource>();
                List<ObjectionResource> RequestResources = new List<ObjectionResource>();

                foreach (var item in Doctor.Objection)
                {
                    var ObjectionRequest = _mapperService.Map<Objection, ObjectionResource>(item);

                    if (ObjectionRequest != null)
                    {
                        if (ObjectionRequest.RequestObjection == RequestObjection.Objection)
                        {
                            ObjectionResources.Add(ObjectionRequest);
                        }
                        else if (ObjectionRequest.RequestObjection == RequestObjection.Request)
                        {
                            RequestResources.Add(ObjectionRequest);
                        }
                    }
                }
                DoctorProfile.Request = RequestResources;

                DoctorProfile.Objection = ObjectionResources;


                List<RequestRp> RequestRpList = new List<RequestRp>();
                foreach (var item in Doctor.Participant)
                {
                    var RequestRp = await _RequestRpService.GetById(item.IdRequestRp);
                    RequestRpList.Add(RequestRp);
                }
                List<RequestRpResource> RequestRpResources = new List<RequestRpResource>();

                foreach (var item in RequestRpList)
                {
                    var Rp = _mapperService.Map<RequestRp, RequestRpResource>(item);

                    if (Rp != null)
                    {
                        RequestRpResources.Add(Rp);
                    }
                }
                DoctorProfile.RequestRp = RequestRpResources;




                List<CommandeResource> CommandeResources = new List<CommandeResource>();

                foreach (var item in Doctor.Commande)
                {
                    var Commande = _mapperService.Map<Commande, CommandeResource>(item);

                    if (Commande != null)
                    {
                        CommandeResources.Add(Commande);
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
        public async Task<ActionResult<DoctorResource>> ApprouveDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                ErrorHandling ErrorMessag = new ErrorHandling();
                if (Token != "")
                {
                    var claims = _UserService.getPrincipal(Token);
                    if (claims == null)
                    {
                        throw new InvalidOperationException("Token is invalid");
                    }
                    var Role = claims.FindFirst("Role").Value;
                    var IdUser = int.Parse(claims.FindFirst("Id").Value);
                    var DoctorToBeModified = await _DoctorService.GetById(Id);
                    if (DoctorToBeModified == null)
                    {
                        throw new InvalidOperationException("No Doctor is found with the ID "+ Id);
                    }
                    DoctorToBeModified.UpdatedOn = DateTime.UtcNow;
                    DoctorToBeModified.UpdatedBy = IdUser;

                    await _DoctorService.Approuve(DoctorToBeModified, DoctorToBeModified);

                    var DoctorUpdated = await _DoctorService.GetById(Id);
                    if (DoctorUpdated == null)
                    {
                        throw new InvalidOperationException("No Doctor is found with the ID "+ Id);
                    }
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function is used to Reject a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the Doctor.</param>
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<DoctorResource>> RejectDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try
            {
                ErrorHandling ErrorMessag = new ErrorHandling();
                if (Token != "")
                {
                    var claims = _UserService.getPrincipal(Token);
                    if (claims == null)
                    {
                        throw new InvalidOperationException("Invalid Token");
                    }
                    var Role = claims.FindFirst("Role").Value;
                    var IdUser = int.Parse(claims.FindFirst("Id").Value);
                    var DoctorToBeModified = await _DoctorService.GetById(Id);
                    if (DoctorToBeModified == null)
                    {
                        throw new InvalidOperationException("No Doctor is found with the ID " + Id);
                    }
                    DoctorToBeModified.UpdatedOn = DateTime.UtcNow;
                    DoctorToBeModified.UpdatedBy = IdUser;

                    await _DoctorService.Reject(DoctorToBeModified, DoctorToBeModified);

                    var DoctorUpdated = await _DoctorService.GetById(Id);
                    if (DoctorUpdated == null)
                    {
                        throw new InvalidOperationException("No Doctor is found with the ID " + Id);
                    }
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            try
            {
                var claims = _UserService.getPrincipal(Token);
                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token");
                }
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var Role = claims.FindFirst("Role").Value;
                var DoctorToBeModified = await _DoctorService.GetById(Id);
                if (DoctorToBeModified == null)
                {
                    throw new InvalidOperationException("No Doctor is found with the ID " + Id);
                }
                var Doctor = _mapperService.Map<SaveDoctorResourceUpdate, Doctor>(SaveDoctorResource);


                if (Role == "Manager")
                {
                    Doctor.Status = Status.Approuved;
                    Doctor.ManagerApprouved = Id;

                }
                else if (Role == "Delegue")
                {
                    Doctor.Status = Status.Pending;
                    Doctor.ManagerApprouved = 0;

                }
                Doctor.VersionLink = 0;
                Doctor.StatusLink = 0;
                Doctor.LinkedId = null;


                await _DoctorService.Update(DoctorToBeModified, Doctor);
                var DoctorUpdated = await _DoctorService.GetById(Id);
                if (DoctorUpdated == null)
                {
                    throw new InvalidOperationException("No Doctor is found with the ID " + Id);
                }
                List<LocationDoctor> LocationDoctors = new List<LocationDoctor>();

                foreach (var item in SaveDoctorResource.SpecialityList)
                {
                    var Specialty = await _SpecialtyDoctorService.GetById(Id, item);
                    if (Specialty != null)
                    {
                        await _SpecialtyDoctorService.Delete(Specialty);
                    }
                }
                foreach (var item in SaveDoctorResource.SpecialityList)
                {
                    if (item != 0)
                    {
                        var Specialty = await _SpecialtyService.GetById(item);
                        SpecialtyDoctor SpecialtyDoctor = new SpecialtyDoctor();

                        SpecialtyDoctor.IdSpecialty = Specialty.IdSpecialty;
                        SpecialtyDoctor.VersionSpecialty = Specialty.Version;
                        SpecialtyDoctor.StatusSpecialty = Specialty.Status;
                        SpecialtyDoctor.Specialty = Specialty;
                        SpecialtyDoctor.CreatedBy = Id;
                        SpecialtyDoctor.UpdatedBy = Id;
                        SpecialtyDoctor.IdDoctorNavigation = Doctor;
                        SpecialtyDoctor.IdDoctor = Doctor.IdDoctor;
                        SpecialtyDoctor.VersionDoctor = Doctor.Version;
                        SpecialtyDoctor.StatusDoctor = Doctor.Status;

                        SpecialtyDoctor.Status = Status.Approuved;
                        SpecialtyDoctor.Version = 0;
                        SpecialtyDoctor.Active = 0;
                        SpecialtyDoctor.CreatedOn = DateTime.UtcNow;
                        SpecialtyDoctor.UpdatedOn = DateTime.UtcNow;
                     var CreatedSpeciality=await _SpecialtyDoctorService.Create(SpecialtyDoctor);
                        if (CreatedSpeciality == null)
                        {
                            throw new InvalidOperationException("Specialty  Doctor not created "+ SpecialtyDoctor);
                        }
                    }
                }
                var OldLocationDoctor = await _LocationDoctorService.GetAll(Doctor.IdDoctor);
          
                foreach (var item in SaveDoctorResource.Cabinets)
                {

                    if (item != null)
                    {
                        //*** Mappage ***
                        var Location = _mapperService.Map<LocationAdd, Location>(item.Cabinet);
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
                        var Locality1 = await _LocalityService.GetById(item.Cabinet.IdLocality1);
                        Location.NameLocality1 = Locality1.Name;
                        Location.VersionLocality1 = Locality1.Version;
                        Location.StatusLocality1 = Locality1.Status;
                        Location.IdLocality1 = Locality1.IdLocality;
                        var Locality2 = await _LocalityService.GetById(item.Cabinet.IdLocality2);
                        Location.NameLocality2 = Locality2.Name;
                        Location.VersionLocality2 = Locality2.Version;
                        Location.StatusLocality2 = Locality2.Status;
                        Location.IdLocality2 = Locality1.IdLocality;
                        var NewLocationType = await _LocationTypeService.GetById(item.Cabinet.IdLocationType);
                        Location.NameLocationType = NewLocationType.Name;
                        Location.StatusLocationType = NewLocationType.Status;
                        Location.VersionLocationType = NewLocationType.Version;
                        Location.TypeLocationType = NewLocationType.Type;
                        Location.CreatedBy = Id;
                        Location.UpdatedBy = Id;
                        if (item.Cabinet.IdBrick1 != 0)
                        {
                            var Brick1 = await _BrickService.GetByIdActif(item.Cabinet.IdBrick1);
                            if (Brick1 != null)
                            {
                                Location.IdBrick1 = Brick1.IdBrick;
                                Location.VersionBrick1 = Brick1.Version;
                                Location.StatusBrick1 = Brick1.Status;
                                Location.NameBrick1 = Brick1.Name;
                                Location.NumBrick1 = Brick1.NumSystemBrick;


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
                        if (item.Cabinet.IdBrick2 != 0)
                        {
                            var Brick2 = await _BrickService.GetByIdActif(item.Cabinet.IdBrick2);

                            if (Brick2 != null)
                            {
                                Location.IdBrick2 = Brick2.IdBrick;
                                Location.VersionBrick2 = Brick2.Version;
                                Location.StatusBrick2 = Brick2.Status;
                                Location.NameBrick2 = Brick2.Name;
                                Location.NumBrick2 = Brick2.NumSystemBrick;


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
                        if (NewCabinet == null)
                        {
                            throw new InvalidOperationException("Cabinet not created " + NewCabinet);
                        }
                        //*** Mappage ***
                        var CabinetResource = _mapperService.Map<Location, LocationResource>(NewCabinet);
                        LocationDoctor LocationDoctor = new LocationDoctor();
                        LocationDoctor.IdLocation = CabinetResource.IdLocation;
                        LocationDoctor.IdDoctor = Doctor.IdDoctor;
                        LocationDoctor.IdService = null;
                        LocationDoctor.Status = Status.Approuved;
                        LocationDoctor.Version = 0;
                        LocationDoctor.Active = 0;
                        LocationDoctor.CreatedOn = DateTime.UtcNow;
                        LocationDoctor.UpdatedOn = DateTime.UtcNow;
                        LocationDoctor.CreatedBy = Id;
                        LocationDoctor.UpdatedBy = Id;
                       var NewLocationDoctor= await _LocationDoctorService.Create(LocationDoctor);
                        if (NewLocationDoctor == null)
                        {
                            throw new InvalidOperationException("Location Doctor not created " + NewLocationDoctor);
                        }
                    }
                }
                if (SaveDoctorResource.Location != null)
                {


                    foreach (var i in OldLocationDoctor)
                    {
                        LocationDoctors.Add(i);
                    }

                    await _LocationDoctorService.DeleteRange(LocationDoctors);

                    foreach (var item in SaveDoctorResource.Location)
                    {
                        var location = await _LocationService.GetById(item.IdLocation);

                        if (location != null)
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
                            var NewLocationDoctor=await _LocationDoctorService.Create(LocationDoctor);
                            if (NewLocationDoctor == null)
                            {
                                throw new InvalidOperationException("Location Doctor not created " + NewLocationDoctor);
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
                        if (Bu == null)
                        {
                            throw new InvalidOperationException("Business Unit not found " + item);
                        }
                        if (Bu != null)
                        {
                            var BuDoctorExist = await _BuDoctorService.GetById(Bu.IdBu, DoctorUpdated.IdDoctor);
                      
                            if (BuDoctorExist != null)
                            {
                                BuDoctor BuDoctor = new BuDoctor();
                                BuDoctor.IdBu = Bu.IdBu;
                                BuDoctor.Version = Bu.Version;
                                BuDoctor.Status = Bu.Status;
                                BuDoctor.NameBu = Bu.Name;
                                BuDoctor.Bu = Bu;
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


                if (SaveDoctorResource.Tags != null)
                {
                    foreach (var item in SaveDoctorResource.Tags)
                    {
                        var TagExist = await _TagsService.GetByExistantActif(item.Name);
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
                                var newTag = await _TagsService.Create(NewTag);


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
                        TagsDoctor.Tags = item;
                        var TagsDoctorExist = await _TagsDoctorService.GetByIdActif(TagsDoctor.IdDoctor, TagsDoctor.IdTags);
                        if (TagsDoctorExist == null)
                        {
                            var TagDoctorCreated= await _TagsDoctorService.Create(TagsDoctor);
                            if (TagDoctorCreated == null)
                            {
                                throw new InvalidOperationException("Tag Doctor not found " + TagDoctorCreated);
                            }
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
                        if (Oldinfo == null)
                        {
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
                            var InfoCreated=await _InfoService.Create(Info);
                            if (InfoCreated == null)
                            {
                                throw new InvalidOperationException("Info Not Created " + InfoCreated);
                            }
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
                        var oldPhone = await _PhoneService.GetByIdDoctor(item.PhoneNumber, DoctorUpdated.IdDoctor);
                        if (oldPhone == null)
                        {
                            Phones.Add(Phone);
                        }
                    }
                    if (Phones.Count > 0)
                    {
                        await _PhoneService.CreateRange(Phones);
                    }
                }




                var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);
                var DoctorResourceInfo = await GetById(DoctorResourceUpdated.IdDoctor);

                return Ok(DoctorResourceInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        ///  This function is used to delete a Doctor
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the doctor.</param>

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try
            {

                var sub = await _DoctorService.GetById(Id);
                if (sub == null) return BadRequest("Le Doctor  n'existe pas");
                await _DoctorService.Delete(sub);

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
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, List<int> Ids)
        {
            try
            {
                List<Doctor> empty = new List<Doctor>();
                foreach (var item in Ids)
                {
                    var sub = await _DoctorService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Doctor  n'existe pas");

                }
                await _DoctorService.DeleteRange(empty);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
