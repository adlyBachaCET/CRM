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

    public class ObjectionController : ControllerBase
    {
        public IList<Objection> Objections;

        private readonly IObjectionService _ObjectionService;
        private readonly IDoctorService _DoctorService; 
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;
        private readonly IProductService _ProductService;
        private readonly IBusinessUnitService _BusinessUnitService;
        private readonly IVisitReportService _VisitReportService;


        private readonly IMapper _mapperService;
        public ObjectionController(IVisitReportService VisitReportService, 
            IBusinessUnitService BusinessUnitService, IProductService ProductService, 
            IUserService UserService, IPharmacyService PharmacyService, IDoctorService DoctorService,
            IObjectionService ObjectionService, IMapper mapper)
        {
            _BusinessUnitService = BusinessUnitService;

            _UserService = UserService;
            _PharmacyService = PharmacyService;
            _DoctorService = DoctorService;
            _VisitReportService = VisitReportService;
            _ProductService = ProductService;

            _ObjectionService = ObjectionService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ObjectionResource>> CreateObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, SaveObjectionResource SaveObjectionResource)
  {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var FirstName = claims.FindFirst("FirstName").Value;
            var LastName = claims.FindFirst("LastName").Value;
            //*** Mappage ***
            var Objection = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            Objection.UpdatedOn = DateTime.UtcNow;
            Objection.CreatedOn = DateTime.UtcNow;
            Objection.Active = 0;
            Objection.Status = 0;
            Objection.CreatedBy = Id;
            Objection.UpdatedBy = Id;
            Objection.CreatedByName = FirstName+ " "+ LastName;
            Objection.UpdatedByName = FirstName + " " + LastName;
            if (SaveObjectionResource.IdVisitReport != 0)
            {
                var VisitReport = await _VisitReportService.GetById(SaveObjectionResource.IdVisitReport);

                if (VisitReport != null)
                {
                    Objection.VisitReport = VisitReport;
                    Objection.VersionVisitReport = VisitReport.Version;
                    Objection.StatusVisitReport = VisitReport.Status;
                    Objection.IdVisitReport = VisitReport.IdReport;

                }
                else
                {
                    Objection.VisitReport = null;
                    Objection.VersionVisitReport = null;
                    Objection.StatusVisitReport = null;
                    Objection.IdVisitReport = null;

                }
            }
            if (SaveObjectionResource.IdPharmacy != 0) { 
            var Pharmacy = await _PharmacyService.GetById(SaveObjectionResource.IdPharmacy);

            if (Pharmacy != null)
            {
                Objection.Name = Pharmacy.Name;
                Objection.Pharmacy = Pharmacy;
                Objection.VersionPharmacy = Pharmacy.Version;
                Objection.StatusPharmacy = Pharmacy.Status;
                Objection.Doctor = null;
                Objection.VersionDoctor = null;
                Objection.StatusDoctor = null;
            }
            }
            if (SaveObjectionResource.IdDoctor != 0)
            {

                var Doctor = await _DoctorService.GetById(SaveObjectionResource.IdDoctor);

                if (Doctor != null)
                {
                    Objection.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                    Objection.Doctor = Doctor;
                    Objection.VersionDoctor = Doctor.Version;
                    Objection.StatusDoctor = Doctor.Status;

                    Objection.Pharmacy = null;
                    Objection.VersionPharmacy = null;
                    Objection.StatusPharmacy = null;
                }
            }
            if (SaveObjectionResource.SatisfiedNotSatisfied != null)
            {
                Objection.Status = SaveObjectionResource.SatisfiedNotSatisfied.Value;
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
            var User = await _UserService.GetById(SaveObjectionResource.IdUser);
            Objection.User = User;
            Objection.VersionUser = User.Version;
            Objection.StatusUser = User.Status;
            if (SaveObjectionResource.IdProduct != 0)
            {
                var Product = await _ProductService.GetById(SaveObjectionResource.IdProduct);
                Objection.Product = Product;
                Objection.VersionProduct = Product.Version;
                Objection.StatusProduct = Product.Status;
            }
            //*** Creation dans la base de données ***
            var NewObjection = await _ObjectionService.Create(Objection);
            //*** Mappage ***
            var ObjectionResource = _mapperService.Map<Objection, ObjectionResource>(NewObjection);
            if (ObjectionResource.IdDoctor != 0)
            {
                var OldDoctor = await _DoctorService.GetById(ObjectionResource.IdDoctor);
                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(OldDoctor);

                ObjectionResource.Doctor = DoctorResource;

            }
            if (ObjectionResource.IdProduct != 0)
            {
                var OldProduct = await _ProductService.GetById(ObjectionResource.IdProduct);
                var ProductResource = _mapperService.Map<Product, ProductResource>(OldProduct);

                var Bu = await _BusinessUnitService.GetById((int)OldProduct.IdBu);
                var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                ProductResource.Bu = BuResource;
                ObjectionResource.Product = ProductResource;

            }
            if (ObjectionResource.IdPharmacy != 0)
            {
                var OldPharmacy = await _PharmacyService.GetById(ObjectionResource.IdPharmacy);
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(OldPharmacy);

                ObjectionResource.Pharmacy = PharmacyResource;

            }
            if (ObjectionResource.IdUser != 0)
            {
                var OldUser = await _UserService.GetById(ObjectionResource.IdUser);
                var UserResource = _mapperService.Map<User, UserResource>(OldUser);

                ObjectionResource.User = UserResource;

            }
            return Ok(ObjectionResource);
      
        }
        [HttpPost("GetAll")]
        public async Task<ActionResult<List<ObjectionResource>>> GetAll([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, RequestObjectionStatus RequestObjectionStatus)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
          

                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Objections = await _ObjectionService.GetAll(RequestObjectionStatus.Status, RequestObjectionStatus.RequestObjection);
                if (Objections == null) return NotFound();
                List<ObjectionResource> ObjectionResources = new List<ObjectionResource>();
                foreach (var item in Objections)
                {
                    var ObjectionResource = _mapperService.Map<Objection, ObjectionResource>(item);
                    if (ObjectionResource.IdDoctor != 0) {
                    var Doctor = await _DoctorService.GetById(ObjectionResource.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                        ObjectionResource.Doctor = DoctorResource;

                    }
                    if (ObjectionResource.IdProduct != 0)
                    {
                        var Product = await _ProductService.GetById(ObjectionResource.IdProduct);
                        var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                        if (Product != null)
                        {
                            var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                            var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                            ProductResource.Bu = BuResource;
                        }
                        ObjectionResource.Product = ProductResource;

                    }
                    if (ObjectionResource.IdPharmacy != 0)
                    {
                        var Pharmacy = await _PharmacyService.GetById(ObjectionResource.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                        ObjectionResource.Pharmacy = PharmacyResource;

                    }
                    if (ObjectionResource.IdUser != 0)
                    {
                        var User = await _UserService.GetById(ObjectionResource.IdUser);
                        var UserResource = _mapperService.Map<User, UserResource>(User);

                        ObjectionResource.User = UserResource;

                    }
           
                    ObjectionResources.Add(ObjectionResource);
                }
                return Ok(ObjectionResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<List<ObjectionResource>>> GetById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, RequestObjectionStatusById RequestObjectionStatus)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;


                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Objections = await _ObjectionService.GetById(RequestObjectionStatus.Id,RequestObjectionStatus.Status, RequestObjectionStatus.RequestObjection);
                if (Objections == null) return NotFound();
             
                    var ObjectionResource = _mapperService.Map<Objection, ObjectionResource>(Objections);
                    if (ObjectionResource.IdDoctor != 0)
                    {
                        var Doctor = await _DoctorService.GetById(ObjectionResource.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);
                        ObjectionResource.Doctor = DoctorResource;

                    }
                    if (ObjectionResource.IdProduct != 0)
                    {
                        var Product = await _ProductService.GetById(ObjectionResource.IdProduct);
                        var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                        if (Product != null)
                        {
                            var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                            var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                            ProductResource.Bu = BuResource;
                        }
                        ObjectionResource.Product = ProductResource;

                    }
                    if (ObjectionResource.IdPharmacy != 0)
                    {
                        var Pharmacy = await _PharmacyService.GetById(ObjectionResource.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                        ObjectionResource.Pharmacy = PharmacyResource;

                    }
                    if (ObjectionResource.IdUser != 0)
                    {
                        var User = await _UserService.GetById(ObjectionResource.IdUser);
                        var UserResource = _mapperService.Map<User, UserResource>(User);

                        ObjectionResource.User = UserResource;

                    }

                
                return Ok(ObjectionResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      [HttpPut("{Id}")]
        public async Task<ActionResult<ObjectionResource>> UpdateObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, SaveObjectionResource SaveObjectionResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var FirstName = claims.FindFirst("FirstName").Value;
            var LastName = claims.FindFirst("LastName").Value;
            var ObjectionToBeModified = await _ObjectionService.GetById(Id);
            if (ObjectionToBeModified == null) return BadRequest("Le Objection n'existe pas"); //NotFound();
            var Objection = _mapperService.Map<SaveObjectionResource, Objection>(SaveObjectionResource);
            //var newObjection = await _ObjectionService.Create(Objections);
            Objection.UpdatedOn = DateTime.UtcNow;
            Objection.CreatedOn = ObjectionToBeModified.CreatedOn;
            Objection.Active = 0;
            Objection.Status = 0;
            Objection.UpdatedBy = IdUser;
            Objection.CreatedBy = ObjectionToBeModified.CreatedBy;
            Objection.UpdatedByName = FirstName+" "+LastName;
            Objection.CreatedByName = ObjectionToBeModified.CreatedByName;
            if (SaveObjectionResource.SatisfiedNotSatisfied != null)
            {
                Objection.Status = SaveObjectionResource.SatisfiedNotSatisfied.Value;
            }
            else { 
            if (Role == "Manager")
            {
                Objection.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Objection.Status = Status.Pending;
            }
            }
            if (SaveObjectionResource.IdVisitReport != 0)
            {
                var VisitReport = await _VisitReportService.GetById(SaveObjectionResource.IdVisitReport);

                if (VisitReport != null)
                {
                    Objection.VisitReport = VisitReport;
                    Objection.VersionVisitReport = VisitReport.Version;
                    Objection.StatusVisitReport = VisitReport.Status;
                    Objection.IdVisitReport = VisitReport.IdReport;

                }
                else
                {
                    Objection.IdVisitReport = null;

                    Objection.VisitReport = null;
                    Objection.VersionVisitReport = null;
                    Objection.StatusVisitReport = null;
                }
            }
            if (Objection.IdPharmacy != 0)
            {
                var Pharmacy = await _PharmacyService.GetById(SaveObjectionResource.IdPharmacy);

                if (Pharmacy.Id != Objection.IdPharmacy)
                {
                    Objection.Name = Pharmacy.Name;
                    Objection.Pharmacy = Pharmacy;
                    Objection.VersionPharmacy = Pharmacy.Version;
                    Objection.StatusPharmacy = Pharmacy.Status;
                    Objection.Doctor = null;
                    Objection.VersionDoctor = null;
                    Objection.StatusDoctor = null;
                }
                else
                {
                    Objection.Name = Objection.Name;
                    Objection.Pharmacy = Objection.Pharmacy;
                    Objection.VersionPharmacy = Objection.VersionPharmacy;
                    Objection.StatusPharmacy = Objection.StatusPharmacy;
                    Objection.Doctor = null;
                    Objection.VersionDoctor = null;
                    Objection.StatusDoctor = null;
                }
            }
            if (Objection.IdDoctor != 0)
            {
                var Doctor = await _DoctorService.GetById(SaveObjectionResource.IdDoctor);

                if (Doctor.IdDoctor != Objection.IdDoctor)
                {
                    Objection.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                    Objection.Doctor = Doctor;
                    Objection.VersionDoctor = Doctor.Version;
                    Objection.StatusDoctor = Doctor.Status;

                    Objection.Pharmacy = null;
                    Objection.VersionPharmacy = null;
                    Objection.StatusPharmacy = null;
                }
                else
                {
                    {
                        Objection.Name = Objection.Name;
                        Objection.Doctor = Objection.Doctor;
                        Objection.VersionDoctor = Objection.VersionDoctor;
                        Objection.StatusDoctor = Objection.StatusDoctor;

                        Objection.Pharmacy = null;
                        Objection.VersionPharmacy = null;
                        Objection.StatusPharmacy = null;
                    }
                }
            }
            await _ObjectionService.Update(ObjectionToBeModified, Objection);

            var ObjectionUpdated = await _ObjectionService.GetById(Id);

            var ObjectionResourceUpdated = _mapperService.Map<Objection, ObjectionResource>(ObjectionUpdated);
            if (ObjectionResourceUpdated.IdDoctor != 0)
            {
                var DoctorNew = await _DoctorService.GetById(ObjectionResourceUpdated.IdDoctor);
                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(DoctorNew);

                ObjectionResourceUpdated.Doctor = DoctorResource;

            }
            if (ObjectionResourceUpdated.IdProduct != 0)
            {
                var Product = await _ProductService.GetById(ObjectionResourceUpdated.IdProduct);
                var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                ProductResource.Bu = BuResource;
                ObjectionResourceUpdated.Product = ProductResource;

            }
            if (ObjectionResourceUpdated.IdPharmacy != 0)
            {
                var PharmacyNew = await _PharmacyService.GetById(ObjectionResourceUpdated.IdPharmacy);
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyNew);

                ObjectionResourceUpdated.Pharmacy = PharmacyResource;

            }
            if (ObjectionResourceUpdated.IdUser != 0)
            {
                var User = await _UserService.GetById(ObjectionResourceUpdated.IdUser);
                var UserResource = _mapperService.Map<User, UserResource>(User);

                ObjectionResourceUpdated.User = UserResource;

            }
            return Ok(ObjectionResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteObjection([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var ObjectionToBeModified = await _ObjectionService.GetById(Id);
                var sub = await _ObjectionService.GetById(Id);
                if (sub == null) return BadRequest("Le Objection  n'existe pas"); //NotFound();
                await _ObjectionService.Delete(sub);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, List<int> Ids)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                List<Objection> empty = new List<Objection>();
                foreach (var item in Ids)
                {
                    var sub = await _ObjectionService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Objection  n'existe pas"); //NotFound();

                }
                await _ObjectionService.DeleteRange(empty);
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
