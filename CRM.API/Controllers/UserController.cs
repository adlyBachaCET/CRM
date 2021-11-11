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
    public class UserController : ControllerBase
    {
        public IList<User> Users;

        private readonly IUserService _UserService;
        private readonly IPhoneService _PhoneService;
        private readonly IBuUserService _BuUserService;
        private readonly IBusinessUnitService _BusinessUnitService;

        private readonly ILocalityService _LocalityService;


        private readonly IMapper _mapperService;
        public UserController(IBusinessUnitService BusinessUnitService,ILocalityService LocalityService,IBuUserService BuUserService, IUserService UserService, IPhoneService PhoneService, IMapper mapper)
        {
            _BusinessUnitService = BusinessUnitService;
            _LocalityService = LocalityService;
             _PhoneService = PhoneService;
             _UserService = UserService;
            _BuUserService = BuUserService;

            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(SaveUserResource SaveUserResource)
        {
            //*** Mappage ***
            var User = _mapperService.Map<SaveUserResource, User>(SaveUserResource);
            User.UpdatedOn = DateTime.UtcNow;
            User.CreatedOn = DateTime.UtcNow;
            //*** Creation dans la base de données ***
            var NewUser = await _UserService.Create(User);
            //*** Mappage ***
            var UserResource = _mapperService.Map<User, UserResource>(NewUser);
            return Ok(UserResource);
        }

        /// <summary>This method authenticates a manager .</summary>
        /// <param name="lm">Login And Password of the user .</param>
        /// <returns>returns the token with all infos on the authenticated manager if found</returns>
        [HttpPost("Login/Manager")]
        public async Task<IActionResult> LoginManager(LoginModel lm)
        {
            IActionResult response = Unauthorized();
            var user = await _UserService.AuthenticateManager(lm);
        
            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;   
        }

        /// <summary>This method authenticates a delegate .</summary>
        /// <param name="lm">Login And Password of the user .</param>
        /// <returns>returns the token with all infos on the authenticated Delegate if found</returns>
        [HttpPost("Login/Delegate")]
        public async Task<IActionResult> LoginDelegate(LoginModel lm)
        {
            List<Locality> localities = new List<Locality>();
            IActionResult response = Unauthorized();

            var user = await _UserService.AuthenticateDelegate(lm);
    
            if (user != null)
            {
        
                var tokenString = _UserService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
  
        /// <summary>This method returns the list of All the details of the selected user (Profil) .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpGet("Profil/{Id}")]
        public async Task<ActionResult<UserProfile>> GetAllProfilById(int Id)
        {
                UserProfile Profile = new UserProfile();
 

                //get the details of the user
                var User = await _UserService.GetById(Id);
                var UserResource = _mapperService.Map<User, SaveUserResource>(User);
            var UserResourceWhitoutPassword = _mapperService.Map<SaveUserResource, SaveUserResourceWithoutPassword>(UserResource);

            Profile.User = UserResourceWhitoutPassword;

          

                //all users by BusinessUnit My team
                var MyBusinessUnit = await _BuUserService.GetByIdUser(Id);
            var Bu = await _BusinessUnitService.GetById(MyBusinessUnit.IdBu);
            var BuResource = _mapperService.Map<BusinessUnit , SaveBusinessUnitResource>(Bu);

            Profile.BusinessUnit = BuResource;
                var Delegates = await _UserService.GetAllDelegateByIdBu(MyBusinessUnit.IdBu);
            //var Users = _mapperService.Map< IEnumerable < User >, IEnumerable<SaveUserResource>>(Delegates);

            Profile.UserOfBu = Delegates;
                

                //Get Immediate Manager
                var ImmediateManage = await _UserService.GetById(Id);
                if (ImmediateManage == null) return NotFound();
                

                return Ok(Profile);
            
        }
        /// <summary>This method returns the list of All the delegate of the same BusinessUnit .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpGet("Delegates/{Id}")]
        public async Task<ActionResult<UserResource>> GetAllDelegateByBu(int Id)
        {
            try
            {
                var User = await _UserService.GetAllDelegateByIdBu(Id);
                if (User == null) return NotFound();
                var UserResource = _mapperService.Map<IEnumerable<User>, UserResource>(User);
                return Ok(UserResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>This method returns the list of All the delegate of one or more BusinessUnit .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpPost("Delegates")]
        public async Task<ActionResult<UserResource>> GetAllDelegateByBu(List<int> Id)
        {
            try
            {
                var User = await _UserService.GetAllDelegateByIdBu(Id);
                if (User == null) return NotFound();
                var UserResource = _mapperService.Map<IEnumerable<User>, UserResource>(User);
                return Ok(UserResource);
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
                var User = await _UserService.GetAllActif();
                if (User == null) return NotFound();
                var UserResource = _mapperService.Map<IEnumerable<User>, UserResource>(User);
                return Ok(UserResource);
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
                var User = await _UserService.GetAllInActif();
                if (User == null) return NotFound();
                var UserResource = _mapperService.Map<IEnumerable<User>, UserResource>(User);
                return Ok(UserResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

  
        [HttpPut("{Id}")]
        public async Task<ActionResult<User>> UpdateUser(int Id, UpdateUserResource UpdateUserResource)
        {

            var UserToBeModified = await _UserService.GetById(Id);
            User UserOld = UserToBeModified;

            if (UserToBeModified == null) return BadRequest("Le User n'existe pas"); //NotFound();
            //var newUser = await _UserService.Create(Users);
            if (UserOld.Password == UpdateUserResource.ConfirmPassword)
            {
           
                await _UserService.Update(UserToBeModified, UserOld);
              
              //  var User= _mapperService.Map<SaveUserResource, User>(UpdateUserResource.User);
                var User = _mapperService.Map<SaveUserResource, User>(UpdateUserResource.User);

             
                User.IdLocality1 = UpdateUserResource.User.IdLocality1;
                User.NameLocality1 = UpdateUserResource.User.NameLocality1;

                User.IdLocality2 = UpdateUserResource.User.IdLocality2;
                User.NameLocality2 = UpdateUserResource.User.NameLocality2;

                User.IdLocality3 = UpdateUserResource.User.IdLocality3;
                User.NameLocality3 = UpdateUserResource.User.NameLocality3;
              
                User.UpdatedOn = DateTime.UtcNow;
               User.CreatedOn = UserToBeModified.CreatedOn;
                //User.IdUser = Id;
                await _UserService.Update(UserToBeModified, User);

                return Ok();
            }
            else
                return BadRequest("Password Not Valid");
        }
        [HttpPut("Password")]
        public async Task<ActionResult<User>> UpdateUserPassword(UpdatePasswordResource UpdatePasswordResource)
        {

            var UserToBeModified = await _UserService.GetById(UpdatePasswordResource.IdUser);
            User UserOld = UserToBeModified;
            if (UserToBeModified == null) return BadRequest("Le User n'existe pas"); //NotFound();
            //var newUser = await _UserService.Create(Users);

            if (UserToBeModified.Password == UpdatePasswordResource.CurrentPassword)
            {
                UserToBeModified.Password = UpdatePasswordResource.NewPassword;
                await _UserService.Update(UserToBeModified, UserOld);
                return Ok(UpdatePasswordResource.NewPassword);
            }
            else
            {
                return BadRequest("Password Not Valid");
            }
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
