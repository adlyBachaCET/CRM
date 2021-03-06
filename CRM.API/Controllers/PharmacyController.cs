using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
using CRM.Core.Services;
using CRM_API.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class PharmacyController : ControllerBase
    {

        private readonly IPharmacyService _PharmacyService;
        private readonly IUserService _UserService;
        private readonly IPhoneService _PhoneService;
        private readonly IBrickService _BrickService;
        private readonly IVisitReportService _VisitReportService;
        private readonly IRequestRpService _RequestRpService;
        private readonly ICommandeService _CommandeService;
        private readonly ILocalityService _LocalityService;
        private readonly IParticipantService _ParticipantService;
        private readonly IObjectionService _ObjectionService;
        private readonly IProductPharmacyService _ProductPharmacyService;
        private readonly IPotentielService _PotentielService;
        private readonly IMapper _mapperService;
        public PharmacyController(ILocalityService LocalityService,
            IBrickService BrickService, IPhoneService PhoneService,

            IUserService UserService,
             IProductPharmacyService ProductPharmacyService,
           IPotentielService PotentielService, IObjectionService ObjectionService,
                        IParticipantService ParticipantService,
                                    IRequestRpService RequestRpService,
  ICommandeService CommandeService,
      
            IVisitReportService VisitReportService, 
            IPharmacyService PharmacyService, IMapper mapper)
        {
            _BrickService = BrickService;
            _LocalityService = LocalityService;

            _PhoneService = PhoneService;
            _VisitReportService = VisitReportService;
  
            _PotentielService = PotentielService;
            _ProductPharmacyService = ProductPharmacyService;
            _PharmacyService = PharmacyService;

            _ObjectionService = ObjectionService;
            _PhoneService = PhoneService;
            _UserService = UserService;
            _ParticipantService = ParticipantService;
            _RequestRpService = RequestRpService;
            _CommandeService = CommandeService;

            _mapperService = mapper;
        }
        [HttpPost("Verify")]
        public async Task<ActionResult> Verify([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
                SaveAddPharmacyResource SaveAddPharmacyResource)
        {
            try {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }

                var Exist = await _PharmacyService.Verify(SaveAddPharmacyResource);

            if (!Exist.ExistPharmacyEmail && !Exist.ExistPharmacyFirstName && !Exist.ExistPharmacyLastName && !Exist.ExistPharmacyName) {
                return BadRequest();
            }
            else
            {
                var genericResult = new { Exist = "Already exists", Pharmacy = Exist };

                return Ok(genericResult);
            }
        }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }
}

        [HttpPost]
        public async Task<ActionResult<PharmacyObjectList>> CreatePharmacy([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
       SaveAddPharmacyResource SaveAddPharmacyResource)
        {
            try {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);

      
                    //*** Mappage ***
                    var Pharmacy = _mapperService.Map<SavePharmacyResource, Pharmacy>(SaveAddPharmacyResource.SavePharmacyResource);
                var Locality1 = await _LocalityService.GetById(SaveAddPharmacyResource.SavePharmacyResource.IdLocality1);
                if (Locality1 == null)
                {
                    throw new InvalidOperationException("No Locality1 found with Id " + SaveAddPharmacyResource.SavePharmacyResource.IdLocality1);
                }
                Pharmacy.NameLocality1 = Locality1.Name;
                Pharmacy.VersionLocality1 = Locality1.Version;
                Pharmacy.StatusLocality1 = Locality1.Status;
                Pharmacy.IdLocality1 = Locality1.IdLocality;
                var Locality2 = await _LocalityService.GetById(SaveAddPharmacyResource.SavePharmacyResource.IdLocality2);
                if (Locality2 == null)
                {
                    throw new InvalidOperationException("No Locality1 found with Id " + SaveAddPharmacyResource.SavePharmacyResource.IdLocality2);
                }
                Pharmacy.NameLocality2 = Locality2.Name;
                Pharmacy.VersionLocality2 = Locality2.Version;
                Pharmacy.StatusLocality2 = Locality2.Status;
                Pharmacy.IdLocality2 = Locality2.IdLocality;
                Pharmacy.CreatedOn = DateTime.UtcNow;
                     Pharmacy.UpdatedOn = DateTime.UtcNow;
                    Pharmacy.Active = 0;
                 Pharmacy.Version = 0;
                 Pharmacy.CreatedBy = Id;
                    Pharmacy.UpdatedBy = Id;

                    var Brick1 = await _BrickService.GetByIdActif(SaveAddPharmacyResource.SavePharmacyResource.IdBrick1);
                if (Brick1 == null)
                {
                    throw new InvalidOperationException("No Brick1 found with Id " + SaveAddPharmacyResource.SavePharmacyResource.IdBrick1);
                }
                var Brick2 = await _BrickService.GetByIdActif(SaveAddPharmacyResource.SavePharmacyResource.IdBrick2);
                if (Brick2 == null)
                {
                    throw new InvalidOperationException("No Brick2 found with Id " + SaveAddPharmacyResource.SavePharmacyResource.IdBrick2);
                }
                if (Brick1 != null)
                    {
                        Pharmacy.IdBrick1 = Brick1.IdBrick;
                        Pharmacy.VersionBrick1 = Brick1.Version;
                        Pharmacy.StatusBrick1 = Brick1.Status;
                        Pharmacy.NameBrick1 = Brick1.Name;
                        Pharmacy.NumBrick1 = Brick1.NumSystemBrick;
                    }
                    else
                    {
                        Pharmacy.IdBrick1 =null;
                        Pharmacy.VersionBrick1 = null;
                        Pharmacy.StatusBrick1 = null;
                        Pharmacy.NameBrick1 = "";
                        Pharmacy.NumBrick1 =0;
                    }
                    if (Brick2 != null)
                    {
                        Pharmacy.IdBrick2 = Brick2.IdBrick;
                        Pharmacy.VersionBrick2 = Brick2.Version;
                        Pharmacy.StatusBrick2 = Brick2.Status;
                        Pharmacy.NameBrick2 = Brick2.Name;
                        Pharmacy.NumBrick2 = Brick2.NumSystemBrick;
                        

                    }
                    else
                    {
                        Pharmacy.IdBrick2 = null;
                        Pharmacy.VersionBrick2 = null;
                        Pharmacy.StatusBrick2 = null;
                        Pharmacy.NameBrick2 = "";
                        Pharmacy.NumBrick2 = 0;
                    }
                    if (Role == "Manager") { 
            Pharmacy.Status = Status.Approuved;
                    }
            else if (Role == "Delegue")
                    {
                        Pharmacy.Status = Status.Pending;
                    }
                    //*** Creation dans la base de donn??es ***
                    var NewPharmacy = await _PharmacyService.Create(Pharmacy);
                if (NewPharmacy == null)
                {
                    throw new InvalidOperationException("No New Pharmacy created " + Pharmacy);
                }
                //*** Mappage ***
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(NewPharmacy);
                foreach (var item in SaveAddPharmacyResource.SavePhoneResource)
                {

                    var Phone = _mapperService.Map<SavePhoneResource, Phone>(item);
                    Phone.IdPharmacy = PharmacyResource.Id;
                Phone.VersionPharmacy = PharmacyResource.Version;
                Phone.StatusPharmacy = PharmacyResource.Status;
                Phone.Pharmacy = Pharmacy;

                //*** Creation dans la base de donn??es ***
                var NewPhone = await _PhoneService.Create(Phone);

                    //*** Mappage ***
                    var PhoneResource = _mapperService.Map<Phone, PhoneResource>(NewPhone);
                    if (NewPhone == null)
                    {
                        throw new InvalidOperationException("No New Phone created " + Phone);
                    }
                    var PhoneResourceOld = PhoneResource;
                }
            var PharmacyObject = await PharmacyById(NewPharmacy.Id);
                if (PharmacyObject == null)
                {
                    throw new InvalidOperationException("No Pharmacy found " + NewPharmacy.Id);
                }

                return Ok(PharmacyObject);

        }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }
}
        [HttpGet("Phone/{Number}")]
        public async Task<ActionResult<PharmacyObjectList>> GetPharmacysNumber([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Number)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                List<PharmacyObjectList> PharmacyObjectList = new List<PharmacyObjectList>();

                var Pharmacys = await _PharmacyService.GetByExistantPhoneNumberActif(Number);
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                foreach (var item in Pharmacys)
                {
                    var Pharmacy = await PharmacyById(item.Id);
                    if (Pharmacys == null)
                    {
                        throw new InvalidOperationException("No Pharmacys with ID "+item.Id);
                    }
                    PharmacyObjectList.Add(Pharmacy);
                }
                

                return Ok(PharmacyObjectList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("NearBy")]
        public async Task<ActionResult<PharmacyObjectList>> GetPharmacysNumber([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      Nearby Nearby)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                List<PharmacyObjectList> PharmacyObjectList = new List<PharmacyObjectList>();

                var Pharmacys = await _PharmacyService.GetByNearByActif(Nearby.Locality1, Nearby.Locality2, Nearby.CodePostal);
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                foreach (var item in Pharmacys)
                {
                    var Pharmacy = await PharmacyById(item.Id);
                    if (Pharmacy == null)
                    {
                        throw new InvalidOperationException("No Pharmacys found with Id "+ item.Id);
                    }
                    PharmacyObjectList.Add(Pharmacy);
                }
                

                return Ok(PharmacyObjectList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<PharmacyObjectList>> GetAllPharmacys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
      )
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                List<PharmacyObjectList> PharmacyObjectList = new List<PharmacyObjectList>();

                var Pharmacys = await _PharmacyService.GetAllActif();
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                if (Pharmacys == null) return NotFound();
                foreach (var item in Pharmacys)
                {
                    var Pharmacy = await PharmacyById(item.Id);
                    if (Pharmacy == null)
                    {
                        throw new InvalidOperationException("No Pharmacys found "+item.Id);
                    }
                    PharmacyObjectList.Add(Pharmacy);
                }
                

                return Ok(PharmacyObjectList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Assigned")]
        public async Task<ActionResult<PharmacyObjectList>> GetPharmacysAssigned([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
      )
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Pharmacys = await _PharmacyService.GetPharmacysAssigned();
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                return Ok(Pharmacys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Actif")]
        public async Task<ActionResult<PharmacyObjectList>> GetAllActifPharmacys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
      )
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Pharmacys = await _PharmacyService.GetAllActif();
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                return Ok(Pharmacys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<PharmacyObjectList>> GetAllInactifPharmacys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
      )
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Pharmacys = await _PharmacyService.GetAllInActif();
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                return Ok(Pharmacys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<PharmacyObjectList> PharmacyById(int Id)
        {
            
            PharmacyObjectList PharmacyProfile = new PharmacyObjectList();
            


                var Pharmacy = await _PharmacyService.GetById(Id);
            if (Pharmacy == null)
            {
                throw new InvalidOperationException("No Pharmacy found with ID "+ Id);
            }
            var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                PharmacyProfile.Pharmacy = PharmacyResource;

                List<PhoneResource> PhoneResources = new List<PhoneResource>();

                foreach (var item in Pharmacy.Phone)
                {
                    var Bu = _mapperService.Map<Phone, PhoneResource>(item);

                    if (Bu != null)
                    {
                        PhoneResources.Add(Bu);
                    }
                }
                PharmacyProfile.Phone = PhoneResources;

            var Potentiel = await _PotentielService.GetById(Pharmacy.IdPotentiel);
            if (Potentiel == null)
            {
                throw new InvalidOperationException("No potentiel found for pharmacy with Id " +Id);
            }
            PotentielPharmacy PotentielPharmacy = new PotentielPharmacy();
            if (Potentiel != null)
            {
                PotentielPharmacy.IdPotentiel = Potentiel.IdPotentiel;
                PotentielPharmacy.NamePotentiel = Potentiel.Name;
                PharmacyResource.PotentielPharmacy = PotentielPharmacy;
            }

            return PharmacyProfile;
    
            }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PharmacyProfile>> GetPharmacyById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Id)
        {
            var claims = _UserService.getPrincipal(Token);

            if (claims == null)
            {
                throw new InvalidOperationException("Invalid Token = " + Token);
            }
            PharmacyProfile PharmacyProfile = new PharmacyProfile();
            try
            {
            

                var Pharmacy = await _PharmacyService.GetById(Id);
                if (Pharmacy == null)
                {
                    throw new InvalidOperationException("No Pharmacy found with ID " + Id);
                }
                var PharmacyResource = _mapperService.Map<Pharmacy, PharmacyResource>(Pharmacy);

                PharmacyProfile.Pharmacy = PharmacyResource;
                if (Pharmacy == null) return NotFound();
                List<PhoneResource> PhoneResources = new List<PhoneResource>();

                foreach (var item in Pharmacy.Phone)
                {
                    var Phone = _mapperService.Map<Phone, PhoneResource>(item);

                    if (Phone != null)
                    {
                        PhoneResources.Add(Phone);
                    }
                }
                PharmacyProfile.Phone = PhoneResources;
                var DoctorVisit = await _VisitReportService.GetByIdPharmacy(Id);
              
                List<VisitReportResource> VisitReportResources = new List<VisitReportResource>();

                foreach (var item in DoctorVisit)
                {
                    var VisitDoctor = _mapperService.Map<VisitReport, VisitReportResource>(item);

                    if (VisitDoctor != null)
                    {
                        VisitReportResources.Add(VisitDoctor);
                    }
                }

                PharmacyProfile.VisitReports = VisitReportResources;

               

                List<ObjectionResource> ObjectionResources = new List<ObjectionResource>();

                List<ObjectionResource> RequestResources = new List<ObjectionResource>();

                foreach (var item in Pharmacy.Objection)
                {
                    var ObjectionRequest = _mapperService.Map<Objection, ObjectionResource>(item);

                    if (ObjectionRequest != null)
                    {
                        if (ObjectionRequest.RequestObjection == RequestObjection.Objection)
                            ObjectionResources.Add(ObjectionRequest);
                        else if (ObjectionRequest.RequestObjection == RequestObjection.Request)
                            RequestResources.Add(ObjectionRequest);
                    }
                }
                PharmacyProfile.Request = RequestResources;

                PharmacyProfile.Objection = ObjectionResources;

               
                var Potentiel = await _PotentielService.GetById(Pharmacy.IdPotentiel);
                if (Potentiel == null)
                {
                    throw new InvalidOperationException("No Potentiel found with ID " + Pharmacy.IdPotentiel);
                }
                PotentielPharmacy PotentielPharmacy = new PotentielPharmacy();
                if (Potentiel != null)
                {
                    PotentielPharmacy.IdPotentiel = Potentiel.IdPotentiel;
                    PotentielPharmacy.NamePotentiel = Potentiel.Name;
                    PharmacyResource.PotentielPharmacy = PotentielPharmacy;
                }

               

             

                List<RequestRp> RequestRpList = new List<RequestRp>();
                foreach (var item in Pharmacy.Participant)
                {
                    var RequestRp = await _RequestRpService.GetById(item.IdRequestRp);
                    if (RequestRp == null)
                    {
                        throw new InvalidOperationException("No RequestRp found with ID " + item.IdRequestRp);
                    }
                    if (RequestRp != null) 
                    { 
                    RequestRpList.Add(RequestRp);
                    }
                }
                List<RequestRpResource> RequestRpResources = new List<RequestRpResource>();

                foreach (var item in RequestRpList)
                {
                    var Bu = _mapperService.Map<RequestRp, RequestRpResource>(item);

                    if (Bu != null)
                    {
                        RequestRpResources.Add(Bu);
                    }
                }
                PharmacyProfile.RequestRp = RequestRpResources;


             

                List<CommandeResource> CommandeResources = new List<CommandeResource>();

                foreach (var item in Pharmacy.Commande)
                {
                    var Bu = _mapperService.Map<Commande, CommandeResource>(item);

                    if (Bu != null)
                    {
                        CommandeResources.Add(Bu);
                    }
                }
                PharmacyProfile.Commande = CommandeResources;

             

                List<ProductResource> ProductResources = new List<ProductResource>();


                foreach (var item in Pharmacy.ProductPharmacy)
                {
                    var Product = _mapperService.Map<Product, ProductResource> (item.Product);


                    if (Product != null)
                    {
                        ProductResources.Add(Product);
                    }
                }

                PharmacyProfile.Product = ProductResources;
                return Ok(PharmacyProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<PharmacyObjectList>> UpdatePharmacy([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Id, SavePharmacyResource SavePharmacyResource)
        {
            try {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Role").Value);

                var PharmacyToBeModified = await _PharmacyService.GetById(Id);
                if (PharmacyToBeModified == null)
                {
                    throw new InvalidOperationException("No Potentiel found with ID " + Id);
                }
            var Pharmacys = _mapperService.Map<SavePharmacyResource, Pharmacy>(SavePharmacyResource);
 


            //*** Mappage ***
            var Pharmacy = _mapperService.Map<SavePharmacyResource, Pharmacy>(SavePharmacyResource);
            var Locality1 = await _LocalityService.GetById(SavePharmacyResource.IdLocality1);
                if (Locality1 == null)
                {
                    throw new InvalidOperationException("No Locality1 found with Id " + SavePharmacyResource.IdLocality1);
                }
                Pharmacy.NameLocality1 = Locality1.Name;
            Pharmacy.VersionLocality1 = Locality1.Version;
            Pharmacy.StatusLocality1 = Locality1.Status;
            Pharmacy.IdLocality1 = Locality1.IdLocality;
            var Locality2 = await _LocalityService.GetById(SavePharmacyResource.IdLocality2);
                if (Locality2 == null)
                {
                    throw new InvalidOperationException("No Locality2 found with Id " + SavePharmacyResource.IdLocality2);
                }
                Pharmacy.NameLocality2 = Locality2.Name;
            Pharmacy.VersionLocality2 = Locality2.Version;
            Pharmacy.StatusLocality2 = Locality2.Status;
            Pharmacy.IdLocality2 = Locality2.IdLocality;
            Pharmacy.CreatedOn = DateTime.UtcNow;
            Pharmacy.UpdatedOn = DateTime.UtcNow;
            Pharmacy.Active = 0;
            Pharmacy.Version = 0;
            Pharmacy.CreatedBy = PharmacyToBeModified.CreatedBy;
            Pharmacy.UpdatedBy = IdUser;

            var Brick1 = await _BrickService.GetByIdActif(SavePharmacyResource.IdBrick1);
                if (Brick1 == null)
                {
                    throw new InvalidOperationException("No Brick 1 found with Id " + SavePharmacyResource.IdBrick1);
                }
                var Brick2 = await _BrickService.GetByIdActif(SavePharmacyResource.IdBrick2);
                if (Brick2 == null)
                {
                    throw new InvalidOperationException("No Brick 2 found with Id " + SavePharmacyResource.IdBrick2);
                }
                if (Brick1 != null)
            {
                Pharmacy.IdBrick1 = Brick1.IdBrick;
                Pharmacy.VersionBrick1 = Brick1.Version;
                Pharmacy.StatusBrick1 = Brick1.Status;
                Pharmacy.NameBrick1 = Brick1.Name;
                Pharmacy.NumBrick1 = Brick1.NumSystemBrick;
  
            }
            else
            {
                Pharmacy.IdBrick1 = null;
                Pharmacy.VersionBrick1 = null;
                Pharmacy.StatusBrick1 = null;
                Pharmacy.NameBrick1 = "";
                Pharmacy.NumBrick1 = 0;
            }
            if (Brick2 != null)
            {
                Pharmacy.IdBrick2 = Brick2.IdBrick;
                Pharmacy.VersionBrick2 = Brick2.Version;
                Pharmacy.StatusBrick2 = Brick2.Status;
                Pharmacy.NameBrick2 = Brick2.Name;
                Pharmacy.NumBrick2 = Brick2.NumSystemBrick;
                
            }
            else
            {
                Pharmacy.IdBrick2 = null;
                Pharmacy.VersionBrick2 = null;
                Pharmacy.StatusBrick2 = null;
                Pharmacy.NameBrick2 = "";
                Pharmacy.NumBrick2 = 0;
            }
            if (Role == "Manager")
            {
                Pharmacy.Status = Status.Approuved;
            }
            else if (Role == "Delegue")
            {
                Pharmacy.Status = Status.Pending;
            }
            await _PharmacyService.Update(PharmacyToBeModified, Pharmacys);

            var PharmacyUpdated = await _PharmacyService.GetById(Id);
                if (PharmacyUpdated == null)
                {
                    throw new InvalidOperationException("No Pharmacy found with Id " + Id);
                }
                var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyUpdated);
            var PharmacyUpdatedNew = await PharmacyById(PharmacyResourceUpdated.Id);
                if (PharmacyUpdatedNew == null)
                {
                    throw new InvalidOperationException("No Pharmacy found with Id " + Id);
                }
                return Ok(PharmacyUpdatedNew);
        }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }
}
        [HttpPost("GetAll")]
        public async Task<ActionResult<List<ObjectionResource>>> GetAll([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, GrossistePharmacyStatus GrossistePharmacyStatus)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Role = claims.FindFirst("Role").Value;

                List<PharmacyObjectList> PharmacyObjectList = new List<PharmacyObjectList>();
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Pharmacys = await _PharmacyService.GetAll(GrossistePharmacyStatus.Status, GrossistePharmacyStatus.GrossistePharmacy);
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                foreach (var item in Pharmacys)
                {
                    var Pharmacy = await PharmacyById(item.Id);
                    if (Pharmacy == null)
                    {
                        throw new InvalidOperationException("No Pharmacy found ");
                    }
                    PharmacyObjectList.Add(Pharmacy);
                }


                return Ok(PharmacyObjectList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<PharmacyObjectList>> GetById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token, GrossistePharmacyStatusById GrossistePharmacyStatusById)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Role = claims.FindFirst("Role").Value;


                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                var Pharmacys = await _PharmacyService.GetById(GrossistePharmacyStatusById.Id, null, GrossistePharmacyStatusById.GrossistePharmacy);
                if (Pharmacys == null)
                {
                    throw new InvalidOperationException("No Pharmacys found ");
                }
                var PharmacyObject = await PharmacyById(Pharmacys.Id);
                if (PharmacyObject == null)
                {
                    throw new InvalidOperationException("No Pharmacys found with Id "+ Pharmacys.Id);
                }

                return Ok(PharmacyObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<PharmacyResource>> ApprouvePharmacy([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
       
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var PharmacyToBeApprouved = await _PharmacyService.GetById(Id);
                if (PharmacyToBeApprouved == null)
                {
                    throw new InvalidOperationException("No Pharmacy To Be Approuved found with Id " + Id);
                }
                PharmacyToBeApprouved.UpdatedOn = DateTime.UtcNow;
                PharmacyToBeApprouved.UpdatedBy = IdUser;

                await _PharmacyService.Approuve(PharmacyToBeApprouved, PharmacyToBeApprouved);

                var PharmacyApprouved = await _PharmacyService.GetById(Id);
                if (PharmacyApprouved == null)
                {
                    throw new InvalidOperationException("No Pharmacy Approuved found with Id " + Id);
                }
                var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyApprouved);
                var PharmacyUpdatedNew = await PharmacyById(PharmacyResourceUpdated.Id);
                if (PharmacyUpdatedNew == null)
                {
                    throw new InvalidOperationException("No Pharmacys found with Id " + Id);
                }
                return Ok(PharmacyUpdatedNew);
            
        }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<PharmacyResource>> RejectPharmacy([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Id)
        {
            try
            {

                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var PharmacyToBeRejected = await _PharmacyService.GetById(Id);
                if (PharmacyToBeRejected == null)
                {
                    throw new InvalidOperationException("No Pharmacy to be rejected found with Id " + Id);
                }

                PharmacyToBeRejected.UpdatedOn = DateTime.UtcNow;
                PharmacyToBeRejected.UpdatedBy = IdUser;

                await _PharmacyService.Reject(PharmacyToBeRejected, PharmacyToBeRejected);

                var PharmacyRejected = await _PharmacyService.GetById(Id);
                if (PharmacyRejected == null)
                {
                    throw new InvalidOperationException("No Pharmacy Rejected found with Id " + Id);
                }
                var PharmacyResourceUpdated = _mapperService.Map<Pharmacy, PharmacyResource>(PharmacyRejected);
                var PharmacyRejectedNew = await PharmacyById(PharmacyResourceUpdated.Id);
                if (PharmacyRejectedNew == null)
                {
                    throw new InvalidOperationException("No New Pharmacy Rejected found with Id " + Id);
                }
                return Ok(PharmacyRejectedNew);
    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePharmacy([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                var Pharmacy = await _PharmacyService.GetById(Id);
                if (Pharmacy == null)
                {
                    throw new InvalidOperationException("No Pharmacy found with Id " + Id);
                }
                await _PharmacyService.Delete(Pharmacy);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
      List<int> Ids)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);

                if (claims == null)
                {
                    throw new InvalidOperationException("Invalid Token = " + Token);
                }
                List<Pharmacy> empty = new List<Pharmacy>();
                foreach (var item in Ids)
                {
                    var Pharmacy = await _PharmacyService.GetById(item);
                    if (Pharmacy == null)
                    {
                        throw new InvalidOperationException("No Pharmacy found with Id " + item);
                    }
                    empty.Add(Pharmacy);

                }
                await _PharmacyService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
