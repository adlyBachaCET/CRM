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

    public class ParticipantVisitController : ControllerBase
    {
        public IList<ParticipantVisit> ParticipantVisits;

        private readonly IParticipantVisitService _ParticipantVisitService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IVisitReportService _VisitReportService;

        private readonly IMapper _mapperService;
        public ParticipantVisitController(IVisitReportService VisitReportService,
            IUserService UserService, IPharmacyService PharmacyService,
            IDoctorService DoctorService,IParticipantVisitService ParticipantVisitService, IMapper mapper)
        {
            _VisitReportService = VisitReportService;
            _UserService = UserService;
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
           _ParticipantVisitService = ParticipantVisitService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ParticipantVisitResource>> CreateParticipantVisit(SaveParticipantVisitResource SaveParticipantVisitResource)
  {     
            //*** Mappage ***
            var ParticipantVisit = _mapperService.Map<SaveParticipantVisitResource, ParticipantVisit>(SaveParticipantVisitResource);
            ParticipantVisit.UpdatedOn = DateTime.UtcNow;
            ParticipantVisit.CreatedOn = DateTime.UtcNow;
            ParticipantVisit.Active = 0;
            ParticipantVisit.Status = 0;
            ParticipantVisit.UpdatedBy = 0;
            ParticipantVisit.CreatedBy = 0;
            var Doctor = await _DoctorService.GetById(SaveParticipantVisitResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveParticipantVisitResource.IdPharmacy);

            if (Pharmacy != null)
            {
                ParticipantVisit.IdPharmacy = Pharmacy.IdPharmacy;

                ParticipantVisit.IdPharmacyNavigation = Pharmacy;
                ParticipantVisit.VersionPharmacy = Pharmacy.Version;
                ParticipantVisit.StatusPharmacy = Pharmacy.Status;
                ParticipantVisit.IdDoctorNavigation = null;
                ParticipantVisit.VersionDoctor = null;
                ParticipantVisit.StatusDoctor = null;

            }
            if (Doctor != null)
            {
                ParticipantVisit.IdDoctor = Doctor.IdDoctor;

                ParticipantVisit.IdDoctorNavigation = Doctor;
                ParticipantVisit.VersionDoctor = Doctor.Version;
                ParticipantVisit.StatusDoctor = Doctor.Status;
                ParticipantVisit.IdPharmacy = null;

                ParticipantVisit.IdPharmacyNavigation = null;
                ParticipantVisit.VersionPharmacy = null;
                ParticipantVisit.StatusPharmacy = null;
            }

            var VisitReport = await _VisitReportService.GetById(SaveParticipantVisitResource.IdVisitReport);
            ParticipantVisit.IdVisitReportNavigation = VisitReport;
            ParticipantVisit.VersionVisitReport = VisitReport.Version;
            ParticipantVisit.StatusVisitReport = VisitReport.Status;

            var User = await _UserService.GetById(SaveParticipantVisitResource.IdUser);
            ParticipantVisit.IdUserNavigation = User;
            ParticipantVisit.VersionUser = User.Version;
            ParticipantVisit.StatusUser = User.Status;
            ParticipantVisit.IdUser = User.IdUser;

            //*** Creation dans la base de données ***
            var NewParticipantVisit = await _ParticipantVisitService.Create(ParticipantVisit);
            //*** Mappage ***
            var ParticipantVisitResource = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(NewParticipantVisit);
            return Ok(ParticipantVisitResource);
      
        }
        [HttpPost("Range")]
        public async Task<ActionResult<ParticipantVisitResource>> CreateRangeParticipantVisit(ListSaveParticipantVisitResource SaveParticipantVisitResource)
        {

            if (SaveParticipantVisitResource.IdDoctor.Count>0)
            {
                foreach(var item in SaveParticipantVisitResource.IdDoctor) { 
                //*** Mappage ***
                var ParticipantVisit = new ParticipantVisit();
                ParticipantVisit.UpdatedOn = DateTime.UtcNow;
                ParticipantVisit.CreatedOn = DateTime.UtcNow;
                ParticipantVisit.Active = 0;
                ParticipantVisit.Status = 0;
                ParticipantVisit.UpdatedBy = 0;
                ParticipantVisit.CreatedBy = 0;
                var Doctor = await _DoctorService.GetById(item);
                ParticipantVisit.IdDoctor = Doctor.IdDoctor;
               ParticipantVisit.IdDoctorNavigation = Doctor;
               ParticipantVisit.VersionDoctor = Doctor.Version;
               ParticipantVisit.StatusDoctor = Doctor.Status;
               ParticipantVisit.IdPharmacy = null;
               ParticipantVisit.IdPharmacyNavigation = null;
               ParticipantVisit.VersionPharmacy = null;
               ParticipantVisit.StatusPharmacy = null;
               ParticipantVisit.IdUserNavigation = null;
               ParticipantVisit.VersionUser = null;
               ParticipantVisit.StatusUser = null;
               ParticipantVisit.IdUser = null;

            var VisitReport = await _VisitReportService.GetById(SaveParticipantVisitResource.IdVisitReport);
               ParticipantVisit.IdVisitReportNavigation = VisitReport;
               ParticipantVisit.VersionVisitReport = VisitReport.Version;
               ParticipantVisit.StatusVisitReport = VisitReport.Status;
                    ParticipantVisit.IdVisitReport = VisitReport.IdReport;


                    //*** Creation dans la base de données ***
                    var NewParticipantVisit = await _ParticipantVisitService.Create(ParticipantVisit);
                var ParticipantVisitResource = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(NewParticipantVisit);
                }
            }

            if (SaveParticipantVisitResource.IdPharmacy.Count > 0)
            {
                foreach (var item in SaveParticipantVisitResource.IdPharmacy)
                {
                    //*** Mappage ***
                    var ParticipantVisit = new ParticipantVisit();
                    ParticipantVisit.UpdatedOn = DateTime.UtcNow;
                    ParticipantVisit.CreatedOn = DateTime.UtcNow;
                    ParticipantVisit.Active = 0;
                    ParticipantVisit.Status = 0;
                    ParticipantVisit.UpdatedBy = 0;
                    ParticipantVisit.CreatedBy = 0;
                    var Pharmacy = await _PharmacyService.GetById(item);
                    ParticipantVisit.IdDoctor =null;
                    ParticipantVisit.IdDoctorNavigation = null;
                    ParticipantVisit.VersionDoctor = null;
                    ParticipantVisit.StatusDoctor = null;
                    ParticipantVisit.IdPharmacy = Pharmacy.IdPharmacy;
                    ParticipantVisit.IdPharmacyNavigation = Pharmacy;
                    ParticipantVisit.VersionPharmacy = Pharmacy.Version;
                    ParticipantVisit.StatusPharmacy = Pharmacy.Status;
                    ParticipantVisit.IdUserNavigation = null;
                    ParticipantVisit.VersionUser = null;
                    ParticipantVisit.StatusUser = null;
                    ParticipantVisit.IdUser = null;

                    var VisitReport = await _VisitReportService.GetById(SaveParticipantVisitResource.IdVisitReport);
                    ParticipantVisit.IdVisitReportNavigation = VisitReport;
                    ParticipantVisit.VersionVisitReport = VisitReport.Version;
                    ParticipantVisit.StatusVisitReport = VisitReport.Status;
                    ParticipantVisit.IdVisitReport = VisitReport.IdReport;



                    //*** Creation dans la base de données ***
                    var NewParticipantVisit = await _ParticipantVisitService.Create(ParticipantVisit);
                    var ParticipantVisitResource = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(NewParticipantVisit);
                }
            }
            if (SaveParticipantVisitResource.IdUser.Count > 0)
            {
                foreach (var item in SaveParticipantVisitResource.IdUser)
                {
                    //*** Mappage ***
                    var ParticipantVisit = new ParticipantVisit();
                    ParticipantVisit.UpdatedOn = DateTime.UtcNow;
                    ParticipantVisit.CreatedOn = DateTime.UtcNow;
                    ParticipantVisit.Active = 0;
                    ParticipantVisit.Status = 0;
                    ParticipantVisit.UpdatedBy = 0;
                    ParticipantVisit.CreatedBy = 0;
                    var User = await _UserService.GetById(item);
                    ParticipantVisit.IdDoctor = null;
                    ParticipantVisit.IdDoctorNavigation = null;
                    ParticipantVisit.VersionDoctor = null;
                    ParticipantVisit.StatusDoctor = null;
                    ParticipantVisit.IdPharmacy = null;
                    ParticipantVisit.IdPharmacyNavigation = null;
                    ParticipantVisit.VersionPharmacy = null;
                    ParticipantVisit.StatusPharmacy = null;
                    ParticipantVisit.IdUserNavigation = User;
                    ParticipantVisit.VersionUser = User.Version;
                    ParticipantVisit.StatusUser = User.Status;
                    ParticipantVisit.IdUser = User.IdUser;

                    var VisitReport = await _VisitReportService.GetById(SaveParticipantVisitResource.IdVisitReport);
                    ParticipantVisit.IdVisitReportNavigation = VisitReport;
                    ParticipantVisit.VersionVisitReport = VisitReport.Version;
                    ParticipantVisit.StatusVisitReport = VisitReport.Status;
                    ParticipantVisit.IdVisitReport = VisitReport.IdReport;


                    //*** Creation dans la base de données ***
                    var NewParticipantVisit = await _ParticipantVisitService.Create(ParticipantVisit);
                    var ParticipantVisitResource = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(NewParticipantVisit);
                }
            }
            //*** Mappage ***

            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult<ParticipantVisitResource>> GetAllParticipantVisits()
        {
            try
            {
                var Employe = await _ParticipantVisitService.GetAll();
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
        public async Task<ActionResult<ParticipantVisitResource>> GetAllActifParticipantVisits()
        {
            try
            {
                var Employe = await _ParticipantVisitService.GetAllActif();
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
        public async Task<ActionResult<ParticipantVisitResource>> GetAllInactifParticipantVisits()
        {
            try
            {
                var Employe = await _ParticipantVisitService.GetAllInActif();
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
        public async Task<ActionResult<ParticipantVisitResource>> GetParticipantVisitById(int Id)
        {
            try
            {
                var ParticipantVisits = await _ParticipantVisitService.GetById(Id);
                if (ParticipantVisits == null) return NotFound();
                var ParticipantVisitRessource = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(ParticipantVisits);
                return Ok(ParticipantVisitRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ParticipantVisitResource>> UpdateParticipantVisit(int Id, SaveParticipantVisitResource SaveParticipantVisitResource)
        {

            var ParticipantVisitToBeModified = await _ParticipantVisitService.GetById(Id);
            if (ParticipantVisitToBeModified == null) return BadRequest("Le ParticipantVisit n'existe pas"); //NotFound();
            var ParticipantVisits = _mapperService.Map<SaveParticipantVisitResource, ParticipantVisit>(SaveParticipantVisitResource);
            //var newParticipantVisit = await _ParticipantVisitService.Create(ParticipantVisits);

            await _ParticipantVisitService.Update(ParticipantVisitToBeModified, ParticipantVisits);

            var ParticipantVisitUpdated = await _ParticipantVisitService.GetById(Id);

            var ParticipantVisitResourceUpdated = _mapperService.Map<ParticipantVisit, ParticipantVisitResource>(ParticipantVisitUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteParticipantVisit(int Id)
        {
            try
            {

                var sub = await _ParticipantVisitService.GetById(Id);
                if (sub == null) return BadRequest("Le ParticipantVisit  n'existe pas"); //NotFound();
                await _ParticipantVisitService.Delete(sub);
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
                List<ParticipantVisit> empty = new List<ParticipantVisit>();
                foreach (var item in Ids)
                {
                    var sub = await _ParticipantVisitService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le ParticipantVisit  n'existe pas"); //NotFound();

                }
                await _ParticipantVisitService.DeleteRange(empty);
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
