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

    public class AppointementController : ControllerBase
    {
        public IList<Appointement> Appointements;
        private readonly IDoctorService _DoctorService;

        private readonly IAppointementService _AppointementService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IMapper _mapperService;
        public AppointementController(IPharmacyService PharmacyService, IDoctorService DoctorService, IUserService UserService,
                        IAppointementService AppointementService, IMapper mapper)
        {
            _UserService = UserService;
            _DoctorService = DoctorService;
            _PharmacyService = PharmacyService;

            _AppointementService = AppointementService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<AppointementResource>> CreateAppointement([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            SaveAppointementResource SaveAppointementResource)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null) { 
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
            if (exp > DateTime.Now)
            {//*** Mappage ***
                var Appointement = _mapperService.Map<SaveAppointementResource, Appointement>(SaveAppointementResource);
                Appointement.UpdatedOn = DateTime.UtcNow;
                Appointement.CreatedOn = DateTime.UtcNow;
                Appointement.Active = 0;
                    if (Role == "Manager")
                    {
                        Appointement.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Appointement.Status = Status.Pending;
                    }
                    Appointement.UpdatedBy = Id;
                Appointement.CreatedBy = Id;
               var Doctor = await _DoctorService.GetById(Appointement.IdDoctor);
               var Pharmacy = await _PharmacyService.GetById(Appointement.IdPharmacy);
                    var User = await _UserService.GetById(Appointement.IdUser);
                    Appointement.IdUser = User.IdUser;
                    Appointement.User = User;
                    Appointement.VersionsUser = User.Version;
                    Appointement.StatusUser = User.Status;
                    if (Pharmacy != null)
                    {
                        Appointement.IdPharmacy = Pharmacy.IdPharmacy;

                        Appointement.Pharmacy = Pharmacy;
                        Appointement.VersionsPharmacy = Pharmacy.Version;
                        Appointement.StatusPharmacy = Pharmacy.Status;
                        Appointement.Doctor = null;
                        Appointement.VersionsDoctor = null;
                        Appointement.StatusDoctor = null;
                    }
                    if (Doctor != null)
                    {
                        Appointement.IdDoctor = Doctor.IdDoctor;

                        Appointement.Doctor = Doctor;
                        Appointement.VersionsDoctor = Doctor.Version;
                        Appointement.StatusDoctor = Doctor.Status;

                        Appointement.Pharmacy = null;
                        Appointement.VersionsPharmacy = null;
                        Appointement.StatusPharmacy = null;
                    }
                    //*** Creation dans la base de données ***
                    var NewAppointement = await _AppointementService.Create(Appointement);
                //*** Mappage ***
                var AppointementResource = _mapperService.Map<Appointement, AppointementResource>(NewAppointement);
                    var NewAppointementCreated = await _AppointementService.GetById(NewAppointement.IdAppointement);
                    return Ok(NewAppointementCreated);
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
        public async Task<ActionResult<AppointementResource>> GetAllAppointements([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
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
                var Employe = await _AppointementService.GetAll();
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
        public async Task<ActionResult<AppointementResource>> GetAllActifAppointements([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
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
                var Employe = await _AppointementService.GetAllActif();
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
        public async Task<ActionResult<AppointementResource>> GetAllInactifAppointements([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
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
                var Employe = await _AppointementService.GetAllInActif();
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

        [HttpGet("AppointementByUser/{Id}")]
        public async Task<ActionResult<AppointementResource>> GetAppointementByIdUser([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
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

                        var Appointements = await _AppointementService.GetByIdUser(Id);
                        if (Appointements == null) return NotFound();
                        List<AppointementResource> AppointementResources = new List<AppointementResource>();

                        foreach (var item in Appointements)
                        {
                            var Bu = _mapperService.Map<Appointement, AppointementResource>(item);

                            if (Bu != null)
                            {
                                AppointementResources.Add(Bu);
                            }
                        }
                        return Ok(AppointementResources);
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
        [HttpPost("GetById")]
        public async Task<ActionResult<AppointementResource>> GetAppointementById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
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
                var Appointements = await _AppointementService.GetById(Id);
                if (Appointements == null) return NotFound();
                var AppointementRessource = _mapperService.Map<Appointement, AppointementResource>(Appointements);
                return Ok(AppointementRessource);
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
        public async Task<ActionResult<AppointementResource>> UpdateAppointement([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            int Id, SaveAppointementResource SaveAppointementResource)
        {
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var exp = DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (exp > DateTime.Now)
                {
                    var AppointementToBeModified = await _AppointementService.GetById(Id);
            if (AppointementToBeModified == null) return BadRequest("Le Appointement n'existe pas"); //NotFound();
            var Appointement = _mapperService.Map<SaveAppointementResource, Appointement>(SaveAppointementResource);
                    //var newAppointement = await _AppointementService.Create(Appointements);
                    Appointement.UpdatedOn = DateTime.UtcNow;
                    Appointement.CreatedOn = AppointementToBeModified.CreatedOn;
                    Appointement.Active = 0;
                    if (Role == "Manager")
                    {
                        Appointement.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Appointement.Status = Status.Pending;
                    }
                    Appointement.UpdatedBy = Id;
                    Appointement.CreatedBy = AppointementToBeModified.CreatedBy;
                    await _AppointementService.Update(AppointementToBeModified, Appointement);

            var AppointementUpdated = await _AppointementService.GetById(Id);

            var AppointementResourceUpdated = _mapperService.Map<Appointement, AppointementResource>(AppointementUpdated);

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
        public async Task<ActionResult> DeleteAppointement([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
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

                var sub = await _AppointementService.GetById(Id);
                if (sub == null) return BadRequest("Le Appointement  n'existe pas"); //NotFound();
                await _AppointementService.Delete(sub);
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
                List<Appointement> empty = new List<Appointement>();
                foreach (var item in Ids)
                {
                    var sub = await _AppointementService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Appointement  n'existe pas"); //NotFound();

                }
                await _AppointementService.DeleteRange(empty);
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
        public async Task<ActionResult<AppointementResource>> ApprouveAppointement(
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
                    var AppointementToBeModified = await _AppointementService.GetById(Id);
                if (AppointementToBeModified == null) return BadRequest("Le Appointement n'existe pas"); //NotFound();
                                                                                             //var newAppointement = await _AppointementService.Create(Appointements);
                                                                                             // Appointements.CreatedOn = SaveAppointementResource.;
                AppointementToBeModified.UpdatedOn = DateTime.UtcNow;
                AppointementToBeModified.UpdatedBy = IdUser;

                await _AppointementService.Approuve(AppointementToBeModified, AppointementToBeModified);

                var AppointementUpdated = await _AppointementService.GetById(Id);

                var AppointementResourceUpdated = _mapperService.Map<Appointement, AppointementResource>(AppointementUpdated);

                return Ok(AppointementResourceUpdated);
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
        ///  This function is used to Reject a Appointement
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the cycle.</param>
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<AppointementResource>> RejectAppointement([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
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
                    var AppointementToBeModified = await _AppointementService.GetById(Id);
                if (AppointementToBeModified == null) return BadRequest("Le Appointement n'existe pas"); //NotFound();
                                                                                             //var newAppointement = await _AppointementService.Create(Appointements);
                                                                                             // Appointements.CreatedOn = SaveAppointementResource.;
                AppointementToBeModified.UpdatedOn = DateTime.UtcNow;
                AppointementToBeModified.UpdatedBy = IdUser;

                await _AppointementService.Reject(AppointementToBeModified, AppointementToBeModified);

                var AppointementUpdated = await _AppointementService.GetById(Id);

                var AppointementResourceUpdated = _mapperService.Map<Appointement, AppointementResource>(AppointementUpdated);

                return Ok(AppointementResourceUpdated);
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
