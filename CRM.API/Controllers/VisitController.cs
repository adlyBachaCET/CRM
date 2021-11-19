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
    [EnableCors("AllowOrigin")]

    public class VisitController : ControllerBase
    {
        public IList<Visit> Visits;

        private readonly IVisitService _VisitService;
        private readonly ILocalityService _LocalityService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IMapper _mapperService;
        public VisitController(ILocalityService LocalityService, IVisitService VisitService, IPharmacyService PharmacyService, IDoctorService DoctorService, IMapper mapper)
        {
            _LocalityService = LocalityService;
            _DoctorService = DoctorService;
            _PharmacyService = PharmacyService;

            _VisitService = VisitService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<VisitResource>> CreateVisit(SaveVisitResource SaveVisitResource)
  {     
            //*** Mappage ***
            var Visit = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitResource);
            var Locality1 = await _LocalityService.GetById(SaveVisitResource.IdLocality1);
            Visit.NameLocality1 = Locality1.Name;
            Visit.VersionLocality1 = Locality1.Version;
            Visit.StatusLocality1 = Locality1.Status;
            Visit.IdLocality1 = Locality1.IdLocality;
            var Locality2 = await _LocalityService.GetById(SaveVisitResource.IdLocality2);
            Visit.NameLocality2 = Locality1.Name;
            Visit.VersionLocality2 = Locality1.Version;
            Visit.StatusLocality2 = Locality1.Status;
            Visit.IdLocality2 = Locality1.IdLocality;
            var Doctor = await _DoctorService.GetById(SaveVisitResource.IdDoctor);
            Visit.VersionDoctor = Doctor.Version;
            Visit.StatusDoctor = Doctor.Status;
            Visit.IdDoctor = Doctor.IdDoctor;
            var Pharmacy = await _PharmacyService.GetById(SaveVisitResource.IdPharmacy);
            Visit.VersionPharmacy = Pharmacy.Version;
            Visit.StatusPharmacy = Pharmacy.Status;
            Visit.IdPharmacy = Pharmacy.IdPharmacy;
            if (Pharmacy != null)
            {
                Visit.Name = Pharmacy.Name;
                Visit.Pharmacy = Pharmacy;
                Visit.VersionPharmacy = Pharmacy.Version;
                Visit.StatusPharmacy = Pharmacy.Status;
                Visit.Doctor = null;
                Visit.VersionDoctor = null;
                Visit.StatusDoctor = null;
            }
            if (Doctor != null)
            {
                Visit.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                Visit.Doctor = Doctor;
                Visit.VersionDoctor = Doctor.Version;
                Visit.StatusDoctor = Doctor.Status;

                Visit.Pharmacy = null;
                Visit.VersionPharmacy = null;
                Visit.StatusPharmacy = null;
            }
            Visit.CreatedOn = DateTime.UtcNow;
            Visit.UpdatedOn = DateTime.UtcNow;
            Visit.Active = 0;
            Visit.Version = 0;
            Visit.CreatedBy = 0;
            Visit.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewVisit = await _VisitService.Create(Visit);
            //*** Mappage ***
            var VisitResource = _mapperService.Map<Visit, VisitResource>(NewVisit);
            return Ok(VisitResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<VisitResource>> GetAllVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAll();
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
        public async Task<ActionResult<VisitResource>> GetAllActifVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAllActif();
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
        public async Task<ActionResult<VisitResource>> GetAllInactifVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAllInActif();
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
        public async Task<ActionResult<VisitResource>> GetVisitById(int Id)
        {
            try
            {
                var Visits = await _VisitService.GetById(Id);
                if (Visits == null) return NotFound();
                var VisitRessource = _mapperService.Map<Visit, VisitResource>(Visits);
                return Ok(VisitRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<VisitResource>> UpdateVisit(int Id, SaveVisitResource SaveVisitResource)
        {

            var VisitToBeModified = await _VisitService.GetById(Id);
            if (VisitToBeModified == null) return BadRequest("Le Visit n'existe pas"); //NotFound();
            var Visits = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitResource);
            //var newVisit = await _VisitService.Create(Visits);

            await _VisitService.Update(VisitToBeModified, Visits);

            var VisitUpdated = await _VisitService.GetById(Id);

            var VisitResourceUpdated = _mapperService.Map<Visit, VisitResource>(VisitUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVisit(int Id)
        {
            try
            {

                var sub = await _VisitService.GetById(Id);
                if (sub == null) return BadRequest("Le Visit  n'existe pas"); //NotFound();
                await _VisitService.Delete(sub);
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
                List<Visit> empty = new List<Visit>();
                foreach (var item in Ids)
                {
                    var sub = await _VisitService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Visit  n'existe pas"); //NotFound();

                }
                await _VisitService.DeleteRange(empty);
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
