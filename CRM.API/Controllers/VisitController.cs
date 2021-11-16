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

    public class VisitController : ControllerBase
    {
        public IList<Visit> Visits;

        private readonly IVisitService _VisitService;

        private readonly IMapper _mapperService;
        public VisitController(IVisitService VisitService, IMapper mapper)
        {
            _VisitService = VisitService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Visit>> CreateVisit(SaveVisitResource SaveVisitResource)
  {     
            //*** Mappage ***
            var Visit = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitResource);
            Visit.UpdatedOn = DateTime.UtcNow;
            Visit.CreatedOn = DateTime.UtcNow;
            //*** Creation dans la base de données ***
            var NewVisit = await _VisitService.Create(Visit);
            //*** Mappage ***
            var VisitResource = _mapperService.Map<Visit, VisitResource>(NewVisit);
            return Ok(VisitResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<VisitResource>> GetAllVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAll();
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
        public async Task<ActionResult<VisitResource>> GetAllActifVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAllActif();
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
        public async Task<ActionResult<VisitResource>> GetAllInactifVisits()
        {
            try
            {
                var Employe = await _VisitService.GetAllInActif();
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
        public async Task<ActionResult<VisitResource>> GetVisitById(int Id)
        {
            try
            {
                var Visits = await _VisitService.GetById(Id);
                if (Visits == null) return NotFound();
                var VisitRessource = _mapperService.Map<Visit, VisitResource>(Visits);
                return Ok(VisitRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Visit>> UpdateVisit(int Id, SaveVisitResource SaveVisitResource)
        {

            var VisitToBeModified = await _VisitService.GetById(Id);
            if (VisitToBeModified == null) return BadRequest("Le Visit n'existe pas"); //NotFound();
            var Visits = _mapperService.Map<SaveVisitResource, Visit>(SaveVisitResource);
            //var newVisit = await _VisitService.Create(Visits);

            await _VisitService.Update(VisitToBeModified, Visits);

            var VisitUpdated = await _VisitService.GetById(Id);

            var VisitResourceUpdated = _mapperService.Map<Visit, VisitResource>(VisitUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVisit(int Id)
        {
            try
            {

                var sub = await _VisitService.GetById(Id);
                if (sub == null) return BadRequest("Le Visit  n'existe pas"); //NotFound();
                await _VisitService.Delete(sub);
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
                List<Visit> empty = new List<Visit>();
                foreach (var item in Ids)
                {
                    var sub = await _VisitService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Visit  n'existe pas"); //NotFound();

                }
                await _VisitService.DeleteRange(empty);
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
