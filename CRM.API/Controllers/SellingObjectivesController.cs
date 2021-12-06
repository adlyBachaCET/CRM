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

    public class SellingObjectivesController : ControllerBase
    {
        public IList<SellingObjectives> SellingObjectivess;

        private readonly ISellingObjectivesService _SellingObjectivesService;

        private readonly IMapper _mapperService;
        public SellingObjectivesController(ISellingObjectivesService SellingObjectivesService, IMapper mapper)
        {
            _SellingObjectivesService = SellingObjectivesService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<SellingObjectivesResource>> CreateSellingObjectives(SaveSellingObjectivesResource SaveSellingObjectivesResource)
        {
            try { 
            //*** Mappage ***
            var SellingObjectives = _mapperService.Map<SaveSellingObjectivesResource, SellingObjectives>(SaveSellingObjectivesResource);
            SellingObjectives.CreatedOn = DateTime.UtcNow;
            SellingObjectives.UpdatedOn = DateTime.UtcNow;
            SellingObjectives.Active = 0;
            SellingObjectives.Version = 0;
            SellingObjectives.CreatedBy = 0;
            SellingObjectives.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewSellingObjectives = await _SellingObjectivesService.Create(SellingObjectives);
            //*** Mappage ***
            var SellingObjectivesResource = _mapperService.Map<SellingObjectives, SellingObjectivesResource>(NewSellingObjectives);
            return Ok(SellingObjectivesResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<ActionResult<SellingObjectivesResource>> GetAllSellingObjectivess()
        {
            try
            {
                var Employe = await _SellingObjectivesService.GetAll();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<SellingObjectivesResource>> GetAllActifSellingObjectivess()
        {
            try
            {
                var Employe = await _SellingObjectivesService.GetAllActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<SellingObjectivesResource>> GetAllInactifSellingObjectivess()
        {
            try
            {
                var Employe = await _SellingObjectivesService.GetAllInActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SellingObjectivesResource>> GetSellingObjectivesById(int Id)
        {
            try
            {
                var SellingObjectivess = await _SellingObjectivesService.GetById(Id);
                if (SellingObjectivess == null) return NotFound();
                var SellingObjectivesRessource = _mapperService.Map<SellingObjectives, SellingObjectivesResource>(SellingObjectivess);
                return Ok(SellingObjectivesRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<SellingObjectivesResource>> UpdateSellingObjectives(int Id, SaveSellingObjectivesResource SaveSellingObjectivesResource)
        {
            try { 
            var SellingObjectivesToBeModified = await _SellingObjectivesService.GetById(Id);
            if (SellingObjectivesToBeModified == null) return BadRequest("Le SellingObjectives n'existe pas");
            var SellingObjectivess = _mapperService.Map<SaveSellingObjectivesResource, SellingObjectives>(SaveSellingObjectivesResource);

            await _SellingObjectivesService.Update(SellingObjectivesToBeModified, SellingObjectivess);

            var SellingObjectivesUpdated = await _SellingObjectivesService.GetById(Id);

            var SellingObjectivesResourceUpdated = _mapperService.Map<SellingObjectives, SellingObjectivesResource>(SellingObjectivesUpdated);

            return Ok(SellingObjectivesResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteSellingObjectives(int Id)
        {
            try
            {

                var sub = await _SellingObjectivesService.GetById(Id);
                if (sub == null) return BadRequest("Le SellingObjectives  n'existe pas");
                await _SellingObjectivesService.Delete(sub);
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
                List<SellingObjectives> empty = new List<SellingObjectives>();
                foreach (var item in Ids)
                {
                    var sub = await _SellingObjectivesService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le SellingObjectives  n'existe pas");
                }
                await _SellingObjectivesService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
