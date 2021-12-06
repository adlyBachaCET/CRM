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

    public class ActivityUserController : ControllerBase
    {
        public IList<ActivityUser> ActivityUsers;

        private readonly IActivityUserService _ActivityUserService;
        private readonly IUserService _UserService;
        private readonly IActivityService _ActivityService;

        private readonly IMapper _mapperService;
        public ActivityUserController( IUserService UserService, IActivityService ActivityService, IActivityUserService ActivityUserService, IMapper mapper)
        {
            _UserService = UserService;
            _ActivityUserService = ActivityUserService;
            _ActivityService = ActivityService;

            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ActivityUserResource>> CreateActivityUser(SaveActivityUserResource SaveActivityUserResource)
        {
            try { 
            //*** Mappage ***
            var ActivityUser = _mapperService.Map<SaveActivityUserResource, ActivityUser>(SaveActivityUserResource);
            var User = await _UserService.GetById(SaveActivityUserResource.IdUser);
            ActivityUser.IdUser = User.IdUser;
            ActivityUser.StatusUser = User.Status;
            ActivityUser.VersionUser = User.Version;
            ActivityUser.User = User;
            ActivityUser.UpdatedOn = User.UpdatedOn;
            ActivityUser.CreatedOn = User.CreatedOn;
            ActivityUser.UpdatedBy = User.UpdatedBy;
            var ActivitysinessUnit = await _ActivityService.GetById(SaveActivityUserResource.IdActivity);
            ActivityUser.IdUser = ActivitysinessUnit.IdActivity;
            ActivityUser.StatusUser = ActivitysinessUnit.Status;
            ActivityUser.VersionUser = ActivitysinessUnit.Version;
            ActivityUser.Activity = ActivitysinessUnit;
            ActivityUser.UpdatedOn = ActivitysinessUnit.UpdatedOn;
            ActivityUser.CreatedOn = ActivitysinessUnit.CreatedOn;
            ActivityUser.UpdatedBy = ActivitysinessUnit.UpdatedBy;
            //*** Creation dans la base de données ***
            ActivityUser.CreatedBy = 0;
            ActivityUser.UpdatedBy = 0;
            var NewActivityUser = await _ActivityUserService.Create(ActivityUser);
            //*** Mappage ***
            var ActivityUserResource = _mapperService.Map<ActivityUser, ActivityUserResource>(NewActivityUser);
            return Ok(ActivityUserResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
