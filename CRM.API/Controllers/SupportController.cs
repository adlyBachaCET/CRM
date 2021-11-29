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

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class SupportController : ControllerBase
    {
        public IList<Support> Supports;

        private readonly ISupportService _SupportService;
        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public SupportController(IUserService UserService, ISupportService SupportService, IMapper mapper)
        {
            _UserService = UserService;

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
            //  var UserExist =await _UserService 
            var mail = await _SupportService.Send(SendMail.Name, SendMail.EmailLogin);
            //*** Creation dans la base de données ***
           if ( mail ==true)
            {
                return Ok("Mail envoyé");   
            }
            else
            {
                return NotFound("L'utilisateur n'est pas trouvé");
            }
            //*** Mappage ***
            ///var SupportResource = _mapperService.Map<Support, SupportResource>(NewSupport);
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
        [HttpGet("VerifyToken/{token}")]
        public async Task<ActionResult> VerifyToken(string token)
        {
           // StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
          //  Request.Headers.TryGetValue("token", out token);

            try
            {
                var Supports = await _SupportService.getPrincipal(token);
                if (Supports == null) return NotFound();
                ErrorMessag.ErrorMessage = "Token Found";
                ErrorMessag.StatusCode = 200;
                return Ok(new{ ErrorHandling = ErrorMessag, Supports= Supports.Claims });
            }
            catch (Exception ex)
            {
                ErrorMessag.ErrorMessage = ex.Message;
                ErrorMessag.StatusCode = 404;
                return BadRequest(ErrorMessag);
            }
        }
        [HttpPost("SendMobile")]
        public async Task<ActionResult> SendMobile(SendMailHtml SendMailHtml)
        {
            //  var UserExist =await _UserService 
            var mail = await _SupportService.SendMailMobile(SendMailHtml.Name, SendMailHtml.EmailLogin, SendMailHtml.Html);
            //*** Creation dans la base de données ***
            if (mail == true)
            {
                return Ok("Mail envoyé");
            }
            else
            {
                return NotFound("L'utilisateur n'est pas trouvé");
            }
            //*** Mappage ***
            ///var SupportResource = _mapperService.Map<Support, SupportResource>(NewSupport);
        }
        [HttpPost("VerifyMobile")]
        public async Task<ActionResult> VerifyToken([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
           EmailLoginPassword EmailLoginPassword)
        {
            // StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            //  Request.Headers.TryGetValue("token", out token);

            try
            {
                var Supports = await _SupportService.getPrincipalByEmailLogin(EmailLoginPassword.LoginEmail);

                if (Supports == null) return NotFound();
                else
                {
                    var Password = Supports.FindFirst("Password").Value;
                    if(Password== EmailLoginPassword.ConfirmPassword) {
                        //Response.Headers.Add("Token", Token);
                        return Ok(new { Token= Token, Supports = Supports.Claims });
                    }
                    else
                    {
                        return BadRequest("Passord missmatch");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessag.ErrorMessage = ex.Message;
                ErrorMessag.StatusCode = 404;
                return BadRequest(ErrorMessag);
            }
        }
        [HttpPut("UpdateForgottenPassword")]
        public async Task<ActionResult> UpdateForgottenPassword([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
           TokenPassword TokenPassword)
        {
            ErrorHandling ErrorMessag = new ErrorHandling();

           
                var Claims = await _SupportService.getPrincipal(Token);

                if (Claims == null)
                {
                    return NotFound();
                }
                else
                {
                    var Id = int.Parse(Claims.FindFirst("Id").Value);

                    await _UserService.UpdateGeneratedPassword(Id, TokenPassword.NewPassword);

                    ErrorMessag.ErrorMessage = "Token Found";
                    ErrorMessag.StatusCode = 200;
                    return Ok(new { ErrorHandling = ErrorMessag, Supports = Claims.Claims });

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
            var Support = _mapperService.Map<SaveSupportResource, Support>(SaveSupportResource);
            //var newSupport = await _SupportService.Create(Supports);
            Support.UpdatedOn = DateTime.UtcNow;
            Support.CreatedOn = SupportToBeModified.CreatedOn;
            Support.Active = 0;
            Support.Status = 0;
            Support.UpdatedBy = 0;
            Support.CreatedBy = SupportToBeModified.CreatedBy;
            await _SupportService.Update(SupportToBeModified, Support);

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
