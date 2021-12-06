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

    public class ActivityController : ControllerBase
    {
        
        private readonly IActivityService _ActivityService;
        private readonly IUserService _UserService;
        private readonly IActivityUserService _ActivityUserService;

        private readonly IMapper _mapperService;
        public ActivityController(IUserService UserService,
                        IActivityService ActivityService, IActivityUserService ActivityUserService, IMapper mapper)
        {
            _UserService = UserService;
            _ActivityUserService = ActivityUserService;

            _ActivityService = ActivityService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ActivityResource>> CreateActivity([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            SaveActivityResource SaveActivityResource)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null) { 
            var Role = claims.FindFirst("Role").Value;
            var Id = int.Parse(claims.FindFirst("Id").Value);
            var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
            if (Exp > DateTime.Now)
            {//*** Mappage ***
                var Activity = _mapperService.Map<SaveActivityResource, Activity>(SaveActivityResource);
                Activity.UpdatedOn = DateTime.UtcNow;
                Activity.CreatedOn = DateTime.UtcNow;
                Activity.Active = 0;
                    if (Role == "Manager")
                    {
                        Activity.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Activity.Status = Status.Pending;
                    }
                    Activity.UpdatedBy = Id;
                Activity.CreatedBy = Id;
                //*** Creation dans la base de données ***
                var NewActivity = await _ActivityService.Create(Activity);
                //*** Mappage ***
                var ActivityResource = _mapperService.Map<Activity, ActivityResource>(NewActivity);
                    var ActivityUser = new ActivityUser();
                    var User = await _UserService.GetById(Id);
                    ActivityUser.IdUser = User.IdUser;
                    ActivityUser.StatusUser = User.Status;
                    ActivityUser.VersionUser = User.Version;
                    ActivityUser.User = User;
                    ActivityUser.UpdatedOn = User.UpdatedOn;
                    ActivityUser.CreatedOn = User.CreatedOn;
                    ActivityUser.UpdatedBy = User.UpdatedBy;
                    var ActivityCreated = await _ActivityService.GetById(ActivityResource.IdActivity);
                    ActivityUser.IdUser = ActivityCreated.IdActivity;
                    ActivityUser.StatusUser = ActivityCreated.Status;
                    ActivityUser.VersionUser = ActivityCreated.Version;
                    ActivityUser.Activity = ActivityCreated;
                    ActivityUser.UpdatedOn = ActivityCreated.UpdatedOn;
                    ActivityUser.CreatedOn = ActivityCreated.CreatedOn;
                    ActivityUser.UpdatedBy = ActivityCreated.UpdatedBy;
                    //*** Creation dans la base de données ***
                    ActivityUser.CreatedBy = 0;
                    ActivityUser.UpdatedBy = 0;
                    await _ActivityUserService.Create(ActivityUser);
                    return Ok(ActivityResource);
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
        public async Task<ActionResult<ActivityResource>> GetAllActivitys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try{
                var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {

                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _ActivityService.GetAll();
                if (Employe == null) return NotFound();
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
        public async Task<ActionResult<ActivityResource>> GetAllActifActivitys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {

                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _ActivityService.GetAllActif();
                if (Employe == null) return NotFound();
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
        public async Task<ActionResult<ActivityResource>> GetAllInactifActivitys([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {

                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {
                var Employe = await _ActivityService.GetAllInActif();
                if (Employe == null) return NotFound();
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

        [HttpGet("ActivityByUser/{Id}")]
        public async Task<ActionResult<ActivityResource>> GetActivityByIdUser([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
            int Id)
        {
            try { 
            
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {

                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
                    {

                        var Activitys = await _ActivityService.GetByIdUser(Id);
                        if (Activitys == null) return NotFound();
                        List<ActivityResource> ActivityResources = new List<ActivityResource>();

                        foreach (var item in Activitys)
                        {
                            var Activity = _mapperService.Map<Activity, ActivityResource>(item);

                            if (Activity != null)
                            {
                                ActivityResources.Add(Activity);
                            }
                        }
                        return Ok(ActivityResources);
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
        [HttpGet("ActivityByUserToday/{Id}")]
        public async Task<ActionResult<ActivityResource>> GetActivityByIdUserToday([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
           int Id)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {

                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
                    {

                        var Activitys = await _ActivityService.GetByIdUserByToday(Id);
                        if (Activitys == null) return NotFound();
                        List<ActivityResource> ActivityResources = new List<ActivityResource>();

                        foreach (var item in Activitys)
                        {
                            var Bu = _mapperService.Map<Activity, ActivityResource>(item);

                            if (Bu != null)
                            {
                                ActivityResources.Add(Bu);
                            }
                        }
                        return Ok(ActivityResources);
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
        public async Task<ActionResult<ActivityResource>> GetActivityById([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            int Id)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
    
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {
                var Activitys = await _ActivityService.GetById(Id);
                if (Activitys == null) return NotFound();
                var ActivityRessource = _mapperService.Map<Activity, ActivityResource>(Activitys);
                return Ok(ActivityRessource);
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
        public async Task<ActionResult<ActivityResource>> UpdateActivity([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, 
            int Id, SaveActivityResource SaveActivityResource)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var Role = claims.FindFirst("Role").Value;
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    var ActivityToBeModified = await _ActivityService.GetById(Id);
            if (ActivityToBeModified == null) return BadRequest("Le Activity n'existe pas"); //NotFound();
            var Activity = _mapperService.Map<SaveActivityResource, Activity>(SaveActivityResource);
                    //var newActivity = await _ActivityService.Create(Activitys);
                    Activity.UpdatedOn = DateTime.UtcNow;
                    Activity.CreatedOn = ActivityToBeModified.CreatedOn;
                    Activity.Active = 0;
                    if (Role == "Manager")
                    {
                        Activity.Status = Status.Approuved;
                    }
                    else if (Role == "Delegue")
                    {
                        Activity.Status = Status.Pending;
                    }
                    Activity.UpdatedBy = IdUser;
                    Activity.CreatedBy = ActivityToBeModified.CreatedBy;
                    await _ActivityService.Update(ActivityToBeModified, Activity);

            var ActivityUpdated = await _ActivityService.GetById(Id);

            var ActivityResourceUpdated = _mapperService.Map<Activity, ActivityResource>(ActivityUpdated);

            return Ok(ActivityResourceUpdated);
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
        public async Task<ActionResult> DeleteActivity([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {

                var sub = await _ActivityService.GetById(Id);
                if (sub == null) return BadRequest("Le Activity  n'existe pas"); //NotFound();
                await _ActivityService.Delete(sub);
                
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
        [HttpPost("DeleteRange")]
        public async Task<ActionResult> DeleteRange(
            [FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, List<int> Ids)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
              
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    try
            {
                List<Activity> empty = new List<Activity>();
                foreach (var item in Ids)
                {
                    var sub = await _ActivityService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Activity  n'existe pas"); //NotFound();

                }
                await _ActivityService.DeleteRange(empty);
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
        public async Task<ActionResult<ActivityResource>> ApprouveActivity(
            [FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token,
            int Id)
        {
            try { 
            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    var ActivityToBeModified = await _ActivityService.GetById(Id);
                if (ActivityToBeModified == null) return BadRequest("Le Activity n'existe pas"); //NotFound();
                                                                                             //var newActivity = await _ActivityService.Create(Activitys);
                                                                                             // Activitys.CreatedOn = SaveActivityResource.;
                ActivityToBeModified.UpdatedOn = DateTime.UtcNow;
                ActivityToBeModified.UpdatedBy = IdUser;

                await _ActivityService.Approuve(ActivityToBeModified, ActivityToBeModified);

                var ActivityUpdated = await _ActivityService.GetById(Id);

                var ActivityResourceUpdated = _mapperService.Map<Activity, ActivityResource>(ActivityUpdated);

                return Ok(ActivityResourceUpdated);
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
        ///  This function is used to Reject a Activity
        /// </summary>
        ///<param name="Token">Token of the connected user to be passed in the header.</param>
        /// <param name="Id">Id of the cycle.</param>
        [HttpPut("Reject/{Id}")]
        public async Task<ActionResult<ActivityResource>> RejectActivity([FromHeader(Name = "Token")][Required(ErrorMessage = "Token is required")] string Token, int Id)
        {

            try { 
          //  var dateFormatVal1 = moment().format('DD-MMM-YYYY');


            var claims = _UserService.getPrincipal(Token);
            if (claims.Claims != null)
            {
                var IdUser = int.Parse(claims.FindFirst("Id").Value);
                var Exp =  DateTime.Parse(claims.FindFirst("Exipres On").Value);
                if (Exp > DateTime.Now)
                {
                    //var actual = new Moment(true) { Year = 1995, Month = 12, Day = 25 }.DateTime();
                    var ActivityToBeModified = await _ActivityService.GetById(Id);
                if (ActivityToBeModified == null) return BadRequest("Le Activity n'existe pas"); //NotFound();
                                                                                             //var newActivity = await _ActivityService.Create(Activitys);
                                                                                             // Activitys.CreatedOn = SaveActivityResource.;
                ActivityToBeModified.UpdatedOn = DateTime.UtcNow;
                ActivityToBeModified.UpdatedBy = IdUser;

                await _ActivityService.Reject(ActivityToBeModified, ActivityToBeModified);

                var ActivityUpdated = await _ActivityService.GetById(Id);

                var ActivityResourceUpdated = _mapperService.Map<Activity, ActivityResource>(ActivityUpdated);

                return Ok(ActivityResourceUpdated);
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
