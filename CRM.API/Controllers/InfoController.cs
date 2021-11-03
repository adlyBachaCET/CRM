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
    public class InfoController : ControllerBase
    {
        public IList<Info> Infos;

        private readonly IInfoService _InfoService;

        private readonly IMapper _mapperService;
        public InfoController(IInfoService InfoService, IMapper mapper)
        {
            _InfoService = InfoService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Info>> CreateInfo(SaveInfoResource SaveInfoResource)
        {
            //*** Mappage ***
            var Info = _mapperService.Map<SaveInfoResource, Info>(SaveInfoResource);
            //*** Creation dans la base de données ***
            var NewInfo = await _InfoService.Create(Info);
            //*** Mappage ***
            var InfoResource = _mapperService.Map<Info, InfoResource>(NewInfo);
            return Ok(InfoResource);
        }
        [HttpGet]
        public async Task<ActionResult<InfoResource>> GetAllInfos()
        {
            try
            {
                var Employe = await _InfoService.GetAll();
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
        public async Task<ActionResult<InfoResource>> GetAllActifInfos()
        {
            try
            {
                var Employe = await _InfoService.GetAllActif();
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
        public async Task<ActionResult<InfoResource>> GetAllInactifInfos()
        {
            try
            {
                var Employe = await _InfoService.GetAllInActif();
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
        public async Task<ActionResult<InfoResource>> GetInfoById(int Id)
        {
            try
            {
                var Infos = await _InfoService.GetById(Id);
                if (Infos == null) return NotFound();
                var InfoRessource = _mapperService.Map<Info, InfoResource>(Infos);
                return Ok(InfoRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Info>> UpdateInfo(int Id, SaveInfoResource SaveInfoResource)
        {

            var InfoToBeModified = await _InfoService.GetById(Id);
            if (InfoToBeModified == null) return BadRequest("Le Info n'existe pas"); //NotFound();
            var Infos = _mapperService.Map<SaveInfoResource, Info>(SaveInfoResource);
            //var newInfo = await _InfoService.Create(Infos);

            await _InfoService.Update(InfoToBeModified, Infos);

            var InfoUpdated = await _InfoService.GetById(Id);

            var InfoResourceUpdated = _mapperService.Map<Info, InfoResource>(InfoUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteInfo(int Id)
        {
            try
            {

                var sub = await _InfoService.GetById(Id);
                if (sub == null) return BadRequest("Le Info  n'existe pas"); //NotFound();
                await _InfoService.Delete(sub);
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
                List<Info> empty = new List<Info>();
                foreach (var item in Ids)
                {
                    var sub = await _InfoService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Info  n'existe pas"); //NotFound();

                }
                await _InfoService.DeleteRange(empty);
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
