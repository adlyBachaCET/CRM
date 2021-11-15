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

            IBuDoctorService BuDoctorService, IMapper mapper)
        {
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
            var NewDoctor = await _DoctorService.Create(Doctor);
            var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(NewDoctor);

            if (SaveDoctorResource.Location!=null)
            {
                foreach(var item in SaveDoctorResource.Location)
                { 
                var location = await _LocationService.GetByExistantActif(item.Name, item.IdLocationType);
                }
            }

            if (SaveDoctorResource.Location != null)
            {
                var Esa = _mapperService.Map<List<SaveLocationResource>, List<Location>>(SaveDoctorResource.Location);

               // var listLocation = await _LocationService.CreateRange(Esa);

                foreach (var item in Esa)
                {
                    LocationDoctor LocationDoctor = new LocationDoctor();
                    LocationDoctor.IdLocation = item.IdLocation;
                    LocationDoctor.VersionLocation = item.Version;
                    LocationDoctor.StatusLocation = item.Status;
                    LocationDoctor.IdLocationNavigation = item;

                    LocationDoctor.IdDoctorNavigation = NewDoctor;
                    LocationDoctor.IdDoctor = NewDoctor.IdDoctor;
                    LocationDoctor.VersionDoctor = NewDoctor.Version;
                    LocationDoctor.StatusDoctor = NewDoctor.Status;



                    LocationDoctor.Status = Status.Approuved;
                    LocationDoctor.Version = 0;
                    LocationDoctor.Active = 0;
                    LocationDoctor.CreatedOn = DateTime.UtcNow;
                    LocationDoctor.UpdatedOn = DateTime.UtcNow;
                    await _LocationDoctorService.Create(LocationDoctor);
                  

                }
            }
            //Assign business units  The new Doctor

            if (SaveDoctorResource.BusinessUnits != null)
            {
                var ListBu = _mapperService.Map<List<SaveBusinessUnitResource>, List<BusinessUnit>>(SaveDoctorResource.BusinessUnits);


                foreach (var item in ListBu)
                {
                    BuDoctor BuDoctor = new BuDoctor();
                    BuDoctor.IdBu = item.IdBu;
                    BuDoctor.Version = item.Version;
                    BuDoctor.Status = item.Status;

                    var Bu = await _BusinessUnitService.GetById(item.IdBu);

                    BuDoctor.IdBuNavigation= item;

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

            if (SaveDoctorResource.TagsDoctor != null)
            {
                foreach(var item in SaveDoctorResource.TagsDoctor) {
                var tag = await _TagsService.GetByExistantActif(item.Name);
                    if(tag==null)
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
                        var TagResource = await _TagsService.GetByExistantActif(item.Name);
                        TagsDoctor.IdTags = TagResource.IdTags;
                       TagsDoctor.IdTagsNavigation = TagResource;
                        await _TagsDoctorService.Create(TagsDoctor);
                    }

                        }


            }


            //Add Infos to database with the Id The new Doctor

            if (SaveDoctorResource.Infos != null)
            {
                var Infos = _mapperService.Map<List<SaveInfoResource>, List<Info>>(SaveDoctorResource.Infos);
                foreach (var item in Infos)
                {
                    item.IdDoctor = NewDoctor.IdDoctor;
                    item.IdDoctorNavigation = NewDoctor;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                    item.Active = 0;
                    item.Version = 0;
                    item.Status = Status.Approuved;

                }

               await _InfoService.CreateRange(Infos);

             
            }

            //Add Phones to database with the Id The new Doctor
            if (SaveDoctorResource.Phones != null)
            {
             var Phones = _mapperService.Map<List<SavePhoneResource>, List<Phone>>(SaveDoctorResource.Phones);
             foreach(var item in Phones)
                {
                    item.IdDoctor = NewDoctor.IdDoctor;
                    item.IdPharmacy = 0;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                    item.Active = 0;
                    item.Version = 0;
                    item.Status = Status.Approuved;
                }
          await _PhoneService.CreateRange(Phones);

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
            try
            {
                var Doctors = await _DoctorService.GetById(Id);
                if (Doctors == null) return NotFound();
                var DoctorRessource = _mapperService.Map<Doctor, DoctorResource>(Doctors);
                return Ok(DoctorRessource);
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
