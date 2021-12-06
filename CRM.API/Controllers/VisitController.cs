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

    public class VisitController : ControllerBase
    {
        private readonly IBrickService _BrickService;
        private readonly ILocalityService _LocalityService;

        private readonly IVisitService _VisitService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IMapper _mapperService;
        public VisitController(IUserService UserService, ILocalityService LocalityService, IBrickService BrickService,
            IVisitService VisitService, IPharmacyService PharmacyService, IDoctorService DoctorService, IMapper mapper)
        {
            _BrickService = BrickService;

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
            
            var Doctor = await _DoctorService.GetById(SaveVisitResource.IdDoctor);
            var Pharmacy = await _PharmacyService.GetById(SaveVisitResource.IdPharmacy);
            var Locality1 = await _LocalityService.GetById(SaveVisitResource.IdLocality1);
            Visit.NameLocality1 = Locality1.Name;
            Visit.VersionLocality1 = Locality1.Version;
            Visit.StatusLocality1 = Locality1.Status;
            Visit.IdLocality1 = Locality1.IdLocality;
            var Locality2 = await _LocalityService.GetById(SaveVisitResource.IdLocality2);
            Visit.NameLocality2 = Locality2.Name;
            Visit.VersionLocality2 = Locality2.Version;
            Visit.StatusLocality2 = Locality2.Status;
            Visit.IdLocality2 = Locality2.IdLocality;
            var Brick1 = await _BrickService.GetByIdActif(SaveVisitResource.IdBrick1);
            var Brick2 = await _BrickService.GetByIdActif(SaveVisitResource.IdBrick2);
            if (Brick1 != null)
            {
                Visit.IdBrick1 = Brick1.IdBrick;
                Visit.VersionBrick1 = Brick1.Version;
                Visit.StatusBrick1 = Brick1.Status;
                Visit.NameBrick1 = Brick1.Name;
                Visit.NumBrick1 = Brick1.NumSystemBrick;
                

            }
            else
            {
                Visit.IdBrick1 = null;
                Visit.VersionBrick1 = null;
                Visit.StatusBrick1 = null;
                Visit.NameBrick1 = "";
                Visit.NumBrick1 = 0;
            }
            if (Brick2 != null)
            {
                Visit.IdBrick2 = Brick2.IdBrick;
                Visit.VersionBrick2 = Brick2.Version;
                Visit.StatusBrick2 = Brick2.Status;
                Visit.NameBrick2 = Brick2.Name;
                Visit.NumBrick2 = Brick2.NumSystemBrick;
                
#pragma warning disable S125 // Sections of code should not be commented out
// Pharmacy.Brick2 = Brick2;
            }
#pragma warning restore S125 // Sections of code should not be commented out
            else
            {
                Visit.IdBrick2 = null;
                Visit.VersionBrick2 = null;
                Visit.StatusBrick2 = null;
                Visit.NameBrick2 = "";
                Visit.NumBrick2 = 0;
            }
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
        [HttpPost("GetAll")]
        public async Task<ActionResult<VisitResource>> GetAllVisits(Lists Lists)
        {
            try
            {
                var Visits = await _VisitService.GetAll(Lists.Status);
                if (Visits == null) return NotFound();
                List<VisitResource> VisitResources = new List<VisitResource>();
                foreach (var item in Visits)
                {
                    var VisitResource = _mapperService.Map<Visit, VisitResource>(item);

                    var Visit = await _VisitService.GetById(item.IdVisit, Lists.Status);
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
        
                    VisitResource = VisitRessource;
                    VisitResources.Add(VisitResource);
                }
                

                return Ok(VisitResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPost("GetById")]
        public async Task<ActionResult<VisitResource>> GetVisitById(ListsGetbyId ListsGetbyId)
        {
            try
            {
                var Visits = await _VisitService.GetById(ListsGetbyId.Id, ListsGetbyId.Status);
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
            if (VisitToBeModified == null) return BadRequest("Le Visit n'existe pas"); 
            var Visit = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitResource);
            

            var Doctor = await _DoctorService.GetById(SaveVisitResource.IdDoctor);
            var Pharmacy = await _PharmacyService.GetById(SaveVisitResource.IdPharmacy);
            Visit.UpdatedOn = DateTime.UtcNow;
            Visit.CreatedOn = VisitToBeModified.CreatedOn;
            Visit.Active = 0;
            Visit.Status = 0;
            Visit.UpdatedBy = 0;
            Visit.CreatedBy = VisitToBeModified.CreatedBy;
            if (Pharmacy.Id != VisitToBeModified.IdPharmacy)
            {
                Visit.Name = Pharmacy.Name;
                Visit.Pharmacy = Pharmacy;
                Visit.VersionPharmacy = Pharmacy.Version;
                Visit.StatusPharmacy = Pharmacy.Status;
                Visit.Doctor = null;
                Visit.VersionDoctor = null;
                Visit.StatusDoctor = null;
            }
        
            if (Doctor.IdDoctor != VisitToBeModified.IdDoctor)
            {
                Visit.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                Visit.Doctor = Doctor;
                Visit.VersionDoctor = Doctor.Version;
                Visit.StatusDoctor = Doctor.Status;

                Visit.Pharmacy = null;
                Visit.VersionPharmacy = null;
                Visit.StatusPharmacy = null;
            }

            await _VisitService.Update(VisitToBeModified, Visit);

            var VisitUpdated = await _VisitService.GetById(Id);

            var VisitResourceUpdated = _mapperService.Map<Visit, VisitResource>(VisitUpdated);

            return Ok(VisitResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVisit(int Id)
        {
            try
            {

                var sub = await _VisitService.GetById(Id);
                if (sub == null) return BadRequest("Le Visit  n'existe pas"); 
                await _VisitService.Delete(sub);
                
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
                    if (sub == null) return BadRequest("Le Visit  n'existe pas"); 

                }
                await _VisitService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
