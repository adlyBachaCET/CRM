using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class SupportController : ControllerBase
    {
        public IList<Support> Supports;

        private readonly ISupportService _SupportService;

        private readonly IMapper _mapperService;
        public SupportController(ISupportService SupportService, IMapper mapper)
        {
            _SupportService = SupportService;
            _mapperService = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SupportResource>> CreateSupport(SaveSupportResource SaveSupportResource)
        {
            //*** Mappage ***
            var Support = _mapperService.Map<SaveSupportResource, Support>(SaveSupportResource);
            Support.CreatedOn = DateTime.UtcNow;
            Support.UpdatedOn = DateTime.UtcNow;
            Support.Active = 0;
            Support.Version = 0;
            Support.CreatedBy = 0;
            Support.UpdatedBy = 0;
            //*** Creation dans la base de données ***
            var NewSupport = await _SupportService.Create(Support);
            //*** Mappage ***
            var SupportResource = _mapperService.Map<Support, SupportResource>(NewSupport);
            return Ok(SupportResource);
        }
        [HttpPost("Send")]
        public async Task<ActionResult> Send(SendMail SendMail)
        {
    
            //*** Creation dans la base de données ***
             await _SupportService.Send(SendMail.Name,SendMail.IdUser);
            //*** Mappage ***
            ///var SupportResource = _mapperService.Map<Support, SupportResource>(NewSupport);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<SupportResource>> GetAllSupports()
        {
            try
            {
                var Employe = await _SupportService.GetAll();
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
        public async Task<ActionResult<SupportResource>> GetAllActifSupports()
        {
            try
            {
                var Employe = await _SupportService.GetAllActif();
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
        public async Task<ActionResult<SupportResource>> GetAllInactifSupports()
        {
            try
            {
                var Employe = await _SupportService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("VerifyToken")]
        public async Task<ActionResult> VerifyToken()
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);

            try
            {
                var Supports = await _SupportService.getPrincipal(token);
                if (Supports == null) return NotFound();
                ErrorMessag.ErrorMessage = "Token Found";
                ErrorMessag.StatusCode = 400;
                return Ok(new{ ErrorHandling = ErrorMessag, Supports= Supports.Claims });
            }
            catch (Exception ex)
            {
                ErrorMessag.ErrorMessage = ex.Message;
                ErrorMessag.StatusCode = 400;
                return BadRequest(ErrorMessag);
            }
        }
       
        [HttpGet("{Id}")]
        public async Task<ActionResult<SupportResource>> GetSupportById(int Id)
        {
            try
            {
                var Supports = await _SupportService.GetById(Id);
                if (Supports == null) return NotFound();
                var SupportRessource = _mapperService.Map<Support, SupportResource>(Supports);
                return Ok(SupportRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<SupportResource>> UpdateSupport(int Id, SaveSupportResource SaveSupportResource)
        {

            var SupportToBeModified = await _SupportService.GetById(Id);
            if (SupportToBeModified == null) return BadRequest("Le Support n'existe pas"); //NotFound();
            var Supports = _mapperService.Map<SaveSupportResource, Support>(SaveSupportResource);
            //var newSupport = await _SupportService.Create(Supports);

            await _SupportService.Update(SupportToBeModified, Supports);

            var SupportUpdated = await _SupportService.GetById(Id);

            var SupportResourceUpdated = _mapperService.Map<Support, SupportResource>(SupportUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteSupport(int Id)
        {
            try
            {

                var sub = await _SupportService.GetById(Id);
                if (sub == null) return BadRequest("Le Support  n'existe pas"); //NotFound();
                await _SupportService.Delete(sub);
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
                List<Support> empty = new List<Support>();
                foreach (var item in Ids)
                {
                    var sub = await _SupportService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Support  n'existe pas"); //NotFound();

                }
                await _SupportService.DeleteRange(empty);
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
