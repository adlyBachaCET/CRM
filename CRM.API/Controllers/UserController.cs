using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Resources;
using CRM.Core.Services;
using CRM_API.Helper;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        public IList<User> Users;

        private readonly IUserService _UserService;
        private readonly IPhoneService _PhoneService;
        private readonly ISupportService _SupportService;

        private readonly ICommandeService _CommandeService;

        private readonly IBuUserService _BuUserService;
        private readonly IBusinessUnitService _BusinessUnitService;
        private readonly IRequestDoctorService _RequestDoctorService;
        private readonly IObjectionService _ObjectionService;

        private readonly IVisitUserService _VisitUserService;
        private readonly IVisitService _VisitService;
        private readonly IParticipantService _ParticipantService;
        private readonly IRequestRpService _RequestRpService;

        private readonly ILocalityService _LocalityService;


        private readonly IMapper _mapperService;
        public UserController(IParticipantService ParticipantService, IRequestRpService RequestRpService, 
            IObjectionService ObjectionService, IVisitService VisitService, IVisitUserService VisitUserService,
            IRequestDoctorService RequestDoctorService, IBusinessUnitService BusinessUnitService,
            ILocalityService LocalityService, 
            IBuUserService BuUserService, IUserService UserService, ISupportService SupportService,
            IPhoneService PhoneService, ICommandeService CommandeService,
            IMapper mapper)
        {
            _RequestDoctorService = RequestDoctorService;
            _VisitUserService = VisitUserService;
            _VisitService = VisitService;
            _ParticipantService = ParticipantService;
            _RequestRpService = RequestRpService;

            _ObjectionService = ObjectionService;
            _BusinessUnitService = BusinessUnitService;
            _LocalityService = LocalityService;
            _SupportService = SupportService;
            _PhoneService = PhoneService;

            _CommandeService = CommandeService;

            _UserService = UserService;
            _BuUserService = BuUserService;

            _mapperService = mapper;
        }

   
        /// <summary>This method returns the new User .</summary>
        /// <param name="SaveUserResource"> Parameters for the new user .</param>
        [HttpPost]
        public async Task<ActionResult<UserResource>> CreateUser(SaveUserResource SaveUserResource)
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
            ErrorHandling errorHandling = new ErrorHandling();
            var user = await _UserService.AuthenticateManager(lm);
        
            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);

                response = Ok(new { token = tokenString });
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                Response.Cookies.Append("token", tokenString, cookieOptions);
                Response.Headers.Append("token", tokenString);
                errorHandling.ErrorMessage = "Authentification effectué";
                errorHandling.StatusCode = 200;
                return StatusCode(200, errorHandling); 

                //  res.Headers.Add("token", tokenString);
            }
            else
            {
                errorHandling.ErrorMessage = "Unauthorized";
                errorHandling.StatusCode = 401;
                return StatusCode(401, errorHandling);
            }


        } 

        /// <summary>This method authenticates a delegate .</summary>
        /// <param name="lm">Login And Password of the user .</param>
        /// <returns>returns the token with all infos on the authenticated Delegate if found</returns>
        [HttpPost("Login/Delegate")]
        public async Task<IActionResult> LoginDelegate(LoginModel lm)
        {
            IActionResult response = Unauthorized();
            ErrorHandling errorHandling = new ErrorHandling();
            var user = await _UserService.AuthenticateManager(lm);

            if (user != null)
            {
                var tokenString = _UserService.GenerateJSONWebToken(user);

                response = Ok(new { token = tokenString });
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                Response.Cookies.Append("token", tokenString, cookieOptions);
                Response.Headers.Append("token", tokenString);
                errorHandling.ErrorMessage = "Authentification effectué";
                errorHandling.StatusCode = 200;
                return StatusCode(200, errorHandling);

                //  res.Headers.Add("token", tokenString);
            }
            else
            {
                errorHandling.ErrorMessage = "Unauthorized";
                errorHandling.StatusCode = 401;
                return StatusCode(401, errorHandling);
            }
        }
        [HttpPost("Token")]
        public IActionResult Token(Token lm)
        {
            IEnumerable<Claim> claimList = Enumerable.Empty<Claim>();

            var claims = _UserService.getPrincipal(lm.TokenString);
            foreach (var item in claims.Claims)
            { 
               // var claims2 = item.Subject.Claims.;
                claimList.Append(item);
            }

            return Ok(claims.Claims);
        }
        /// <summary>This method returns the list of All the details of the selected user (Profil) .</summary>
        /// <param name="Id">Id of the user .</param>
        [HttpGet("Profil/{Id}")]
        public async Task<ActionResult<UserProfile>> GetAllProfilById(int Id)
        {
            UserProfile Profile = new UserProfile();
 

                //get the details of the user
            var User = await _UserService.GetById(Id);
            var UserResource = _mapperService.Map<User, SaveUserResource>(User);
            var UserResourceWhitoutPassword = _mapperService.Map<SaveUserResource, SaveUserResourceWithoutPassword>(UserResource);
            UserResourceWhitoutPassword.IdUser = Id;
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

            var Objection = await _ObjectionService.GetByIdActifUser(Id);
            //var Users = _mapperService.Map< IEnumerable < User >, IEnumerable<SaveUserResource>>(Delegates);

            Profile.Objection = Objection;
            var RequestDoctor = await _RequestDoctorService.GetByIdActifUser(Id);
            //var Users = _mapperService.Map< IEnumerable < User >, IEnumerable<SaveUserResource>>(Delegates);

            Profile.RequestDoctor = RequestDoctor;

            var Visits = await _VisitUserService.GetAllById(Id);
            List<Visit> VisitList = new List<Visit>();
            foreach(var item in Visits)
            {
                var Visit = await _VisitService.GetById(item.IdVisit);
                VisitList.Add(Visit);
            }
            Profile.Visit = VisitList;

            var Participant = await _ParticipantService.GetAllById(Id);
            List<RequestRp> RequestRpList = new List<RequestRp>();
            foreach (var item in Participant)
            {
                var RequestRp = await _RequestRpService.GetById(item.IdRequestRp);
                RequestRpList.Add(RequestRp);
            }
            Profile.RequestRp = RequestRpList;
            var Commande = await _CommandeService.GetByIdActifUser(Id);

            Profile.Commande = (List<Commande>)Commande;

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

        /// <summary>This method returns the list of all actif users .</summary>
        /// 
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
        /// <summary>This method returns the list of all inactif users .</summary>
        /// 
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
        /// <summary>This method updates .</summary>
        /// <param name="Id">Id of the BusinessUnit .</param>
        [HttpPut("Photo/{Id}")]
        public async Task<IActionResult> Photo(int Id, UpdatePhoto File)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", File.FormFile.FileName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await File.FormFile.CopyToAsync(stream);
                }
                



                await _UserService.UpdatePhoto(Id, File.FormFile.FileName);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //*** Mappage ***


        }
        [HttpPut("UpdateForgottenPassword")]
        public async Task<ActionResult> UpdateForgottenPassword(TokenPassword TokenPassword)
        {
            StringValues token = "";
            ErrorHandling ErrorMessag = new ErrorHandling();
            Request.Headers.TryGetValue("token", out token);

            try
            {
                var Claims = await _SupportService.getPrincipal(token);

                if (Claims == null) { 
                    return NotFound();
                }
                else {
                    var Id = int.Parse(Claims.FindFirst("Id").Value);

                    await _UserService.UpdateGeneratedPassword(Id, TokenPassword.NewPassword);

                        ErrorMessag.ErrorMessage = "Token Found";
                ErrorMessag.StatusCode = 200;
                return Ok(new { ErrorHandling = ErrorMessag, Supports = Claims.Claims });
                   
                }
            }
            catch (Exception ex)
            {
                ErrorMessag.ErrorMessage = ex.Message;
                ErrorMessag.StatusCode = 400;
                return BadRequest(ErrorMessag);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<UserResource>> UpdateUser(int Id, UpdateUserResource UpdateUserResource)
        {

            var UserInDataBase = await _UserService.GetById(Id);
            User UserOld = UserInDataBase;

            if (UserInDataBase == null) return BadRequest("Le User n'existe pas"); //NotFound();
            //var newUser = await _UserService.Create(Users);
            if (UserInDataBase.Password == UpdateUserResource.ConfirmPassword)
            {
           
             //   await _UserService.Update(UserInDataBase, UserOld);
              
              //  var User= _mapperService.Map<SaveUserResource, User>(UpdateUserResource.User);
                var User = _mapperService.Map<SaveUserResourceWithoutPasswordUpdate, User>(UpdateUserResource.User);
                User.Password = UserInDataBase.Password;


                User.IdLocality1 = UpdateUserResource.User.IdLocality1;
                User.NameLocality1 = UpdateUserResource.User.NameLocality1;

                User.IdLocality2 = UpdateUserResource.User.IdLocality2;
                User.NameLocality2 = UpdateUserResource.User.NameLocality2;

            
                User.UpdatedOn = DateTime.UtcNow;
               User.CreatedOn = UserInDataBase.CreatedOn;
                //User.IdUser = Id;
                 await _UserService.Update(UserInDataBase, User);

                return Ok();
            }
            else
                return BadRequest("Password Not Valid");
        }
        [HttpPut("Password/{Id}")]
        public async Task<ActionResult<User>> UpdateUserPassword(int Id,UpdatePasswordResource UpdatePasswordResource)
        {

            var UserInDataBase = await _UserService.GetById(Id);



            if (UserInDataBase.Password == UpdatePasswordResource.CurrentPassword)
            {

              
                await _UserService.UpdatePassword(Id, UpdatePasswordResource.NewPassword);
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
