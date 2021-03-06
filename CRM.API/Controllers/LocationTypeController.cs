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

    public class LocationTypeController : ControllerBase
    {
        public IList<LocationType> LocationTypes;

        private readonly ILocationTypeService _LocationTypeService;

        private readonly IMapper _mapperService;
        public LocationTypeController(ILocationTypeService LocationTypeService, IMapper mapper)
        {
            _LocationTypeService = LocationTypeService;
            _mapperService = mapper;
        }

        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<LocationTypeResource>> ApprouveLocationType(int Id)
        {
            try { 
            var LocationTypeToBeModified = await _LocationTypeService.GetById(Id);
            if (LocationTypeToBeModified == null) return BadRequest("Le LocationType n'existe pas"); //NotFound();
            //var newLocationType = await _LocationTypeService.Create(LocationTypes);
            // LocationTypes.CreatedOn = SaveLocationTypeResource.;
            await _LocationTypeService.Approuve(LocationTypeToBeModified, LocationTypeToBeModified);

            var LocationTypeUpdated = await _LocationTypeService.GetById(Id);

            var LocationTypeResourceUpdated = _mapperService.Map<LocationType, LocationTypeResource>(LocationTypeUpdated);

            return Ok(LocationTypeResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<LocationTypeResource>> RejectLocationType(int Id)
        {
            try { 
            var LocationTypeToBeModified = await _LocationTypeService.GetById(Id);
            if (LocationTypeToBeModified == null) return BadRequest("Le LocationType n'existe pas"); //NotFound();
            //var newLocationType = await _LocationTypeService.Create(LocationTypes);
            // LocationTypes.CreatedOn = SaveLocationTypeResource.;
            LocationTypeToBeModified.UpdatedOn = DateTime.UtcNow;
            await _LocationTypeService.Reject(LocationTypeToBeModified, LocationTypeToBeModified);

            var LocationTypeUpdated = await _LocationTypeService.GetById(Id);

            var LocationTypeResourceUpdated = _mapperService.Map<LocationType, LocationTypeResource>(LocationTypeUpdated);

            return Ok(LocationTypeResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<LocationTypeResource>> CreateLocationType(SaveLocationTypeResource SaveLocationTypeResource)
        {
            try { 
            var Exist = await _LocationTypeService.GetByExistantActif(SaveLocationTypeResource.Name, SaveLocationTypeResource.Type);
            if (Exist == null) { 
            //*** Mappage ***
            var LocationType = _mapperService.Map<SaveLocationTypeResource, LocationType>(SaveLocationTypeResource);
            LocationType.UpdatedOn = DateTime.UtcNow;
            LocationType.CreatedOn = DateTime.UtcNow;

            //*** Creation dans la base de donn??es ***
            var NewLocationType = await _LocationTypeService.Create(LocationType);
            //*** Mappage ***
            var LocationTypeResource = _mapperService.Map<LocationType, LocationTypeResource>(NewLocationType);
            return Ok(LocationTypeResource);
            }
            else
            {
                var genericResult = new { Exist = "Already exists", Location = Exist };
                return Ok(genericResult);
            }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<LocationTypeResource>> GetAllLocationTypes()
        {
            try
            {
                var Employe = await _LocationTypeService.GetAll();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<LocationTypeResource>> GetAllActifLocationTypes()
        {
            try
            {
                var Employe = await _LocationTypeService.GetAllActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<LocationTypeResource>> GetAllInactifLocationTypes()
        {
            try
            {
                var Employe = await _LocationTypeService.GetAllInActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<LocationTypeResource>> GetLocationTypeById(int Id)
        {
            try
            {
                var LocationTypes = await _LocationTypeService.GetById(Id);
                if (LocationTypes == null) return NotFound();
                var LocationTypeRessource = _mapperService.Map<LocationType, LocationTypeResource>(LocationTypes);
                return Ok(LocationTypeRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<LocationTypeResource>> UpdateLocationType(int Id, SaveLocationTypeResource SaveLocationTypeResource)
        {
            try { 
            var LocationTypeToBeModified = await _LocationTypeService.GetById(Id);
            if (LocationTypeToBeModified == null) return BadRequest("Le LocationType n'existe pas"); 
            var LocationType = _mapperService.Map<SaveLocationTypeResource, LocationType>(SaveLocationTypeResource);
            LocationType.UpdatedOn = DateTime.UtcNow;
            LocationType.CreatedOn = LocationTypeToBeModified.CreatedOn;
            await _LocationTypeService.Update(LocationTypeToBeModified, LocationType);

            var LocationTypeUpdated = await _LocationTypeService.GetById(Id);

            var LocationTypeResourceUpdated = _mapperService.Map<LocationType, LocationTypeResource>(LocationTypeUpdated);

            return Ok(LocationTypeResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteLocationType(int Id)
        {
            try
            {

                var sub = await _LocationTypeService.GetById(Id);
                if (sub == null) return BadRequest("Le LocationType  n'existe pas");
                await _LocationTypeService.Delete(sub);
               
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
                List<LocationType> empty = new List<LocationType>();
                foreach (var item in Ids)
                {
                    var sub = await _LocationTypeService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le LocationType  n'existe pas");

                }
                await _LocationTypeService.DeleteRange(empty);
               
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
