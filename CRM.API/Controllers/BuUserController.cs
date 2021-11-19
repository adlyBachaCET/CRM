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

    public class BuUserController : ControllerBase
    {
        public IList<BuUser> BuUsers;

        private readonly IBuUserService _BuUserService;
        private readonly IBusinessUnitService _BuService;
        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public BuUserController(IBusinessUnitService BuService, IUserService UserService, IBuUserService BuUserService, IMapper mapper)
        {
            _BuService = BuService;
            _UserService = UserService;
            _BuUserService = BuUserService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BuUserResource>> CreateBuUser(SaveBuUserResource SaveBuUserResource)
        {
            //*** Mappage ***
            var BuUser = _mapperService.Map<SaveBuUserResource, BuUser>(SaveBuUserResource);
            var User = await _UserService.GetById(SaveBuUserResource.IdUser);
            BuUser.IdUser = User.IdUser;
            BuUser.StatusUser = User.Status;
            BuUser.VersionUser = User.Version;
            BuUser.IdUserNavigation = User;
            BuUser.UpdatedOn = User.UpdatedOn;
            BuUser.CreatedOn = User.CreatedOn;
            BuUser.UpdatedBy = User.UpdatedBy;
            var BusinessUnit = await _BuService.GetById(SaveBuUserResource.IdBu);
            BuUser.IdUser = BusinessUnit.IdBu;
            BuUser.StatusUser = BusinessUnit.Status;
            BuUser.VersionUser = BusinessUnit.Version;
            BuUser.IdBuNavigation = BusinessUnit;
            BuUser.UpdatedOn = BusinessUnit.UpdatedOn;
            BuUser.CreatedOn = BusinessUnit.CreatedOn;
            BuUser.UpdatedBy = BusinessUnit.UpdatedBy;
            //*** Creation dans la base de données ***
            BuUser.CreatedBy = 0;
            BuUser.UpdatedBy = 0;
            var NewBuUser = await _BuUserService.Create(BuUser);
            //*** Mappage ***
            var BuUserResource = _mapperService.Map<BuUser, BuUserResource>(NewBuUser);
            return Ok(BuUserResource);
        }
        
    }
}
