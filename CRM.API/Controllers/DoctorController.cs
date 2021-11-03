using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
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
        private readonly IServiceDoctorService _ServiceDoctorService;
        private readonly IServiceService _ServiceService;

        private readonly IEstablishmentDoctorService _EstablishmentDoctorService;
        private readonly IEstablishmentService _EstablishmentService;
        private readonly IPhoneService _PhoneService;

        private readonly IInfoService _InfoService;
        private readonly IPotentielService _PotentielService;
        private readonly ISpecialtyService _SpecialtyService;
        private readonly ISpecialityDoctorService _SpecialityDoctorService;
        private readonly IMapper _mapperService;
        public DoctorController(IEstablishmentDoctorService EstablishmentDoctorService,
            IPhoneService PhoneService,
            IEstablishmentService EstablishmentService,
            IServiceDoctorService ServiceDoctorService,
            IServiceService ServiceService,
            IDoctorService DoctorService,
            IPotentielService PotentielService,
            ISpecialtyService SpecialtyService,
            IBusinessUnitService BusinessUnitService,
            ISpecialityDoctorService SpecialityDoctorService,
            IInfoService InfoService,

            IBuDoctorService BuDoctorService, IMapper mapper)
        {
            _EstablishmentDoctorService = EstablishmentDoctorService;
            _EstablishmentService = EstablishmentService;
            _PotentielService=PotentielService;
            _BuDoctorService = BuDoctorService;
            _DoctorService = DoctorService;
            _ServiceDoctorService = ServiceDoctorService;
            _ServiceService = ServiceService;
            _SpecialtyService=SpecialtyService;
            _PhoneService = PhoneService;
            _SpecialityDoctorService=SpecialityDoctorService;
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

            if (SaveDoctorResource.BusinessUnits != null) { 
            foreach (var item in SaveDoctorResource.BusinessUnits)
            {
                BuDoctor Budoctor = new BuDoctor();
                var Bu = await _BusinessUnitService.GetById(item.idBu);
                Budoctor.IdBu = Bu.IdBu;
                Budoctor.IdBuNavigation = Bu;
                Budoctor.IdDoctor = NewDoctor.IdDoctor;
                Budoctor.IdDoctorNavigation = NewDoctor;
                await _BuDoctorService.Create(Budoctor);
            }
            }

            if (SaveDoctorResource.Establishments != null)
            {
                var Esa = _mapperService.Map<List<SaveEstablishmentResource>, List<Establishment>>(SaveDoctorResource.Establishments);

                var listEstablishment = await _EstablishmentService.CreateRange(Esa);

                foreach (var item in listEstablishment)
                {
                    EstablishmentDoctor EstablishmentDoctor = new EstablishmentDoctor();
                    EstablishmentDoctor.IdEstablishment = item.IdEstablishment;
                    var Bu = await _EstablishmentService.GetById(item.IdEstablishment);

                    EstablishmentDoctor.IdEstablishmentNavigation = item;

                    EstablishmentDoctor.IdDoctorNavigation = NewDoctor;
                    EstablishmentDoctor.IdDoctor = NewDoctor.IdDoctor;
                    await _EstablishmentDoctorService.Create(EstablishmentDoctor);

                }
            }
            if (SaveDoctorResource.Specialtys != null)
            {
                var Esa = _mapperService.Map<List<SaveEstablishmentResource>, List<Establishment>>(SaveDoctorResource.Establishments);

                var listEstablishment = await _EstablishmentService.CreateRange(Esa);

                foreach (var item in listEstablishment)
                {
                    EstablishmentDoctor EstablishmentDoctor = new EstablishmentDoctor();
                    EstablishmentDoctor.IdEstablishment = item.IdEstablishment;
                    var Bu = await _EstablishmentService.GetById(item.IdEstablishment);

                    EstablishmentDoctor.IdEstablishmentNavigation = item;

                    EstablishmentDoctor.IdDoctorNavigation = NewDoctor;
                    EstablishmentDoctor.IdDoctor = NewDoctor.IdDoctor;
                    await _EstablishmentDoctorService.Create(EstablishmentDoctor);

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
                    item.IdDoctorNavigation = NewDoctor;

                    //await _EstablishmentDoctorService.Create(EstablishmentDoctor);

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
