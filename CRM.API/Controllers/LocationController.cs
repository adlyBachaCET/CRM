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

    public class LocationController : ControllerBase
    {
        public IList<Location> Locations;

        private readonly ILocationService _LocationService;
        private readonly ILocationTypeService _LocationTypeService;

        private readonly IMapper _mapperService;
        public LocationController(ILocationService LocationService,ILocationTypeService LocationTypeService, IMapper mapper)
        {
            _LocationTypeService = LocationTypeService;
               _LocationService = LocationService;
            _mapperService = mapper;
        }

        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<Location>> ApprouveLocation(int Id)
        {

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            //var newLocation = await _LocationService.Create(Locations);
            // Locations.CreatedOn = SaveLocationResource.;
            await _LocationService.Approuve(LocationToBeModified, LocationToBeModified);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok(LocationResourceUpdated);
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<Location>> RejectLocation(int Id)
        {

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            //var newLocation = await _LocationService.Create(Locations);
            // Locations.CreatedOn = SaveLocationResource.;
            LocationToBeModified.UpdatedOn = DateTime.UtcNow;
            await _LocationService.Reject(LocationToBeModified, LocationToBeModified);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok(LocationResourceUpdated);
        }
        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(SaveLocationResource SaveLocationResource)
        {
            var LocationExiste = await _LocationService.GetByExistantActif(SaveLocationResource.Name, SaveLocationResource.IdLocationType);
            LocationResource LocationResourceOld = new LocationResource();
            if (LocationExiste == null)
            { //*** Mappage ***
                var Location = _mapperService.Map<SaveLocationResource, Location>(SaveLocationResource);
                Location.UpdatedOn = DateTime.UtcNow;
                Location.CreatedOn = DateTime.UtcNow;
                var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.IdLocationType);
                Location.NameLocationType = NewLocationType.Name;
                Location.StatusLocationType = NewLocationType.Status;
                Location.VersionLocationType = NewLocationType.Version;
                Location.TypeLocationType = NewLocationType.Type;

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

            var LocationToBeModified = await _LocationService.GetById(Id);
            if (LocationToBeModified == null) return BadRequest("Le Location n'existe pas"); //NotFound();
            var Location = _mapperService.Map<SaveLocationResource, Location>(SaveLocationResource);
            //var newLocation = await _LocationService.Create(Locations);
            Location.UpdatedOn = DateTime.UtcNow;
            Location.CreatedOn = LocationToBeModified.CreatedOn;
            var NewLocationType = await _LocationTypeService.GetById(SaveLocationResource.IdLocationType);
            Location.NameLocationType = NewLocationType.Name;
            Location.StatusLocationType = NewLocationType.Status;
            Location.VersionLocationType = NewLocationType.Version;
            Location.TypeLocationType = NewLocationType.Type;
            await _LocationService.Update(LocationToBeModified, Location);

            var LocationUpdated = await _LocationService.GetById(Id);

            var LocationResourceUpdated = _mapperService.Map<Location, LocationResource>(LocationUpdated);

            return Ok();
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
