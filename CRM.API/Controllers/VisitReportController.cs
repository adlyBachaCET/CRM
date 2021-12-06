using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class VisitReportController : ControllerBase
    {
        public IList<VisitReport> VisitReports;
        private readonly IObjectionService _ObjectionService;

        private readonly IVisitReportService _VisitReportService;
        private readonly IVisitService _VisitService;
        private readonly IUserService _UserService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;
        private readonly IBrickService _BrickService;
        private readonly ILocalityService _LocalityService;
        private readonly IProductService _ProductService;
        private readonly IParticipantService _ParticipantService;
        private readonly IProductVisitReportService _ProductVisitReporService;
        private readonly IExternalsService _ExternalsService;

        private readonly IMapper _mapperService;
        public VisitReportController(IParticipantService ParticipantService,
            IObjectionService ObjectionService,
            ILocalityService LocalityService, 
            IBrickService BrickService, 
            IPharmacyService PharmacyService,
            IDoctorService DoctorService,
            IUserService UserService,
            IProductService ProductService,
            IProductVisitReportService ProductVisitReportService,
                        IExternalsService ExternalsService,

            IVisitReportService VisitReportService,
            IVisitService VisitService,
            IMapper mapper)
        {
            _ExternalsService = ExternalsService;

            _ParticipantService = ParticipantService;
            _BrickService = BrickService;
            _ProductVisitReporService = ProductVisitReportService;
            _LocalityService = LocalityService;
            _PharmacyService = PharmacyService;
            _ProductService = ProductService;

            _ObjectionService = ObjectionService;
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
        public async Task<ActionResult<VisitReportResource>> CreateVisitReport([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token,SaveVisitReportResource SaveVisitReportResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var FirstName = claims.FindFirst("FirstName").Value;
            var LastName = claims.FindFirst("LastName").Value;
            
            var Visit = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitReportResource.Visit);

            var Doctor = await _DoctorService.GetById(SaveVisitReportResource.Visit.IdDoctor);
            var Pharmacy = await _PharmacyService.GetById(SaveVisitReportResource.Visit.IdPharmacy);
            var Locality1 = await _LocalityService.GetById(SaveVisitReportResource.Visit.IdLocality1);

            Visit.NameLocality1 = Locality1.Name;
            Visit.VersionLocality1 = Locality1.Version;
            Visit.StatusLocality1 = Locality1.Status;
            Visit.IdLocality1 = Locality1.IdLocality;
            var Locality2 = await _LocalityService.GetById(SaveVisitReportResource.Visit.IdLocality2);
            Visit.NameLocality2 = Locality2.Name;
            Visit.VersionLocality2 = Locality2.Version;
            Visit.StatusLocality2 = Locality2.Status;
            Visit.IdLocality2 = Locality2.IdLocality;
            var Brick1 = await _BrickService.GetByIdActif(SaveVisitReportResource.Visit.IdBrick1);
            var Brick2 = await _BrickService.GetByIdActif(SaveVisitReportResource.Visit.IdBrick2);
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
            }
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
            //*** Mappage ***
            var VisitReport = _mapperService.Map<SaveVisitReportResource, VisitReport>(SaveVisitReportResource);
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
            foreach (var item in SaveVisitReportResource.Objections)
            {
                var Objection = _mapperService.Map<SaveObjectionResource, Objection>(item);
                Objection.UpdatedOn = DateTime.UtcNow;
                Objection.CreatedOn = DateTime.UtcNow;
                Objection.RequestObjection = RequestObjection.Objection;
                Objection.Active = 0;
                Objection.Status = 0;
                Objection.CreatedBy = Id;
                Objection.UpdatedBy = Id;
                Objection.CreatedByName = FirstName + " " + LastName;
                Objection.UpdatedByName = FirstName + " " + LastName;
                if (NewVisitReport.IdReport != 0)
                {
                    var VisitReportOld = await _VisitReportService.GetById(NewVisitReport.IdReport);

                    if (VisitReportOld != null)
                    {
                        Objection.VisitReport = VisitReportOld;
                        Objection.VersionVisitReport = VisitReportOld.Version;
                        Objection.StatusVisitReport = VisitReportOld.Status;

                    }
                    else
                    {
                        Objection.VisitReport = null;
                        Objection.VersionVisitReport = null;
                        Objection.StatusVisitReport = null;
                    }
                }
                if (item.IdPharmacy != 0)
                {
                    var PharmacyOld = await _PharmacyService.GetById(item.IdPharmacy);

                    if (PharmacyOld != null)
                    {
                        Objection.Name = PharmacyOld.Name;
                        Objection.Pharmacy = PharmacyOld;
                        Objection.VersionPharmacy = PharmacyOld.Version;
                        Objection.StatusPharmacy = PharmacyOld.Status;
                        Objection.Doctor = null;
                        Objection.VersionDoctor = null;
                        Objection.StatusDoctor = null;
                    }
                }
                if (item.IdDoctor != 0)
                {

                    var DoctorOld = await _DoctorService.GetById(item.IdDoctor);

                    if (DoctorOld != null)
                    {
                        Objection.Name = DoctorOld.Title + " " + DoctorOld.FirstName + " " + DoctorOld.LastName;
                        Objection.Doctor = DoctorOld;
                        Objection.VersionDoctor = DoctorOld.Version;
                        Objection.StatusDoctor = DoctorOld.Status;

                        Objection.Pharmacy = null;
                        Objection.VersionPharmacy = null;
                        Objection.StatusPharmacy = null;
                    }
                }
                if (item.SatisfiedNotSatisfied != null)
                {
                    Objection.Status = item.SatisfiedNotSatisfied.Value;
                }
                else
                {
                    if (Role == "Manager")
                    {
                        Objection.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Objection.Status = Status.Pending;
                    }
                }
                var User = await _UserService.GetById(item.IdUser);
                Objection.User = User;
                Objection.VersionUser = User.Version;
                Objection.StatusUser = User.Status;

                var Product = await _ProductService.GetById(item.IdProduct);
                Objection.Product = Product;
                Objection.VersionProduct = Product.Version;
                Objection.StatusProduct = Product.Status;
                //*** Creation dans la base de données ***
                var NewObjection = await _ObjectionService.Create(Objection);
                //*** Mappage ***
            }
            foreach (var item in SaveVisitReportResource.Requests)
            {
                var Objection = _mapperService.Map<SaveObjectionResource, Objection>(item);
                Objection.UpdatedOn = DateTime.UtcNow;
                Objection.CreatedOn = DateTime.UtcNow;
                Objection.RequestObjection = RequestObjection.Request;
                Objection.Active = 0;
                Objection.Status = 0;
                Objection.CreatedBy = Id;
                Objection.UpdatedBy = Id;
                Objection.CreatedByName = FirstName + " " + LastName;
                Objection.UpdatedByName = FirstName + " " + LastName;
                if (NewVisitReport.IdReport != 0)
                {
                    var VisitReportOld = await _VisitReportService.GetById(NewVisitReport.IdReport);

                    if (VisitReportOld != null)
                    {
                        Objection.VisitReport = VisitReportOld;
                        Objection.VersionVisitReport = VisitReportOld.Version;
                        Objection.StatusVisitReport = VisitReportOld.Status;

                    }
                    else
                    {
                        Objection.VisitReport = null;
                        Objection.VersionVisitReport = null;
                        Objection.StatusVisitReport = null;
                    }
                }
                if (item.IdPharmacy != 0)
                {
                    var PharmacyOld = await _PharmacyService.GetById(item.IdPharmacy);

                    if (PharmacyOld != null)
                    {
                        Objection.Name = PharmacyOld.Name;
                        Objection.Pharmacy = PharmacyOld;
                        Objection.VersionPharmacy = PharmacyOld.Version;
                        Objection.StatusPharmacy = PharmacyOld.Status;
                        Objection.Doctor = null;
                        Objection.VersionDoctor = null;
                        Objection.StatusDoctor = null;
                    }
                }
                if (item.IdDoctor != 0)
                {

                    var DoctorOld = await _DoctorService.GetById(item.IdDoctor);

                    if (DoctorOld != null)
                    {
                        Objection.Name = DoctorOld.Title + " " + DoctorOld.FirstName + " " + DoctorOld.LastName;
                        Objection.Doctor = DoctorOld;
                        Objection.VersionDoctor = DoctorOld.Version;
                        Objection.StatusDoctor = DoctorOld.Status;

                        Objection.Pharmacy = null;
                        Objection.VersionPharmacy = null;
                        Objection.StatusPharmacy = null;
                    }
                }
                if (item.SatisfiedNotSatisfied != null)
                {
                    Objection.Status = item.SatisfiedNotSatisfied.Value;
                }
                else
                {
                    if (Role == "Manager")
                    {
                        Objection.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Objection.Status = Status.Pending;
                    }
                }
                var User = await _UserService.GetById(item.IdUser);
                Objection.User = User;
                Objection.VersionUser = User.Version;
                Objection.StatusUser = User.Status;

                var Product = await _ProductService.GetById(item.IdProduct);
                Objection.Product = Product;
                Objection.VersionProduct = Product.Version;
                Objection.StatusProduct = Product.Status;
                //*** Creation dans la base de données ***
                var NewObjection = await _ObjectionService.Create(Objection);
                //*** Mappage ***
            }


            foreach(var item in SaveVisitReportResource.ParticipantPharmacys)
            {
                var Participant = new Participant();
                Participant.UpdatedOn = DateTime.UtcNow;
                Participant.CreatedOn = DateTime.UtcNow;
                Participant.Context = StatusParticipant.Visit;
                Participant.Active = 0;
                Participant.Status = 0;
                Participant.CreatedBy = Id;
                Participant.UpdatedBy = Id;
                var PharmacyOld = await _PharmacyService.GetById(item);
                Participant.IdDoctor = PharmacyOld.Id;
                Participant.VersionPharmacy = PharmacyOld.Version;
                Participant.StatusPharmacy = PharmacyOld.Status;
                Participant.IdPharmacyNavigation = PharmacyOld;
              await _ParticipantService.Create(Participant);

            }
            foreach (var item in SaveVisitReportResource.ParticipantDoctors)
            {
                var Participant = new Participant();
                Participant.UpdatedOn = DateTime.UtcNow;
                Participant.CreatedOn = DateTime.UtcNow;
                Participant.Context = StatusParticipant.Visit;
                Participant.Active = 0;
                Participant.Status = 0;
                Participant.CreatedBy = Id;
                Participant.UpdatedBy = Id;
                var DoctorOld = await _DoctorService.GetById(item);
                Participant.IdDoctor = DoctorOld.IdDoctor;
                Participant.VersionDoctor = DoctorOld.Version;
                Participant.StatusDoctor = DoctorOld.Status;
                Participant.IdDoctorNavigation = DoctorOld;

                await _ParticipantService.Create(Participant);

            }
            foreach (var item in SaveVisitReportResource.ListOfProducts)
            {
                var ProductVisitReport = new ProductVisitReport();
                ProductVisitReport.UpdatedOn = DateTime.UtcNow;
                ProductVisitReport.CreatedOn = DateTime.UtcNow;
                ProductVisitReport.Active = 0;
                ProductVisitReport.Status = 0;
                ProductVisitReport.CreatedBy = Id;
                ProductVisitReport.UpdatedBy = Id;
                var Product = await _ProductService.GetById(item);
                ProductVisitReport.IdProduct = Product.IdProduct;
                ProductVisitReport.VersionProduct = Product.Version;
                ProductVisitReport.StatusProduct = Product.Status;
                ProductVisitReport.Product = Product;

                ProductVisitReport.VersionProduct = Product.Version;
                ProductVisitReport.StatusProduct = Product.Status;

                ProductVisitReport.Report = NewVisitReport;
                ProductVisitReport.IdReport = NewVisitReport.IdReport;
                ProductVisitReport.StatusReport = NewVisitReport.Status;
                ProductVisitReport.VersionReport = NewVisitReport.Version;
                await _ProductVisitReporService.Create(ProductVisitReport);

            }
            foreach (var item in SaveVisitReportResource.Externals)
            {
                var Externals = new Externals();
                Externals.UpdatedOn = DateTime.UtcNow;
                Externals.CreatedOn = DateTime.UtcNow;
                Externals.Active = 0;
                Externals.Status = 0;
                Externals.CreatedBy = Id;
                Externals.UpdatedBy = Id;

                Externals.FullName = item.FullName;
                Externals.Email = item.Email;

                Externals.Context = StatusParticipant.Visit;
                Externals.IdVisitReportNavigation = NewVisitReport;
                Externals.IdVisitReport = NewVisitReport.IdReport;
                Externals.StatusVisitReport = NewVisitReport.Status;
                Externals.VersionVisitReport = NewVisitReport.Version;
                Externals.IdRequestRpNavigation = null;
                Externals.IdRequestRp = null;
                Externals.StatusRequestRp = null;
                Externals.VersionRequestRp = null;
                await _ExternalsService.Create(Externals);

            }
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
