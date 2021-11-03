using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        public IList<Establishment> Establishments;

        private readonly IEstablishmentService _EstablishmentService;

        private readonly IMapper _mapperService;
        public EstablishmentController(IEstablishmentService EstablishmentService, IMapper mapper)
        {
            _EstablishmentService = EstablishmentService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Establishment>> CreateEstablishment(SaveEstablishmentResource SaveEstablishmentResource)
        {
            //*** Mappage ***
            var Establishment = _mapperService.Map<SaveEstablishmentResource, Establishment>(SaveEstablishmentResource);
            //*** Creation dans la base de données ***
            var NewEstablishment = await _EstablishmentService.Create(Establishment);
            //*** Mappage ***
            var EstablishmentResource = _mapperService.Map<Establishment, EstablishmentResource>(NewEstablishment);
            return Ok(EstablishmentResource);
        }
        [HttpGet]
        public async Task<ActionResult<EstablishmentResource>> GetAllEstablishments()
        {
            try
            {
                var Employe = await _EstablishmentService.GetAll();
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
        public async Task<ActionResult<EstablishmentResource>> GetAllActifEstablishments()
        {
            try
            {
                var Employe = await _EstablishmentService.GetAllActif();
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
        public async Task<ActionResult<EstablishmentResource>> GetAllInactifEstablishments()
        {
            try
            {
                var Employe = await _EstablishmentService.GetAllInActif();
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
        public async Task<ActionResult<EstablishmentResource>> GetEstablishmentById(int Id)
        {
            try
            {
                var Establishments = await _EstablishmentService.GetById(Id);
                if (Establishments == null) return NotFound();
                var EstablishmentRessource = _mapperService.Map<Establishment, EstablishmentResource>(Establishments);
                return Ok(EstablishmentRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Establishment>> UpdateEstablishment(int Id, SaveEstablishmentResource SaveEstablishmentResource)
        {

            var EstablishmentToBeModified = await _EstablishmentService.GetById(Id);
            if (EstablishmentToBeModified == null) return BadRequest("Le Establishment n'existe pas"); //NotFound();
            var Establishments = _mapperService.Map<SaveEstablishmentResource, Establishment>(SaveEstablishmentResource);
            //var newEstablishment = await _EstablishmentService.Create(Establishments);

            await _EstablishmentService.Update(EstablishmentToBeModified, Establishments);

            var EstablishmentUpdated = await _EstablishmentService.GetById(Id);

            var EstablishmentResourceUpdated = _mapperService.Map<Establishment, EstablishmentResource>(EstablishmentUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteEstablishment(int Id)
        {
            try
            {

                var sub = await _EstablishmentService.GetById(Id);
                if (sub == null) return BadRequest("Le Establishment  n'existe pas"); //NotFound();
                await _EstablishmentService.Delete(sub);
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
                List<Establishment> empty = new List<Establishment>();
                foreach (var item in Ids)
                {
                    var sub = await _EstablishmentService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Establishment  n'existe pas"); //NotFound();

                }
                await _EstablishmentService.DeleteRange(empty);
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
