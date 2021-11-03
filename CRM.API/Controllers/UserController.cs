using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IList<User> Users;

        private readonly IUserService _UserService;

        private readonly IMapper _mapperService;
        public UserController(IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(SaveUserResource SaveUserResource)
        {
            //*** Mappage ***
            var User = _mapperService.Map<SaveUserResource, User>(SaveUserResource);
            //*** Creation dans la base de données ***
            var NewUser = await _UserService.Create(User);
            //*** Mappage ***
            var UserResource = _mapperService.Map<User, UserResource>(NewUser);
            return Ok(UserResource);
        }
        [HttpPost("Login/Manager")]
        public async Task<IActionResult> LoginManager(LoginModel lm)
        {
            IActionResult response = Unauthorized();

            //*** Mappage ***
            // var User = _mapperService.Map<SaveUserResource, User>(SaveUserResource);
            var user = await _UserService.AuthenticateManager(lm);
            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;   
        }
        [HttpPost("Login/Delegate")]
        public async Task<IActionResult> LoginDelegate(LoginModel lm)
        {
            IActionResult response = Unauthorized();

            //*** Mappage ***
            // var User = _mapperService.Map<SaveUserResource, User>(SaveUserResource);
            var user = await _UserService.AuthenticateDelegate(lm);
            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        [HttpGet]
        public async Task<ActionResult<UserResource>> GetAllUsers()
        {
            try
            {
                var Employe = await _UserService.GetAll();
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
        public async Task<ActionResult<UserResource>> GetAllActifUsers()
        {
            try
            {
                var Employe = await _UserService.GetAllActif();
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
        public async Task<ActionResult<UserResource>> GetAllInactifUsers()
        {
            try
            {
                var Employe = await _UserService.GetAllInActif();
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
        public async Task<ActionResult<UserResource>> GetUserById(int Id)
        {
            try
            {
                var Users = await _UserService.GetById(Id);
                if (Users == null) return NotFound();
                var UserRessource = _mapperService.Map<User, UserResource>(Users);
                return Ok(UserRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<User>> UpdateUser(int Id, SaveUserResource SaveUserResource)
        {

            var UserToBeModified = await _UserService.GetById(Id);
            if (UserToBeModified == null) return BadRequest("Le User n'existe pas"); //NotFound();
            var Users = _mapperService.Map<SaveUserResource, User>(SaveUserResource);
            //var newUser = await _UserService.Create(Users);

            await _UserService.Update(UserToBeModified, Users);

            var UserUpdated = await _UserService.GetById(Id);

            var UserResourceUpdated = _mapperService.Map<User, UserResource>(UserUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            try
            {

                var sub = await _UserService.GetById(Id);
                if (sub == null) return BadRequest("Le User  n'existe pas"); //NotFound();
                await _UserService.Delete(sub);
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
                List<User> empty = new List<User>();
                foreach (var item in Ids)
                {
                    var sub = await _UserService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le User  n'existe pas"); //NotFound();

                }
                await _UserService.DeleteRange(empty);
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
