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
        public async Task<ActionResult<InfoResource>> CreateInfo(SaveInfoResource SaveInfoResource)
        {
            //*** Mappage ***
            var Info = _mapperService.Map<SaveInfoResource, Info>(SaveInfoResource);
            Info.Version = 0;
            Info.Active = 0;
            Info.CreatedOn = DateTime.UtcNow;
            Info.UpdatedOn = DateTime.UtcNow;
            Info.CreatedBy = 0;
            Info.UpdatedBy = 0;
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
        public async Task<ActionResult<InfoResource>> UpdateInfo(int Id, SaveInfoResource SaveInfoResource)
        {

            var InfoToBeModified = await _InfoService.GetById(Id);
            if (InfoToBeModified == null) return BadRequest("Le Info n'existe pas"); //NotFound();
            var Info = _mapperService.Map<SaveInfoResource, Info>(SaveInfoResource);
            //var newInfo = await _InfoService.Create(Infos);
            Info.Version = InfoToBeModified.Version+1;
            Info.Active = 0;
            Info.CreatedOn = InfoToBeModified.CreatedOn;
            Info.UpdatedOn = DateTime.UtcNow;
            Info.CreatedBy = InfoToBeModified.CreatedBy;
            Info.UpdatedBy = 0;
            await _InfoService.Update(InfoToBeModified, Info);

            var InfoUpdated = await _InfoService.GetById(Id);

            var InfoResourceUpdated = _mapperService.Map<Info, InfoResource>(InfoUpdated);

            return Ok(InfoResourceUpdated);
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
