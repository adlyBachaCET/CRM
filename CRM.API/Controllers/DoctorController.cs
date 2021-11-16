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

    public class DoctorController : ControllerBase
    {
        public IList<Doctor> Doctors;

        private readonly IDoctorService _DoctorService;

        private readonly IBusinessUnitService _BusinessUnitService;

        private readonly IBuDoctorService _BuDoctorService;
        private readonly IServiceService _ServiceService;

        private readonly ILocationDoctorService _LocationDoctorService;
        private readonly ILocationService _LocationService;
        private readonly IPhoneService _PhoneService;
        private readonly ITagsDoctorService _TagsDoctorService;
        private readonly ITagsService _TagsService;

        private readonly IInfoService _InfoService;
        private readonly IPotentielService _PotentielService;
        private readonly ISpecialtyService _SpecialtyService;
        private readonly IMapper _mapperService;
        public DoctorController(ILocationDoctorService LocationDoctorService,
            IPhoneService PhoneService,
            ILocationService LocationService,
            IServiceService ServiceService,
            IDoctorService DoctorService,
            IPotentielService PotentielService,
            ISpecialtyService SpecialtyService,
            IBusinessUnitService BusinessUnitService,
            IInfoService InfoService,
            ITagsService TagsService,
            ITagsDoctorService TagsDoctorService,

            IBuDoctorService BuDoctorService, IMapper mapper)
        {
            _TagsDoctorService = TagsDoctorService;
            _LocationDoctorService = LocationDoctorService;
            _LocationService = LocationService;
            _TagsService = TagsService;

            _PotentielService = PotentielService;
            _BuDoctorService = BuDoctorService;
            _DoctorService = DoctorService;
            _ServiceService = ServiceService;
            _SpecialtyService=SpecialtyService;
            _PhoneService = PhoneService;
            _BusinessUnitService=BusinessUnitService;
           _InfoService = InfoService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(SaveDoctorResource SaveDoctorResource)
        {

            //*** Mappage ***
            var Doctor = _mapperService.Map<SaveDoctorResource, Doctor>(SaveDoctorResource);
            Doctor.Status = Status.Approuved;
            Doctor.Version = 0;
            Doctor.Active = 0;
            Doctor.CreatedOn = DateTime.UtcNow;
            Doctor.UpdatedOn = DateTime.UtcNow;
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

            if (SaveDoctorResource.Tags != null)
            {
                foreach(var item in SaveDoctorResource.Tags) {
                var Tag = await _TagsService.GetByExistantActif(item.Name);
                    if(Tag==null)
                    {
                   var NewTag = _mapperService.Map<SaveTagsResource, Tags>(item);
                   NewTag.Status = Status.Approuved;
                   NewTag.Version = 0;
                   NewTag.Active = 0;
                   NewTag.CreatedOn = DateTime.UtcNow;
                   NewTag.UpdatedOn = DateTime.UtcNow;
                   var TagCreated=await _TagsService.Create(NewTag);
                  var TagResource = _mapperService.Map<Tags, TagsResource>(TagCreated);

                        TagsDoctor TagsDoctor = new TagsDoctor();
                        TagsDoctor.Status = Status.Approuved;
                        TagsDoctor.Version = 0;
                        TagsDoctor.Active = 0;
                        TagsDoctor.CreatedOn = DateTime.UtcNow;
                        TagsDoctor.UpdatedOn = DateTime.UtcNow;
                        TagsDoctor.IdDoctor = NewDoctor.IdDoctor;
                        TagsDoctor.IdDoctorNavigation = NewDoctor;
                        var TagResourceNavigation = _mapperService.Map<TagsResource, Tags>(TagResource);

                        TagsDoctor.IdTags = TagResourceNavigation.IdTags;
                        TagsDoctor.IdTagsNavigation = TagResourceNavigation;
                        await _TagsDoctorService.Create(TagsDoctor);
                    }
                    else
                    {
                        TagsDoctor TagsDoctor = new TagsDoctor();
                        TagsDoctor.Status = Status.Approuved;
                        TagsDoctor.Version = 0;
                        TagsDoctor.Active = 0;
                        TagsDoctor.CreatedOn = DateTime.UtcNow;
                        TagsDoctor.UpdatedOn = DateTime.UtcNow;
                        TagsDoctor.IdDoctor = NewDoctor.IdDoctor;
                        TagsDoctor.IdDoctorNavigation = NewDoctor;
                        //var TagResource = await _TagsService.GetByExistantActif(item.Name);
                        TagsDoctor.IdTags = Tag.IdTags;
                       TagsDoctor.IdTagsNavigation = Tag;

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
                    var Info = _mapperService.Map<SaveInfoResource,Info>(item);
                    Info.IdDoctor = NewDoctor.IdDoctor;
                    Info.IdDoctorNavigation = NewDoctor;
                    Info.CreatedOn = DateTime.UtcNow;
                    Info.UpdatedOn = DateTime.UtcNow;
                    Info.Active = 0;
                    Info.Version = 0;
                    Info.Status = Status.Approuved;
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
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<DoctorResource>> GetPharmacysNumber(int Number)
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
        public async Task<ActionResult<DoctorResource>> GetAllDoctors()
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
        public async Task<ActionResult<DoctorResource>> GetAllDoctorsByBu(int Id)
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
        public async Task<ActionResult<DoctorResource>> GetAllActifDoctors()
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
        public async Task<ActionResult<DoctorResource>> GetAllInactifDoctors()
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
        public async Task<ActionResult<DoctorResource>> GetAllAssignedDoctors()
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
        public async Task<ActionResult<DoctorResource>> GetAllNotAssignedDoctors()
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
        public async Task<ActionResult<DoctorResource>> GetAllAssignedDoctorsByBu(int Id)
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
        public async Task<ActionResult<DoctorResource>> GetAllNotAssignedDoctorsByBu(int Id)
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
        public async Task<ActionResult<DoctorResource>> GetDoctorById(int Id)
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



                DoctorProfile.BusinessUnit = BusinessUnits;
                var Doctors = await _DoctorService.GetById(Id);
                DoctorProfile.Doctor = Doctors;
                var TagsDoctor = (List<TagsDoctor>)await _TagsDoctorService.GetByIdActif(Id);
                List<Tags> Tags = new List<Tags>();

                foreach (var item in TagsDoctor)
                {
                    var Tag = await _TagsService.GetById(item.IdTags);
                    Tags.Add(Tag);
                }
                DoctorProfile.Tags = Tags;

                if (Doctors == null) return NotFound();
                var Info = (List<Info>)await _InfoService.GetByIdDoctor(Id);
                DoctorProfile.Infos = Info;
                var Phone = (List<Phone>)await _PhoneService.GetAllById(Id);
                DoctorProfile.Phone = Phone;
                var DoctorLocation = await _LocationDoctorService.GetAllAcceptedActif(Id);

                return Ok(DoctorProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<Location>> ApprouveLocation(int Id)
        {

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            //var newLocation = await _LocationService.Create(Locations);
            // Locations.CreatedOn = SaveLocationResource.;
            await _LocationService.Approuve(LocationToBeModified, LocationToBeModified);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok(LocationResourceUpdated);
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<Location>> RejectLocation(int Id)
        {

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            //var newLocation = await _LocationService.Create(Locations);
            // Locations.CreatedOn = SaveLocationResource.;
            LocationToBeModified.UpdatedOn = DateTime.UtcNow;
            await _LocationService.Reject(LocationToBeModified, LocationToBeModified);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok(LocationResourceUpdated);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int Id, SaveDoctorResource SaveDoctorResource)
        {

            var DoctorToBeModified = await _DoctorService.GetById(Id);
            if (DoctorToBeModified == null) return BadRequest("Le Doctor n'existe pas"); //NotFound();
            var Doctors = _mapperService.Map<SaveDoctorResource, Doctor>(SaveDoctorResource);
            //var newDoctor = await _DoctorService.Create(Doctors);

            await _DoctorService.Update(DoctorToBeModified, Doctors);

            var DoctorUpdated = await _DoctorService.GetById(Id);

            var DoctorResourceUpdated = _mapperService.Map<Doctor, DoctorResource>(DoctorUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDoctor(int Id)
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
        public async Task<ActionResult> DeleteRange(List<int> Ids)
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
