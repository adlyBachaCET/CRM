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

    public class PotentielController : ControllerBase
    {
        public IList<Potentiel> Potentiels;

        private readonly IPotentielService _PotentielService;

        private readonly IMapper _mapperService;
        public PotentielController(IPotentielService PotentielService, IMapper mapper)
        {
            _PotentielService = PotentielService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<PotentielResource>> CreatePotentiel(SavePotentielResource SavePotentielResource)
        {
            try { 
            //*** Mappage ***
            var Potentiel = _mapperService.Map<SavePotentielResource, Potentiel>(SavePotentielResource);
            Potentiel.CreatedOn = DateTime.UtcNow;
            Potentiel.UpdatedOn = DateTime.UtcNow;
            Potentiel.Active = 0;
            Potentiel.Version = 0;
            Potentiel.CreatedBy = 0;
            Potentiel.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewPotentiel = await _PotentielService.Create(Potentiel);
            //*** Mappage ***
            var PotentielResource = _mapperService.Map<Potentiel, PotentielResource>(NewPotentiel);
            return Ok(PotentielResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<ActionResult<PotentielResource>> GetAllPotentiels()
        {
            try
            {
                var Employe = await _PotentielService.GetAll();
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
        public async Task<ActionResult<PotentielResource>> GetAllActifPotentiels()
        {
            try
            {
                var Employe = await _PotentielService.GetAllActif();
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
        public async Task<ActionResult<PotentielResource>> GetAllInactifPotentiels()
        {
            try
            {
                var Employe = await _PotentielService.GetAllInActif();
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
        public async Task<ActionResult<PotentielResource>> GetPotentielById(int Id)
        {
            try
            {
                var Potentiels = await _PotentielService.GetById(Id);
                if (Potentiels == null) return NotFound();
                var PotentielRessource = _mapperService.Map<Potentiel, PotentielResource>(Potentiels);
                return Ok(PotentielRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<PotentielResource>> UpdatePotentiel(int Id, SavePotentielResource SavePotentielResource)
        {
            try { 
            var PotentielToBeModified = await _PotentielService.GetById(Id);
            if (PotentielToBeModified == null) return BadRequest("Le Potentiel n'existe pas"); //NotFound();
            var Potentiels = _mapperService.Map<SavePotentielResource, Potentiel>(SavePotentielResource);
            //var newPotentiel = await _PotentielService.Create(Potentiels);

            await _PotentielService.Update(PotentielToBeModified, Potentiels);

            var PotentielUpdated = await _PotentielService.GetById(Id);

            var PotentielResourceUpdated = _mapperService.Map<Potentiel, PotentielResource>(PotentielUpdated);

            return Ok(PotentielResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePotentiel(int Id)
        {
            try
            {

                var sub = await _PotentielService.GetById(Id);
                if (sub == null) return BadRequest("Le Potentiel  n'existe pas");
                await _PotentielService.Delete(sub);
                
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
                List<Potentiel> empty = new List<Potentiel>();
                foreach (var item in Ids)
                {
                    var sub = await _PotentielService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Potentiel  n'existe pas"); 

                }
                await _PotentielService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
