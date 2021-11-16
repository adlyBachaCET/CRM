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

    public class LocalityController : ControllerBase
    {
        public IList<Locality> Localitys;

        private readonly ILocalityService _LocalityService;

        private readonly IMapper _mapperService;
        public LocalityController(ILocalityService LocalityService, IMapper mapper)
        {
            _LocalityService = LocalityService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Locality>> CreateLocality(SaveLocalityResource SaveLocalityResource)
        {
            //*** Mappage ***
            var Locality = _mapperService.Map<SaveLocalityResource, Locality>(SaveLocalityResource);
            Locality.UpdatedOn = DateTime.UtcNow;
            Locality.CreatedOn = DateTime.UtcNow;
            Locality LocalityParent = new Locality();
            if (SaveLocalityResource.IdParent!=null)
            {
                LocalityParent = await _LocalityService.GetByIdActif(SaveLocalityResource.IdParent);

                Locality.IdParent = LocalityParent.IdLocality;
                Locality.InverseIdParentNavigation.Add(LocalityParent);
                Locality.StatusParent = LocalityParent.Status;
                Locality.VersionParent = LocalityParent.Version;
            }




            //*** Creation dans la base de données ***
            var NewLocality = await _LocalityService.Create(Locality);
            //*** Mappage ***
            var LocalityResource = _mapperService.Map<Locality, LocalityResource>(NewLocality);
            return Ok(LocalityResource);
        }
        [HttpGet]
        public async Task<ActionResult<LocalityResource>> GetAllLocalitys()
        {
            try
            {
                var Employe = await _LocalityService.GetAll();
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
        public async Task<ActionResult<LocalityResource>> GetAllActifLocalitys()
        {
            try
            {
                var Employe = await _LocalityService.GetAllActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("LVL/{Id}")]
        public async Task<ActionResult<LocalityResource>> GetAllActifLocalitysLVL2(int Id)
        {
            try
            {
                var Employe = await _LocalityService.GetAllActifLVL2(Id);
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
               return Ok(Employe);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("LVL")]
        public async Task<ActionResult<LocalityResource>> GetAllActifLocalitysLVL1()
        {
            try
            {
                var Employe = await _LocalityService.GetAllActifLVL1();
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
        public async Task<ActionResult<LocalityResource>> GetAllInactifLocalitys()
        {
            try
            {
                var Employe = await _LocalityService.GetAllInActif();
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
        public async Task<ActionResult<LocalityResource>> GetLocalityById(int Id)
        {
            try
            {
                var Localitys = await _LocalityService.GetById(Id);
                if (Localitys == null) return NotFound();
                var LocalityRessource = _mapperService.Map<Locality, LocalityResource>(Localitys);
                return Ok(LocalityRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Locality>> UpdateLocality(int Id, SaveLocalityResource SaveLocalityResource)
        {

            var LocalityToBeModified = await _LocalityService.GetById(Id);
            if (LocalityToBeModified == null) return BadRequest("Le Locality n'existe pas"); //NotFound();
            var Localitys = _mapperService.Map<SaveLocalityResource, Locality>(SaveLocalityResource);
            //var newLocality = await _LocalityService.Create(Localitys);

            await _LocalityService.Update(LocalityToBeModified, Localitys);

            var LocalityUpdated = await _LocalityService.GetById(Id);

            var LocalityResourceUpdated = _mapperService.Map<Locality, LocalityResource>(LocalityUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteLocality(int Id)
        {
            try
            {

                var sub = await _LocalityService.GetById(Id);
                if (sub == null) return BadRequest("Le Locality  n'existe pas"); //NotFound();
                await _LocalityService.Delete(sub);
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
                List<Locality> empty = new List<Locality>();
                foreach (var item in Ids)
                {
                    var sub = await _LocalityService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Locality  n'existe pas"); //NotFound();

                }
                await _LocalityService.DeleteRange(empty);
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
