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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MomentJs.Net.Formatters;
namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class PlanificationController : ControllerBase
    {
        public IList<Planification> Planifications;
        
        private readonly IPlanificationService _PlanificationService;
        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public PlanificationController(IUserService UserService,
                        IPlanificationService PlanificationService, IMapper mapper)
        {
            _UserService = UserService;

            _PlanificationService = PlanificationService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<PlanificationResource>> CreatePlanification([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            SavePlanificationResource SavePlanificationResource)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null) { 
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            if (exp > DateTime.Now)
            {//*** Mappage ***
                var Planification = _mapperService.Map<SavePlanificationResource, Planification>(SavePlanificationResource);
                Planification.UpdatedOn = DateTime.UtcNow;
                Planification.CreatedOn = DateTime.UtcNow;
                Planification.Active = 0;
                    if (Role == "Manager")
                    {
                        Planification.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Planification.Status = Status.Pending;
                    }
                    Planification.UpdatedBy = Id;
                Planification.CreatedBy = Id;
                //*** Creation dans la base de données ***
                var NewPlanification = await _PlanificationService.Create(Planification);
                //*** Mappage ***
                var PlanificationResource = _mapperService.Map<Planification, PlanificationResource>(NewPlanification);
                return Ok(PlanificationResource);
            }
            else
            {
                return BadRequest("Session Expired");
            }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpGet]
        public async Task<ActionResult<PlanificationResource>> GetAllPlanifications([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _PlanificationService.GetAll();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<PlanificationResource>> GetAllActifPlanifications([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _PlanificationService.GetAllActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<PlanificationResource>> GetAllInactifPlanifications([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var Id = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _PlanificationService.GetAllInActif();
                if (Employe == null) return NotFound();
                // var EmployeResource = _mapperService.Map<Employe, EmployeResource>(Employe);
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PlanificationResource>> GetPlanificationById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            int Id)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var Iduser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {
                var Planifications = await _PlanificationService.GetById(Id);
                if (Planifications == null) return NotFound();
                var PlanificationRessource = _mapperService.Map<Planification, PlanificationResource>(Planifications);
                return Ok(PlanificationRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<PlanificationResource>> UpdatePlanification([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            int Id, SavePlanificationResource SavePlanificationResource)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    var PlanificationToBeModified = await _PlanificationService.GetById(Id);
            if (PlanificationToBeModified == null) return BadRequest("Le Planification n'existe pas"); //NotFound();
            var Planification = _mapperService.Map<SavePlanificationResource, Planification>(SavePlanificationResource);
                    //var newPlanification = await _PlanificationService.Create(Planifications);
                    Planification.UpdatedOn = DateTime.UtcNow;
                    Planification.CreatedOn = PlanificationToBeModified.CreatedOn;
                    Planification.Active = 0;
                    if (Role == "Manager")
                    {
                        Planification.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Planification.Status = Status.Pending;
                    }
                    Planification.UpdatedBy = Id;
                    Planification.CreatedBy = PlanificationToBeModified.CreatedBy;
                    await _PlanificationService.Update(PlanificationToBeModified, Planification);

            var PlanificationUpdated = await _PlanificationService.GetById(Id);

            var PlanificationResourceUpdated = _mapperService.Map<Planification, PlanificationResource>(PlanificationUpdated);

            return Ok();
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePlanification([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {

                var sub = await _PlanificationService.GetById(Id);
                if (sub == null) return BadRequest("Le Planification  n'existe pas"); //NotFound();
                await _PlanificationService.Delete(sub);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange(
            [FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, List<int> Ids)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    try
            {
                List<Planification> empty = new List<Planification>();
                foreach (var item in Ids)
                {
                    var sub = await _PlanificationService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Planification  n'existe pas"); //NotFound();

                }
                await _PlanificationService.DeleteRange(empty);
                ;
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<PlanificationResource>> ApprouvePlanification(
            [FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
            int Id)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    var PlanificationToBeModified = await _PlanificationService.GetById(Id);
                if (PlanificationToBeModified == null) return BadRequest("Le Planification n'existe pas"); //NotFound();
                                                                                             //var newPlanification = await _PlanificationService.Create(Planifications);
                                                                                             // Planifications.CreatedOn = SavePlanificationResource.;
                PlanificationToBeModified.UpdatedOn = DateTime.UtcNow;
                PlanificationToBeModified.UpdatedBy = IdUser;

                await _PlanificationService.Approuve(PlanificationToBeModified, PlanificationToBeModified);

                var PlanificationUpdated = await _PlanificationService.GetById(Id);

                var PlanificationResourceUpdated = _mapperService.Map<Planification, PlanificationResource>(PlanificationUpdated);

                return Ok(PlanificationResourceUpdated);
            }
       
            
            else
            {
                return BadRequest("Session Expired");
            }

        }
            else
            {
                return BadRequest("Bad Token");
    }
}
        /// <summary>
        ///  This function is used to Reject a Planification
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the cycle.</param>
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<PlanificationResource>> RejectPlanification([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {


          //  var dateFormatVal1 = moment().format('DD-MMM-YYYY');


            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    //var actual = new Moment(true) { Year = 1995, Month = 12, Day = 25 }.DateTime();
                    var PlanificationToBeModified = await _PlanificationService.GetById(Id);
                if (PlanificationToBeModified == null) return BadRequest("Le Planification n'existe pas"); //NotFound();
                                                                                             //var newPlanification = await _PlanificationService.Create(Planifications);
                                                                                             // Planifications.CreatedOn = SavePlanificationResource.;
                PlanificationToBeModified.UpdatedOn = DateTime.UtcNow;
                PlanificationToBeModified.UpdatedBy = IdUser;

                await _PlanificationService.Reject(PlanificationToBeModified, PlanificationToBeModified);

                var PlanificationUpdated = await _PlanificationService.GetById(Id);

                var PlanificationResourceUpdated = _mapperService.Map<Planification, PlanificationResource>(PlanificationUpdated);

                return Ok(PlanificationResourceUpdated);
            }
            else
                {
                    return BadRequest("Session Expired");
                }

            }
            else
            {
                return BadRequest("Bad Token");
            }
        }

    }
}
