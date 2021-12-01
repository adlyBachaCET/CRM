using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
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

    public class RequestDoctorController : ControllerBase
    {
        public IList<RequestDoctor> RequestDoctors;

        private readonly IRequestDoctorService _RequestDoctorService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;
        private readonly IProductService _ProductService;
        private readonly IBusinessUnitService _BusinessUnitService;


        private readonly IMapper _mapperService;
        public RequestDoctorController(IBusinessUnitService BusinessUnitService, IProductService ProductService, IUserService UserService, IPharmacyService PharmacyService, IDoctorService DoctorService, IRequestDoctorService RequestDoctorService, IMapper mapper)
        {
            _ProductService = ProductService;
            _BusinessUnitService = BusinessUnitService;

            _UserService = UserService;
            _PharmacyService = PharmacyService;
            _DoctorService = DoctorService;

            _RequestDoctorService = RequestDoctorService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<RequestDoctorResource>> CreateRequestDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, SaveRequestDoctorResource SaveRequestDoctorResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var FirstName = claims.FindFirst("FirstName").Value;
            var LastName = claims.FindFirst("LastName").Value;
            //*** Mappage ***
            var RequestDoctor = _mapperService.Map<SaveRequestDoctorResource, RequestDoctor>(SaveRequestDoctorResource);
            RequestDoctor.UpdatedOn = DateTime.UtcNow;
            RequestDoctor.CreatedOn = DateTime.UtcNow;
            RequestDoctor.Active = 0;
            RequestDoctor.Status = 0;
            RequestDoctor.CreatedBy = Id;
            RequestDoctor.UpdatedBy = Id;
            RequestDoctor.CreatedByName = FirstName + " " + LastName;
            RequestDoctor.UpdatedByName = FirstName + " " + LastName;
            var Doctor = await _DoctorService.GetById(SaveRequestDoctorResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveRequestDoctorResource.IdPharmacy);

            if (Pharmacy != null)
            {
                RequestDoctor.Name = Pharmacy.Name;
                RequestDoctor.Pharmacy = Pharmacy;
                RequestDoctor.VersionPharmacy = Pharmacy.Version;
                RequestDoctor.StatusPharmacy = Pharmacy.Status;
                RequestDoctor.Doctor = null;
                RequestDoctor.VersionDoctor = null;
                RequestDoctor.StatusDoctor = null;
            }
            if (Doctor != null)
            {
                RequestDoctor.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                RequestDoctor.Doctor = Doctor;
                RequestDoctor.VersionDoctor = Doctor.Version;
                RequestDoctor.StatusDoctor = Doctor.Status;

                RequestDoctor.Pharmacy = null;
                RequestDoctor.VersionPharmacy = null;
                RequestDoctor.StatusPharmacy = null;
            }
            var User = await _UserService.GetById(SaveRequestDoctorResource.IdUser);
            RequestDoctor.User = User;
            RequestDoctor.VersionUser = User.Version;
            RequestDoctor.StatusUser = User.Status;

            var Product = await _ProductService.GetById(SaveRequestDoctorResource.IdProduct);
            RequestDoctor.Product = Product;
            RequestDoctor.VersionProduct = Product.Version;
            RequestDoctor.StatusProduct = Product.Status;
            //*** Creation dans la base de données ***
            var NewRequestDoctor = await _RequestDoctorService.Create(RequestDoctor);
            //*** Mappage ***
            var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(NewRequestDoctor);
            if (RequestDoctorResource.IdDoctor != 0)
            {
                var OldDoctor = await _DoctorService.GetById(RequestDoctorResource.IdDoctor);
                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(OldDoctor);

                RequestDoctorResource.Doctor = DoctorResource;

            }
            if (RequestDoctorResource.IdProduct != 0)
            {
                var OldProduct = await _ProductService.GetById(RequestDoctorResource.IdProduct);
                var ProductResource = _mapperService.Map<Product, ProductResource>(OldProduct);

                var Bu = await _BusinessUnitService.GetById((int)OldProduct.IdBu);
                var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                ProductResource.Bu = BuResource;
                RequestDoctorResource.Product = ProductResource;

            }
            if (RequestDoctorResource.IdPharmacy != 0)
            {
                var OldPharmacy = await _PharmacyService.GetById(RequestDoctorResource.IdPharmacy);
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(OldPharmacy);

                RequestDoctorResource.Pharmacy = PharmacyResource;

            }
            if (RequestDoctorResource.IdUser != 0)
            {
                var OldUser = await _UserService.GetById(RequestDoctorResource.IdUser);
                var UserResource = _mapperService.Map<User, UserResource>(OldUser);

                RequestDoctorResource.User = UserResource;

            }
            return Ok(RequestDoctorResource);

        }
        [HttpGet]
        public async Task<ActionResult<List<RequestDoctorResource>>> GetAllRequestDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;


                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var RequestDoctors = await _RequestDoctorService.GetAll();
                if (RequestDoctors == null) return NotFound();
                List<RequestDoctorResource> RequestDoctorResources = new List<RequestDoctorResource>();
                foreach (var item in RequestDoctors)
                {
                    var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(item);
                    if (RequestDoctorResource.IdDoctor != 0)
                    {
                        var Doctor = await _DoctorService.GetById(RequestDoctorResource.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                        RequestDoctorResource.Doctor = DoctorResource;

                    }
                    if (RequestDoctorResource.IdProduct != 0)
                    {
                        var Product = await _ProductService.GetById(RequestDoctorResource.IdProduct);
                        var ProductResource = _mapperService.Map<Product, ProductResource>(Product);

                        var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                        var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                        ProductResource.Bu = BuResource;
                        RequestDoctorResource.Product = ProductResource;

                    }
                    if (RequestDoctorResource.IdPharmacy != 0)
                    {
                        var Pharmacy = await _PharmacyService.GetById(RequestDoctorResource.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                        RequestDoctorResource.Pharmacy = PharmacyResource;

                    }
                    if (RequestDoctorResource.IdUser != 0)
                    {
                        var User = await _UserService.GetById(RequestDoctorResource.IdUser);
                        var UserResource = _mapperService.Map<User, UserResource>(User);

                        RequestDoctorResource.User = UserResource;

                    }

                    RequestDoctorResources.Add(RequestDoctorResource);
                }
                return Ok(RequestDoctorResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<List<RequestDoctorResource>>> GetAllActifRequestDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var RequestDoctors = await _RequestDoctorService.GetAllActif();
                if (RequestDoctors == null) return NotFound();
                List<RequestDoctorResource> RequestDoctorResources = new List<RequestDoctorResource>();
                foreach (var item in RequestDoctors)
                {
                    var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(item);
                    if (RequestDoctorResource.IdDoctor != 0)
                    {
                        var Doctor = await _DoctorService.GetById(RequestDoctorResource.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                        RequestDoctorResource.Doctor = DoctorResource;

                    }
                    if (RequestDoctorResource.IdProduct != 0)
                    {
                        var Product = await _ProductService.GetById(RequestDoctorResource.IdProduct);
                        var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                        var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                        var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                        ProductResource.Bu = BuResource;
                        RequestDoctorResource.Product = ProductResource;

                    }
                    if (RequestDoctorResource.IdPharmacy != 0)
                    {
                        var Pharmacy = await _PharmacyService.GetById(RequestDoctorResource.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                        RequestDoctorResource.Pharmacy = PharmacyResource;

                    }
                    if (RequestDoctorResource.IdUser != 0)
                    {
                        var User = await _UserService.GetById(RequestDoctorResource.IdUser);
                        var UserResource = _mapperService.Map<User, UserResource>(User);

                        RequestDoctorResource.User = UserResource;

                    }
                    RequestDoctorResources.Add(RequestDoctorResource);
                }
                return Ok(RequestDoctorResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<List<RequestDoctorResource>>> GetAllInactifRequestDoctors([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var RequestDoctors = await _RequestDoctorService.GetAllInActif();
                if (RequestDoctors == null) return NotFound();
                List<RequestDoctorResource> RequestDoctorResources = new List<RequestDoctorResource>();
                foreach (var item in RequestDoctors)
                {
                    var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(item);
                    if (RequestDoctorResource.IdDoctor != 0)
                    {
                        var Doctor = await _DoctorService.GetById(RequestDoctorResource.IdDoctor);
                        var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                        RequestDoctorResource.Doctor = DoctorResource;

                    }
                    if (RequestDoctorResource.IdProduct != 0)
                    {
                        var Product = await _ProductService.GetById(RequestDoctorResource.IdProduct);
                        var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                        var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                        var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                        ProductResource.Bu = BuResource;
                        RequestDoctorResource.Product = ProductResource;

                    }
                    if (RequestDoctorResource.IdPharmacy != 0)
                    {
                        var Pharmacy = await _PharmacyService.GetById(RequestDoctorResource.IdPharmacy);
                        var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                        RequestDoctorResource.Pharmacy = PharmacyResource;

                    }
                    if (RequestDoctorResource.IdUser != 0)
                    {
                        var User = await _UserService.GetById(RequestDoctorResource.IdUser);
                        var UserResource = _mapperService.Map<User, UserResource>(User);

                        RequestDoctorResource.User = UserResource;

                    }
                    RequestDoctorResources.Add(RequestDoctorResource);
                }
                return Ok(RequestDoctorResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<RequestDoctorResource>> GetRequestDoctorById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var RequestDoctors = await _RequestDoctorService.GetById(Id);
                if (RequestDoctors == null) return NotFound();
                var RequestDoctorResource = _mapperService.Map<RequestDoctor, RequestDoctorResource>(RequestDoctors);
                if (RequestDoctorResource.IdDoctor != 0)
                {
                    var Doctor = await _DoctorService.GetById(RequestDoctorResource.IdDoctor);
                    var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(Doctor);

                    RequestDoctorResource.Doctor = DoctorResource;

                }
                if (RequestDoctorResource.IdProduct != 0)
                {
                    var Product = await _ProductService.GetById(RequestDoctorResource.IdProduct);
                    var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                    var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                    var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                    ProductResource.Bu = BuResource;
                    RequestDoctorResource.Product = ProductResource;

                }
                if (RequestDoctorResource.IdPharmacy != 0)
                {
                    var Pharmacy = await _PharmacyService.GetById(RequestDoctorResource.IdPharmacy);
                    var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                    RequestDoctorResource.Pharmacy = PharmacyResource;

                }
                if (RequestDoctorResource.IdUser != 0)
                {
                    var User = await _UserService.GetById(RequestDoctorResource.IdUser);
                    var UserResource = _mapperService.Map<User, UserResource>(User);

                    RequestDoctorResource.User = UserResource;

                }
                return Ok(RequestDoctorResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<RequestDoctorResource>> UpdateRequestDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id, SaveRequestDoctorResource SaveRequestDoctorResource)
        {
            var claims = _UserService.getPrincipal(Token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            var FirstName = claims.FindFirst("FirstName").Value;
            var LastName = claims.FindFirst("LastName").Value;
            var RequestDoctorToBeModified = await _RequestDoctorService.GetById(Id);
            if (RequestDoctorToBeModified == null) return BadRequest("Le RequestDoctor n'existe pas"); //NotFound();
            var RequestDoctor = _mapperService.Map<SaveRequestDoctorResource, RequestDoctor>(SaveRequestDoctorResource);
            //var newRequestDoctor = await _RequestDoctorService.Create(RequestDoctors);
            RequestDoctor.UpdatedOn = DateTime.UtcNow;
            RequestDoctor.CreatedOn = RequestDoctorToBeModified.CreatedOn;
            RequestDoctor.Active = 0;
            RequestDoctor.Status = 0;
            RequestDoctor.UpdatedBy = IdUser;
            RequestDoctor.CreatedBy = RequestDoctorToBeModified.CreatedBy;
            RequestDoctor.UpdatedByName = FirstName + " " + LastName;
            RequestDoctor.CreatedByName = RequestDoctorToBeModified.CreatedByName;
            var Doctor = await _DoctorService.GetById(SaveRequestDoctorResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveRequestDoctorResource.IdPharmacy);
            if (RequestDoctor.IdPharmacy != 0)
            {
                if (Pharmacy.IdPharmacy != RequestDoctor.IdPharmacy)
                {
                    RequestDoctor.Name = Pharmacy.Name;
                    RequestDoctor.Pharmacy = Pharmacy;
                    RequestDoctor.VersionPharmacy = Pharmacy.Version;
                    RequestDoctor.StatusPharmacy = Pharmacy.Status;
                    RequestDoctor.Doctor = null;
                    RequestDoctor.VersionDoctor = null;
                    RequestDoctor.StatusDoctor = null;
                }
                else
                {
                    RequestDoctor.Name = RequestDoctor.Name;
                    RequestDoctor.Pharmacy = RequestDoctor.Pharmacy;
                    RequestDoctor.VersionPharmacy = RequestDoctor.VersionPharmacy;
                    RequestDoctor.StatusPharmacy = RequestDoctor.StatusPharmacy;
                    RequestDoctor.Doctor = null;
                    RequestDoctor.VersionDoctor = null;
                    RequestDoctor.StatusDoctor = null;
                }
            }
            if (RequestDoctor.IdDoctor != 0)
            {
                if (Doctor.IdDoctor != RequestDoctor.IdDoctor)
                {
                    RequestDoctor.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                    RequestDoctor.Doctor = Doctor;
                    RequestDoctor.VersionDoctor = Doctor.Version;
                    RequestDoctor.StatusDoctor = Doctor.Status;

                    RequestDoctor.Pharmacy = null;
                    RequestDoctor.VersionPharmacy = null;
                    RequestDoctor.StatusPharmacy = null;
                }
                else
                {
                    {
                        RequestDoctor.Name = RequestDoctor.Name;
                        RequestDoctor.Doctor = RequestDoctor.Doctor;
                        RequestDoctor.VersionDoctor = RequestDoctor.VersionDoctor;
                        RequestDoctor.StatusDoctor = RequestDoctor.StatusDoctor;

                        RequestDoctor.Pharmacy = null;
                        RequestDoctor.VersionPharmacy = null;
                        RequestDoctor.StatusPharmacy = null;
                    }
                }
            }
            await _RequestDoctorService.Update(RequestDoctorToBeModified, RequestDoctor);

            var RequestDoctorUpdated = await _RequestDoctorService.GetById(Id);

            var RequestDoctorResourceUpdated = _mapperService.Map<RequestDoctor, RequestDoctorResource>(RequestDoctorUpdated);
            if (RequestDoctorResourceUpdated.IdDoctor != 0)
            {
                var DoctorNew = await _DoctorService.GetById(RequestDoctorResourceUpdated.IdDoctor);
                var DoctorResource = _mapperService.Map<Doctor, DoctorResource>(DoctorNew);

                RequestDoctorResourceUpdated.Doctor = DoctorResource;

            }
            if (RequestDoctorResourceUpdated.IdProduct != 0)
            {
                var Product = await _ProductService.GetById(RequestDoctorResourceUpdated.IdProduct);
                var ProductResource = _mapperService.Map<Product, ProductResource>(Product);
                var Bu = await _BusinessUnitService.GetById((int)Product.IdBu);
                var BuResource = _mapperService.Map<BusinessUnit, BusinessUnitResource>(Bu);
                ProductResource.Bu = BuResource;
                RequestDoctorResourceUpdated.Product = ProductResource;

            }
            if (RequestDoctorResourceUpdated.IdPharmacy != 0)
            {
                var PharmacyNew = await _PharmacyService.GetById(RequestDoctorResourceUpdated.IdPharmacy);
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyNew);

                RequestDoctorResourceUpdated.Pharmacy = PharmacyResource;

            }
            if (RequestDoctorResourceUpdated.IdUser != 0)
            {
                var User = await _UserService.GetById(RequestDoctorResourceUpdated.IdUser);
                var UserResource = _mapperService.Map<User, UserResource>(User);

                RequestDoctorResourceUpdated.User = UserResource;

            }
            return Ok(RequestDoctorResourceUpdated);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteRequestDoctor([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var RequestDoctorToBeModified = await _RequestDoctorService.GetById(Id);
                var sub = await _RequestDoctorService.GetById(Id);
                if (sub == null) return BadRequest("Le RequestDoctor  n'existe pas"); //NotFound();
                await _RequestDoctorService.Delete(sub);
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
                List<RequestDoctor> empty = new List<RequestDoctor>();
                foreach (var item in Ids)
                {
                    var sub = await _RequestDoctorService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le RequestDoctor  n'existe pas"); //NotFound();

                }
                await _RequestDoctorService.DeleteRange(empty);
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
