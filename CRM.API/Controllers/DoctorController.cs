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

    

            if (SaveDoctorResource.Establishments != null)
            {
                var Esa = _mapperService.Map<List<SaveLocationResource>, List<Location>>(SaveDoctorResource.Establishments);

                var listLocation = await _LocationService.CreateRange(Esa);

                foreach (var item in listLocation)
                {
                    LocationDoctor LocationDoctor = new LocationDoctor();
                    LocationDoctor.IdLocation = item.IdLocation;
                    LocationDoctor.VersionLocation = item.Version;
                    LocationDoctor.StatusLocation = item.Status;

                    var Bu = await _LocationService.GetById(item.IdLocation);

                    LocationDoctor.IdLocationNavigation = item;

                    LocationDoctor.IdDoctorNavigation = NewDoctor;
                    LocationDoctor.IdDoctor = NewDoctor.IdDoctor;
                    LocationDoctor.VersionDoctor = NewDoctor.Version;
                    LocationDoctor.StatusDoctor = NewDoctor.Status;

                    await _LocationDoctorService.Create(LocationDoctor);

                }
            }
           /* if (SaveDoctorResource.TagsDoctor != null)
            {
                var Esa = _mapperService.Map<List<SaveTagsResource>, List<Tags>>(SaveDoctorResource.TagsDoctor);

                var listTags = await _TagsService.CreateRange(Esa);

                foreach (var item in listLocation)
                {
                    LocationDoctor LocationDoctor = new LocationDoctor();
                    LocationDoctor.IdLocation = item.IdLocation;
                    var Bu = await _LocationService.GetById(item.IdLocation);

                    LocationDoctor.IdLocationNavigation = item;

                    LocationDoctor.IdDoctorNavigation = NewDoctor;
                    LocationDoctor.IdDoctor = NewDoctor.IdDoctor;
                    await _LocationDoctorService.Create(LocationDoctor);

                }
            }*/
             if (SaveDoctorResource.Specialtys != null)
             {
                var Esa = _mapperService.Map<List<SaveSpecialtyResource>, List<Specialty>>(SaveDoctorResource.Specialtys);


                foreach (var item in Esa)
                 {
                     SpecialityDoctor SpecialityDoctor = new SpecialityDoctor();
                     var Specialty = await _SpecialtyService.GetById(item.IdSpecialty);
                   
                    SpecialityDoctor.IdSpecialty = Specialty.IdSpecialty;
                    SpecialityDoctor.IdSpecialtyNavigation = Specialty;
                    SpecialityDoctor.VersionSpecialty = Specialty.Version;
                    SpecialityDoctor.StatusSpecialty = Specialty.Status;

                    SpecialityDoctor.IdDoctor = NewDoctor.IdDoctor;
                    SpecialityDoctor.IdDoctorNavigation = NewDoctor;
                    SpecialityDoctor.VersionDoctor = NewDoctor.Version;
                    SpecialityDoctor.StatusDoctor = NewDoctor.Status;

                }
             }

            if (SaveDoctorResource.Infos != null)
            {
                var Infos = _mapperService.Map<List<SaveInfoResource>, List<Info>>(SaveDoctorResource.Infos);

                var listInfo = await _InfoService.CreateRange(Infos);

                foreach (var item in listInfo)
                {
                    item.IdDoctor = NewDoctor.IdDoctor;
                    item.IdDoctorNavigation = NewDoctor;
              

                }
            }
            if (SaveDoctorResource.Phones != null)
            {
                var Phones = _mapperService.Map<List<SavePhoneResource>, List<Phone>>(SaveDoctorResource.Phones);

                var listPhone = await _PhoneService.CreateRange(Phones);

                foreach (var item in listPhone)
                {
                    item.IdDoctor = NewDoctor.IdDoctor;
                    item.Doctor = NewDoctor;

                    //await _LocationDoctorService.Create(LocationDoctor);

                }
            }
            //*** Creation dans la base de données ***
            //*** Mappage ***
            var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(NewDoctor);
            return Ok(DoctorResource);
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
