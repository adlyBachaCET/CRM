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
    public class BrickLocalityController : ControllerBase
    {
        public IList<BrickLocality> BrickLocalitys;

        private readonly IBrickLocalityService _BrickLocalityService;

        private readonly IMapper _mapperService;
        public BrickLocalityController(IBrickLocalityService BrickLocalityService, IMapper mapper)
        {
            _BrickLocalityService = BrickLocalityService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BrickLocality>> CreateBrickLocality(SaveBrickLocalityResource SaveBrickLocalityResource)
        {
            //*** Mappage ***
            var BrickLocality = _mapperService.Map<SaveBrickLocalityResource, BrickLocality>(SaveBrickLocalityResource);
            //*** Creation dans la base de données ***
            var NewBrickLocality = await _BrickLocalityService.Create(BrickLocality);
            //*** Mappage ***
            var BrickLocalityResource = _mapperService.Map<BrickLocality, BrickLocalityResource>(NewBrickLocality);
            return Ok(BrickLocalityResource);
        }
        [HttpGet]
        public async Task<ActionResult<BrickLocalityResource>> GetAllBrickLocalitys()
        {
            try
            {
                var Employe = await _BrickLocalityService.GetAll();
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
        public async Task<ActionResult<BrickLocalityResource>> GetAllActifBrickLocalitys()
        {
            try
            {
                var Employe = await _BrickLocalityService.GetAllActif();
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
        public async Task<ActionResult<BrickLocalityResource>> GetAllInactifBrickLocalitys()
        {
            try
            {
                var Employe = await _BrickLocalityService.GetAllInActif();
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
        public async Task<ActionResult<BrickLocalityResource>> GetBrickLocalityById(int Id)
        {
            try
            {
                var BrickLocalitys = await _BrickLocalityService.GetById(Id);
                if (BrickLocalitys == null) return NotFound();
                var BrickLocalityRessource = _mapperService.Map<BrickLocality, BrickLocalityResource>(BrickLocalitys);
                return Ok(BrickLocalityRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]



        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBrickLocality(int Id)
        {
            try
            {

                var sub = await _BrickLocalityService.GetById(Id);
                if (sub == null) return BadRequest("Le BrickLocality  n'existe pas"); //NotFound();
                await _BrickLocalityService.Delete(sub);
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
                List<BrickLocality> empty = new List<BrickLocality>();
                foreach (var item in Ids)
                {
                    var sub = await _BrickLocalityService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le BrickLocality  n'existe pas"); //NotFound();

                }
                await _BrickLocalityService.DeleteRange(empty);
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
