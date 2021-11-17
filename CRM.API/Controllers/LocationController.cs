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

        private readonly ILocationService _LocationService;
        private readonly ILocationTypeService _LocationTypeService;

        private readonly IMapper _mapperService;
        public LocationController(IServiceService ServiceService,ILocationService LocationService, IUserService UserService, ILocationTypeService LocationTypeService, IMapper mapper)
        {
            _ServiceService = ServiceService;
            _UserService = UserService;
            _LocationTypeService = LocationTypeService;
            _LocationService = LocationService;
            _mapperService = mapper;
        }

        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<Location>> ApprouveLocation(int Id)
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
        public async Task<ActionResult<Location>> RejectLocation(int Id)
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
        public async Task<ActionResult<Location>> CreateLocation(SaveLocationResource SaveLocationResource)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);
            if (token != "")
            {
                var claims = _UserService.getPrincipal(token);
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);

                var LocationExiste = await _LocationService.GetByExistantActif(SaveLocationResource.Name, SaveLocationResource.IdLocationType);
                LocationResource LocationResourceOld = new LocationResource();
                if (LocationExiste == null)
                { //*** Mappage ***
                    var Location = _mapperService.Map<SaveLocationResource, Location>(SaveLocationResource);
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

                    var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.IdLocationType);
                    Location.NameLocationType = NewLocationType.Name;
                    Location.StatusLocationType = NewLocationType.Status;
                    Location.VersionLocationType = NewLocationType.Version;
                    Location.TypeLocationType = NewLocationType.Type;
                    Location.CreatedBy = Id;
                    Location.UpdatedBy = Id;
                    foreach (var item in SaveLocationResource.SaveServiceResource)
                    {
                        var Service = _mapperService.Map<SaveServiceResource, Service>(item);
                        Service.UpdatedOn = DateTime.UtcNow;
                        Service.CreatedOn = DateTime.UtcNow;
                        Service.Version = 0;
                        Service.Active = 0;
                        Service.CreatedBy = Id;
                        Service.UpdatedBy = Id;

                        if (Role == "Manager")
                        {
                            Service.Status = Status.Approuved;
                        }
                        else if (Role == "Delegue")
                        {
                            Service.Status = Status.Pending;
                        }
                    }
                    //*** Creation dans la base de données ***
                    var NewLocation = await _LocationService.Create(Location);
                    //*** Mappage ***
                    var LocationResource = _mapperService.Map<Location, LocationResource>(NewLocation);

                    return Ok(LocationResource);
                }
                else
                {
                    var genericResult = new { Exist = "Already exists", Location = LocationExiste };

                    return Ok(genericResult);
                }
            }
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
            }
        [HttpGet]
        public async Task<ActionResult<LocationResource>> GetAllLocations()
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
        public async Task<ActionResult<LocationResource>> GetAllActifLocations()
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
        public async Task<ActionResult<LocationResource>> GetAllInactifLocations()
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
        public async Task<ActionResult<LocationResource>> GetLocationById(int Id)
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
        public async Task<ActionResult<Location>> UpdateLocation(int Id, SaveLocationResource SaveLocationResource)
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
            var Location = _mapperService.Map<SaveLocationResource, Location>(SaveLocationResource);
            //var newLocation = await _LocationService.Create(Locations);
            Location.UpdatedOn = DateTime.UtcNow;
            Location.CreatedOn = LocationToBeModified.CreatedOn;
          
            Location.CreatedBy = Location.CreatedBy;
            Location.UpdatedBy = IdUser;
            var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.IdLocationType);
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
            else
            {
                ErrorMessag.ErrorMessage = "Empty Token";
                ErrorMessag.StatusCode = 400;
                return Ok(ErrorMessag);

            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteLocation(int Id)
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
        public async Task<ActionResult> DeleteRange(List<int> Ids)
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
