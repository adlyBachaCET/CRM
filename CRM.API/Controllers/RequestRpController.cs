using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class RequestRpController : ControllerBase
    {
        public IList<RequestRp> RequestRps;

        private readonly IRequestRpService _RequestRpService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IParticipantService _ParticipantService;


        private readonly IMapper _mapperService;
        public RequestRpController(IParticipantService ParticipantService,
            IUserService UserService,
            IDoctorService DoctorService,
            IRequestRpService RequestRpService, IMapper mapper)
        {
            _UserService = UserService;
            _DoctorService = DoctorService;
            _ParticipantService = ParticipantService;

            _RequestRpService = RequestRpService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<RequestRpResource>> CreateRequestRp([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
         SaveRequestRpResource SaveRequestRpResource)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                if (claims.Claims != null)
                {
                    var Role = claims.FindFirst("Role").Value;
                    var IdUser = int.Parse(claims.FindFirst("Id").Value);
                    var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                    if (exp > DateTime.Now)
                    {
                        //*** Mappage ***
                        var RequestRp = _mapperService.Map<SaveRequestRpResource, RequestRp>(SaveRequestRpResource);
                        RequestRp.UpdatedOn = DateTime.UtcNow;
                        RequestRp.CreatedOn = DateTime.UtcNow;
                        RequestRp.Active = 0;
                        RequestRp.Status = 0;
                        RequestRp.CreatedBy = IdUser;
                        RequestRp.UpdatedBy = IdUser;
                        if (Role == "Manager")
                        {
                            RequestRp.Status = Status.Approuved;
                        }
                        else if (Role == "Delegue")
                        {
                            RequestRp.Status = Status.Pending;
                        }
                        //*** Creation dans la base de données ***
                        var NewRequestRp = await _RequestRpService.Create(RequestRp);
                        //*** Mappage ***
                        var RequestRpResource = _mapperService.Map<RequestRp, RequestRpResource>(NewRequestRp);
                        return Ok(RequestRpResource);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllRequestRpsByUser/{Id}")]
        public async Task<ActionResult> GetAllRequestRpsByUser([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                if (claims.Claims != null)
                {
                    var Role = claims.FindFirst("Role").Value;
                    var IdUser = int.Parse(claims.FindFirst("Id").Value);
                    var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                    if (exp > DateTime.Now)
                    {
                        var Participant = await _ParticipantService.GetAllById(Id);
                        List<RequestRp> RequestRpList = new List<RequestRp>();
                        foreach (var item in Participant)
                        {
                            var RequestRp = await _RequestRpService.GetById(item.IdRequestRp);
                            RequestRpList.Add(RequestRp);
                        }
                        List<RequestRpResource> RequestRpResources = new List<RequestRpResource>();
                        foreach (var item in RequestRpList)
                        {
                            var NewRequestRp = _mapperService.Map<RequestRp, RequestRpResource>(item);

                            if (NewRequestRp != null)
                            {
                                RequestRpResources.Add(NewRequestRp);

                            }
                        }
                        return Ok(RequestRpResources);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<RequestRpResource>> GetAllRequestRps([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
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
                            var Employe = await _RequestRpService.GetAll();
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<RequestRpResource>> GetAllActifRequestRps([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token
        )
        {
            try
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
                            var Employe = await _RequestRpService.GetAllActif();
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<RequestRpResource>> GetAllInactifRequestRps([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try
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
                            var Employe = await _RequestRpService.GetAllInActif();
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<RequestRpResource>> GetRequestRpById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
        int Id)
        {
            try
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
                            var RequestRps = await _RequestRpService.GetById(Id);
                            if (RequestRps == null) return NotFound();
                            var RequestRpRessource = _mapperService.Map<RequestRp, RequestRpResource>(RequestRps);
                            return Ok(RequestRpRessource);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<RequestRpResource>> UpdateRequestRp([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
        int Id, SaveRequestRpResource SaveRequestRpResource)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    var RequestRpToBeModified = await _RequestRpService.GetById(Id);
                    if (RequestRpToBeModified == null) return BadRequest("Le RequestRp n'existe pas"); //NotFound();
                    var RequestRp = _mapperService.Map<SaveRequestRpResource, RequestRp>(SaveRequestRpResource);
                    //var newRequestRp = await _RequestRpService.Create(RequestRps);
                    RequestRp.UpdatedOn = DateTime.UtcNow;
                    RequestRp.CreatedOn = RequestRpToBeModified.CreatedOn;
                    RequestRp.Active = 0;
                    RequestRp.Status = 0;
                    RequestRp.UpdatedBy = IdUser;
                    RequestRp.CreatedBy = RequestRpToBeModified.CreatedBy;
                    if (Role == "Manager")
                    {
                        RequestRp.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        RequestRp.Status = Status.Pending;
                    }
                    await _RequestRpService.Update(RequestRpToBeModified, RequestRp);

                    var RequestRpUpdated = await _RequestRpService.GetById(Id);

                    var RequestRpResourceUpdated = _mapperService.Map<RequestRp, RequestRpResource>(RequestRpUpdated);

                    return Ok(RequestRpResourceUpdated);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteRequestRp([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
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
                    try
                    {

                        var sub = await _RequestRpService.GetById(Id);
                        if (sub == null) return BadRequest("Le RequestRp  n'existe pas"); //NotFound();
                        await _RequestRpService.Delete(sub);
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
        public async Task<ActionResult> DeleteRange([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
        List<int> Ids)
        {
            try { 
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
                        List<RequestRp> empty = new List<RequestRp>();
                        foreach (var item in Ids)
                        {
                            var sub = await _RequestRpService.GetById(item);
                            empty.Add(sub);
                            if (sub == null) return BadRequest("Le RequestRp  n'existe pas"); //NotFound();

                        }
                        await _RequestRpService.DeleteRange(empty);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Approuve/{Id}")]
        public async Task<ActionResult<RequestRpResource>> ApprouveRequestRp(
         [FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
         int Id)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    var RequestRpToBeModified = await _RequestRpService.GetById(Id);
                    if (RequestRpToBeModified == null) return BadRequest("Le RequestRp n'existe pas"); //NotFound();
                                                                                                       //var newRequestRp = await _RequestRpService.Create(RequestRps);
                                                                                                       // RequestRps.CreatedOn = SaveRequestRpResource.;
                    RequestRpToBeModified.UpdatedOn = DateTime.UtcNow;
                    RequestRpToBeModified.UpdatedBy = IdUser;

                    await _RequestRpService.Approuve(RequestRpToBeModified, RequestRpToBeModified);

                    var RequestRpUpdated = await _RequestRpService.GetById(Id);

                    var RequestRpResourceUpdated = _mapperService.Map<RequestRp, RequestRpResource>(RequestRpUpdated);

                    return Ok(RequestRpResourceUpdated);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  This function is used to Reject a RequestRp
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the cycle.</param>
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<RequestRpResource>> RejectRequestRp([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try { 

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
                    var RequestRpToBeModified = await _RequestRpService.GetById(Id);
                    if (RequestRpToBeModified == null) return BadRequest("Le RequestRp n'existe pas"); //NotFound();
                                                                                                       //var newRequestRp = await _RequestRpService.Create(RequestRps);
                                                                                                       // RequestRps.CreatedOn = SaveRequestRpResource.;
                    RequestRpToBeModified.UpdatedOn = DateTime.UtcNow;
                    RequestRpToBeModified.UpdatedBy = IdUser;

                    await _RequestRpService.Reject(RequestRpToBeModified, RequestRpToBeModified);

                    var RequestRpUpdated = await _RequestRpService.GetById(Id);

                    var RequestRpResourceUpdated = _mapperService.Map<RequestRp, RequestRpResource>(RequestRpUpdated);

                    return Ok(RequestRpResourceUpdated);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
