using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
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

    public class LocationController : ControllerBase
    {
        public IList<Location> Locations;
        private readonly IUserService _UserService;
        private readonly IServiceService _ServiceService;
        private readonly ILocalityService _LocalityService;
        private readonly IBrickService _BrickService;

        private readonly ILocationService _LocationService;
        private readonly ILocationDoctorService _LocationDoctorService;

        private readonly ILocationTypeService _LocationTypeService;

        private readonly IMapper _mapperService;
        public LocationController(ILocalityService LocalityService, IServiceService ServiceService, ILocationService LocationService,
            ILocationDoctorService LocationDoctorService, IUserService UserService, IBrickService BrickService, ILocationTypeService LocationTypeService, IMapper mapper)
        {
            _LocationService = LocationService;

            _LocalityService = LocalityService;
            _BrickService = BrickService;

            _ServiceService = ServiceService;
            _UserService = UserService;
            _LocationTypeService = LocationTypeService;
            _LocationDoctorService = LocationDoctorService;

            _mapperService = mapper;
        }

        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<LocationResource>> ApprouveLocation(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var LocationToBeModified = await _LocationService.GetById(Id);
                if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
                                                                                                 //var newLocation = await _LocationService.Create(Locations);
                                                                                                 // Locations.CreatedOn = SaveLocationResource.;
                LocationToBeModified.UpdatedOn = DateTime.UtcNow;
                LocationToBeModified.UpdatedBy = IdUser;

                await _LocationService.Approuve(LocationToBeModified, LocationToBeModified);

                var LocationUpdated = await _LocationService.GetById(Id);

                var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

                return Ok(LocationResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<LocationResource>> RejectLocation(int Id)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
            var claims = _UserService.getPrincipal(token);
            var Role = claims.FindFirst("Role").Value;
            var IdUser = int.Parse(claims.FindFirst("Id").Value);
            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            //var newLocation = await _LocationService.Create(Locations);
            // Locations.CreatedOn = SaveLocationResource.;
            LocationToBeModified.UpdatedOn = DateTime.UtcNow;
            LocationToBeModified.UpdatedBy = IdUser;

            await _LocationService.Reject(LocationToBeModified, LocationToBeModified);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok(LocationResourceUpdated);
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }
        [HttpPost]
        public async Task<ActionResult<LocationResource>> CreateLocation([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")]
        string Token,SaveLocationResource SaveLocationResource)
        {

                var claims = _UserService.getPrincipal(Token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

                var LocationExiste = await _LocationService.GetByExistantActif(SaveLocationResource.LocationAdd.Name, SaveLocationResource.LocationAdd.IdLocationType);
                LocationResource LocationResourceOld = new LocationResource();
                if (LocationExiste == null)
                { //*** Mappage ***
                    var Location = _mapperService.Map<LocationAdd, Location>(SaveLocationResource.LocationAdd);
                    Location.UpdatedOn = DateTime.UtcNow;
                    Location.CreatedOn = DateTime.UtcNow;
                    Location.Version = 0;
                    Location.Active = 0;

                    if (Role == "Manager")
                    {
                        Location.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Location.Status = Status.Pending;
                    }
                    var Locality1 = await _LocalityService.GetById(SaveLocationResource.LocationAdd.IdLocality1);
                    Location.NameLocality1 = Locality1.Name;
                    Location.VersionLocality1 = Locality1.Version;
                    Location.StatusLocality1 = Locality1.Status;
                    Location.IdLocality1 = Locality1.IdLocality;
                    var Locality2 = await _LocalityService.GetById(SaveLocationResource.LocationAdd.IdLocality2);
                    Location.NameLocality2 = Locality2.Name;
                    Location.VersionLocality2 = Locality2.Version;
                    Location.StatusLocality2 = Locality2.Status;
                    Location.IdLocality2 = Locality1.IdLocality;
                    var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.LocationAdd.IdLocationType);
                    Location.NameLocationType = NewLocationType.Name;
                    Location.StatusLocationType = NewLocationType.Status;
                    Location.VersionLocationType = NewLocationType.Version;
                    Location.TypeLocationType = NewLocationType.Type;
                    Location.CreatedBy = Id;
                    Location.UpdatedBy = Id;
                    var Brick1 = await _BrickService.GetByIdActif(SaveLocationResource.LocationAdd.IdBrick1);
                    var Brick2 = await _BrickService.GetByIdActif(SaveLocationResource.LocationAdd.IdBrick2);
                    if (Brick1 != null)
                    {
                        Location.IdBrick1 = Brick1.IdBrick;
                        Location.VersionBrick1 = Brick1.Version;
                        Location.StatusBrick1 = Brick1.Status;
                        Location.NameBrick1 = Brick1.Name;
                        Location.NumBrick1 = Brick1.NumSystemBrick;
                        // Pharmacy.Brick1 = Brick1;
                    }
                    else
                    {
                        Location.IdBrick1 = null;
                        Location.VersionBrick1 = null;
                        Location.StatusBrick1 = null;
                        Location.NameBrick1 = "";
                        Location.NumBrick1 = 0;
                    }
                    if (Brick2 != null)
                    {
                        Location.IdBrick2 = Brick2.IdBrick;
                        Location.VersionBrick2 = Brick2.Version;
                        Location.StatusBrick2 = Brick2.Status;
                        Location.NameBrick2 = Brick2.Name;
                        Location.NumBrick2 = Brick2.NumSystemBrick;
                        // Pharmacy.Brick2 = Brick2;
                    }
                    else
                    {
                        Location.IdBrick2 = null;
                        Location.VersionBrick2 = null;
                        Location.StatusBrick2 = null;
                        Location.NameBrick2 = "";
                        Location.NumBrick2 = 0;
                    }
                    List<Service> Services = new List<Service>();
                    List<int> Order = new List<int>();
                    foreach (var item in SaveLocationResource.SaveServiceResource)
                    {
                        var Service = _mapperService.Map<SaveServiceResource, Service>(item);
                        Service.UpdatedOn = DateTime.UtcNow;
                        Service.CreatedOn = DateTime.UtcNow;
                        Service.Version = 0;
                        Service.Active = 0;
                        Service.CreatedBy = Id;
                        Service.UpdatedBy = Id;
                        //Order.Add(item.)
                        if (Role == "Manager")
                        {
                            Service.Status = Status.Approuved;
                        }
                        else if (Role == "Delegue")
                        {
                            Service.Status = Status.Pending;
                        }
                        var NewService = await _ServiceService.Create(Service);
                        Services.Add(NewService);
                    }
                    //*** Creation dans la base de données ***
                    var NewLocation = await _LocationService.Create(Location);
                    //*** Mappage ***
                    var LocationResource = _mapperService.Map<Location, LocationResource>(NewLocation);
                    foreach (var item in Services)
                    {
                       LocationDoctor LD = new LocationDoctor();
                        LD.IdLocation = LocationResource.IdLocation;
                        LD.IdDoctor = 0;
                        LD.IdService = item.IdService;
                        LD.UpdatedOn = DateTime.UtcNow;
                        LD.CreatedOn = DateTime.UtcNow;
                        LD.Version = 0;
                        LD.Active = 0;
                        LD.CreatedBy = Id;
                        LD.UpdatedBy = Id;
                        //LD.Order = SaveLocationResource.OrderLocation.Order;
                        //LD.Primary = SaveLocationResource.OrderLocation.Primary;
                        //LD.ChefService = SaveLocationResource.Chief.ChiefService;
                        var NewLocationDoctor = await _LocationDoctorService.Create(LD);

                    }


                    return Ok(LocationResource);
                }
                else
                {
                    var genericResult = new { Exist = "Already exists", Location = LocationExiste };

                    return Ok(genericResult);
                }
            }
    
    
        [HttpGet]
        public async Task<ActionResult<LocationResource>> GetAllLocations([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _LocationService.GetAll();
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
        public async Task<ActionResult<LocationResource>> GetAllActifLocations([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _LocationService.GetAllActif();
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
        public async Task<ActionResult<LocationResource>> GetAllInactifLocations([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
            {
                var Employe = await _LocationService.GetAllInActif();
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
        public async Task<ActionResult<LocationResource>> GetLocationById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {
                var Locations = await _LocationService.GetById(Id);
                if (Locations == null) return NotFound();
                var LocationRessource = _mapperService.Map<Location, LocationResource>(Locations);
                return Ok(LocationRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<LocationResource>> UpdateLocation([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id, SaveLocationResource SaveLocationResource)
        {
          
             var claims = _UserService.getPrincipal(Token);
             var Role = claims.FindFirst("Role").Value;
             var IdUser = int.Parse(claims.FindFirst("Id").Value);

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            var Location = _mapperService.Map<SaveLocationResource, Location>(SaveLocationResource);
            //var newLocation = await _LocationService.Create(Locations);
            Location.UpdatedOn = DateTime.UtcNow;
            Location.CreatedOn = LocationToBeModified.CreatedOn;
          
            Location.CreatedBy = Location.CreatedBy;
            Location.UpdatedBy = IdUser;
            var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.LocationAdd.IdLocationType);
            Location.NameLocationType = NewLocationType.Name;
                if (Role == "Manager")
                {
                    Location.Status = Status.Approuved;
                }
                else if (Role == "Delegue")
                {
                    Location.Status = Status.Pending;
                }
            Location.VersionLocationType = NewLocationType.Version+1;
            Location.TypeLocationType = NewLocationType.Type;
            await _LocationService.Update(LocationToBeModified, Location);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok();
            }
      
        


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteLocation([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,int Id)
        {
            try
            {

                var sub = await _LocationService.GetById(Id);
                if (sub == null) return BadRequest("Le Location  n'existe pas"); //NotFound();
                await _LocationService.Delete(sub);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, List<int> Ids)
        {
            try
            {
                List<Location> empty = new List<Location>();
                foreach (var item in Ids)
                {
                    var sub = await _LocationService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Location  n'existe pas"); //NotFound();

                }
                await _LocationService.DeleteRange(empty);
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
