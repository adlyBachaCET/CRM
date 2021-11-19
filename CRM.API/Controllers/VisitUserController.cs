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

    public class VisitUserController : ControllerBase
    {
        public IList<VisitUser> VisitUsers;
        public IList<User> Users;

        private readonly IVisitUserService _VisitUserService;
        private readonly IUserService _UserService;
        private readonly IVisitService _VisitService;

        private readonly IMapper _mapperService;
        public VisitUserController(IVisitService VisitService, IVisitUserService VisitUserService, IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _VisitService = VisitService;

            _VisitUserService = VisitUserService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<VisitUserResource>> CreateVisitUser(SaveVisitUserResource SaveVisitUserResource)
  {     
            //*** Mappage ***
            var VisitUser = _mapperService.Map<SaveVisitUserResource, VisitUser>(SaveVisitUserResource);
            var Visit = await _VisitService.GetById(SaveVisitUserResource.IdVisit);
            VisitUser.IdVisit = Visit.IdVisit;
            VisitUser.StatusVisit = Visit.Status;
            VisitUser.VersionVisit = Visit.Version;
            VisitUser.Visit = Visit;
            VisitUser.UpdatedOn = DateTime.UtcNow;
            VisitUser.CreatedOn = DateTime.UtcNow;
            VisitUser.UpdatedBy = Visit.UpdatedBy;
            VisitUser.CreatedBy = Visit.CreatedBy;
            var User = await _UserService.GetById(SaveVisitUserResource.IdUser);
            VisitUser.IdUser = User.IdUser;
            VisitUser.StatusUser = User.Status;
            VisitUser.VersionUser = User.Version;
            VisitUser.User = User;
            VisitUser.UpdatedOn = DateTime.UtcNow;
            VisitUser.CreatedOn = DateTime.UtcNow;
            VisitUser.UpdatedBy = User.UpdatedBy;
            VisitUser.CreatedBy = User.CreatedBy;
            //*** Creation dans la base de données ***
            var NewVisitUser = await _VisitUserService.Create(VisitUser);
            //*** Mappage ***
            var VisitUserResource = _mapperService.Map<VisitUser, VisitUserResource>(NewVisitUser);
            return Ok(VisitUserResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<VisitUserResource>> GetAllVisitUsers()
        {
            try
            {
                var Employe = await _VisitUserService.GetAll();
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
        public async Task<ActionResult<VisitUserResource>> GetAllActifVisitUsers()
        {
            try
            {
                var Employe = await _VisitUserService.GetAllActif();
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
        public async Task<ActionResult<VisitUserResource>> GetAllInactifVisitUsers()
        {
            try
            {
                var Employe = await _VisitUserService.GetAllInActif();
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
        public async Task<ActionResult<VisitUserResource>> GetVisitUserById(int Id)
        {
            try
            {
                var VisitUsers = await _VisitUserService.GetById(Id);
                if (VisitUsers == null) return NotFound();
                var VisitUserRessource = _mapperService.Map<VisitUser, VisitUserResource>(VisitUsers);
                return Ok(VisitUserRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<VisitUserResource>> UpdateVisitUser(int Id, SaveVisitUserResource SaveVisitUserResource)
        {

            var VisitUserToBeModified = await _VisitUserService.GetById(Id);
            if (VisitUserToBeModified == null) return BadRequest("Le VisitUser n'existe pas"); //NotFound();
            var VisitUsers = _mapperService.Map<SaveVisitUserResource, VisitUser>(SaveVisitUserResource);
            //var newVisitUser = await _VisitUserService.Create(VisitUsers);

            await _VisitUserService.Update(VisitUserToBeModified, VisitUsers);

            var VisitUserUpdated = await _VisitUserService.GetById(Id);

            var VisitUserResourceUpdated = _mapperService.Map<VisitUser, VisitUserResource>(VisitUserUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVisitUser(int Id)
        {
            try
            {

                var sub = await _VisitUserService.GetById(Id);
                if (sub == null) return BadRequest("Le VisitUser  n'existe pas"); //NotFound();
                await _VisitUserService.Delete(sub);
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
                List<VisitUser> empty = new List<VisitUser>();
                foreach (var item in Ids)
                {
                    var sub = await _VisitUserService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le VisitUser  n'existe pas"); //NotFound();

                }
                await _VisitUserService.DeleteRange(empty);
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
