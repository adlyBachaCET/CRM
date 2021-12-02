using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
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
    public class VisitReportController : ControllerBase
    {
        public IList<VisitReport> VisitReports;

        private readonly IVisitReportService _VisitReportService;
        private readonly IVisitService _VisitService;
        private readonly IUserService _UserService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IMapper _mapperService;
        public VisitReportController(IPharmacyService PharmacyService, IDoctorService DoctorService,IUserService UserService, IVisitReportService VisitReportService, IVisitService VisitService, IMapper mapper)
        {
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
               _UserService = UserService;
            _VisitService = VisitService;
            _VisitReportService = VisitReportService;
            _mapperService = mapper;
        }
        [HttpPost("GetAll")]
        public async Task<ActionResult<VisitReportResource>> GetAllVisitReports(Lists Lists)
        {
            try
            {
                var VisitReports = await _VisitReportService.GetAll(Lists.Status);
                if (VisitReports == null) return NotFound();
                List<VisitReportResource> VisitReportResources = new List<VisitReportResource>();
                foreach(var item in VisitReports) {
                var VisitReportResource = _mapperService.Map<VisitReport, VisitReportResource>(item);

               var Visit = await _VisitService.GetById(item.IdVisit, Lists.Status);
                var VisitRessource = _mapperService.Map<Visit, VisitResource>(Visit);
                    if(Visit.IdDoctor!=null)
                    {
                        var Doctor = await _DoctorService.GetById(Visit.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);
                        VisitRessource.StatusDoctor = Doctor.Status;
                        VisitRessource.VersionDoctor = Doctor.Version;
                        VisitRessource.Doctor = DoctorResource;
                    }
                    if (Visit.IdPharmacy != null)
                    {
                        var Pharmacy = await _PharmacyService.GetById(Visit.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);
                        VisitRessource.StatusPharmacy = Pharmacy.Status;
                        VisitRessource.VersionPharmacy = Pharmacy.Version;
                        VisitRessource.Pharmacy = PharmacyResource;
                    }
                    VisitReportResource.Visit = VisitRessource;
                    VisitReportResources.Add(VisitReportResource);
                }
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(VisitReportResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<VisitReportResource>> GetVisitReportById(ListsGetbyId ListsGetbyId)
        {
            try
            {
                var VisitReports = await _VisitReportService.GetById(ListsGetbyId.Id, ListsGetbyId.Status);
                if (VisitReports == null) return NotFound();
                var VisitReportRessource = _mapperService.Map<VisitReport, VisitReportResource>(VisitReports);
                var Visit = await _VisitService.GetById(VisitReportRessource.IdVisit, ListsGetbyId.Status);
                var VisitRessource = _mapperService.Map<Visit, VisitResource>(Visit);
                if (Visit.IdDoctor != null)
                {
                    var Doctor = await _DoctorService.GetById(Visit.IdDoctor);
                    var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);
                    VisitRessource.StatusDoctor = Doctor.Status;
                    VisitRessource.VersionDoctor = Doctor.Version;
                    VisitRessource.Doctor = DoctorResource;
                }
                if (Visit.IdPharmacy != null)
                {
                    var Pharmacy = await _PharmacyService.GetById(Visit.IdPharmacy);
                    var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);
                    VisitRessource.StatusPharmacy = Pharmacy.Status;
                    VisitRessource.VersionPharmacy = Pharmacy.Version;
                    VisitRessource.Pharmacy = PharmacyResource;
                }

                VisitReportRessource.Visit = VisitRessource;
                return Ok(VisitReportRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VisitReportResource>> CreateVisitReport(SaveVisitReportResource SaveVisitReportResource)
        {
            //*** Mappage ***
            var VisitReport = _mapperService.Map<SaveVisitReportResource, VisitReport>(SaveVisitReportResource);
            var Visit = await _VisitService.GetById(SaveVisitReportResource.IdVisit);
            VisitReport.IdVisit = Visit.IdVisit;
            VisitReport.StatusVisit = Visit.Status;
            VisitReport.VersionVisit = Visit.Version;
            VisitReport.Visit = Visit;
            VisitReport.UpdatedOn = DateTime.UtcNow;
            VisitReport.CreatedOn = DateTime.UtcNow;
            VisitReport.UpdatedBy = Visit.UpdatedBy;
            VisitReport.CreatedBy = Visit.CreatedBy;
            //*** Creation dans la base de données ***
            var NewVisitReport = await _VisitReportService.Create(VisitReport);
            //*** Mappage ***
            var VisitReportResource = _mapperService.Map<VisitReport, VisitReportResource>(NewVisitReport);
            return Ok(VisitReportResource);
        }
        [HttpPost("Range")]
        public async Task<ActionResult<VisitReportResource>> CreateVisitReport(List<SaveVisitReportResource> SaveVisitReportResource)
        {
            //*** Mappage ***
            var VisitReport = _mapperService.Map<List<SaveVisitReportResource>, VisitReport>(SaveVisitReportResource);
            //*** Creation dans la base de données ***
            var NewVisitReport = await _VisitReportService.Create(VisitReport);
            //*** Mappage ***
            var VisitReportResource = _mapperService.Map<VisitReport, VisitReportResource>(NewVisitReport);
            return Ok(VisitReportResource);
        }

        [HttpGet("Actif")]
        public async Task<ActionResult<VisitReportResource>> GetAllActifVisitReports()
        {
            try
            {
                var Employe = await _VisitReportService.GetAllActif();
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
        public async Task<ActionResult<VisitReportResource>> GetAllInactifVisitReports()
        {
            try
            {
                var Employe = await _VisitReportService.GetAllInActif();
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
        public async Task<ActionResult<VisitReportResource>> GetVisitReportById(int Id)
        {
            try
            {
                var VisitReports = await _VisitReportService.GetById(Id);
                if (VisitReports == null) return NotFound();
                var VisitReportRessource = _mapperService.Map<VisitReport, VisitReportResource>(VisitReports);
                return Ok(VisitReportRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<VisitReportResource>> UpdateVisitReport(int Id, SaveVisitReportResource SaveVisitReportResource)
        {

            var VisitReportToBeModified = await _VisitReportService.GetById(Id);
            if (VisitReportToBeModified == null) return BadRequest("Le VisitReport n'existe pas"); //NotFound();
            var VisitReport = _mapperService.Map<SaveVisitReportResource, VisitReport>(SaveVisitReportResource);
            //var newVisitReport = await _VisitReportService.Create(VisitReports);
            var Visit = await _VisitService.GetById(SaveVisitReportResource.IdVisit);
            VisitReport.IdVisit = Visit.IdVisit;
            VisitReport.StatusVisit = Visit.Status;
            VisitReport.VersionVisit = Visit.Version;
            VisitReport.Visit = Visit;
            VisitReport.UpdatedOn = DateTime.UtcNow;
            VisitReport.CreatedOn = VisitReportToBeModified.CreatedOn;
            VisitReport.UpdatedBy = Visit.UpdatedBy;
            VisitReport.CreatedBy = Visit.CreatedBy;
            await _VisitReportService.Update(VisitReportToBeModified, VisitReport);

            var VisitReportUpdated = await _VisitReportService.GetById(Id);

            var VisitReportResourceUpdated = _mapperService.Map<VisitReport, VisitReportResource>(VisitReportUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVisitReport(int Id)
        {
            try
            {

                var sub = await _VisitReportService.GetById(Id);
                if (sub == null) return BadRequest("Le VisitReport  n'existe pas"); //NotFound();
                await _VisitReportService.Delete(sub);
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
                List<VisitReport> empty = new List<VisitReport>();
                foreach (var item in Ids)
                {
                    var sub = await _VisitReportService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le VisitReport  n'existe pas"); //NotFound();

                }
                await _VisitReportService.DeleteRange(empty);
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
