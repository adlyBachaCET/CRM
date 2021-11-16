using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CRM_API.Resources;
using System.Security.Claims;
using Newtonsoft.Json;

namespace CRM.Services.Services
{
    public class UserService : IUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;

        public UserService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }

        public async Task<User> Create(User newUser)
        {

            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }
        public async Task<List<User>> CreateRange(List<User> newUser)
        {

            await _unitOfWork.Users.AddRange(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return
                           await _unitOfWork.Users.GetAll();
        }

        public string GenerateJSONWebToken(User userInfo)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("Login", userInfo.Login),
                    new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                      new Claim("Photo", userInfo.Photo.ToString()),
                    new Claim("Id", userInfo.IdUser.ToString())
                    ,
                    new Claim("FirstName", userInfo.FirstName)
                        ,
                    new Claim("LastName", userInfo.LastName.ToString())
    };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> AuthenticateManager(LoginModel lm)
        {
            return
                           await _unitOfWork.Users.SingleOrDefault(i=>i.Login==lm.Login&&i.Password==lm.Password && i.UserType==UserType.Manager && i.Active==0);
        }
        public async Task<User> AuthenticateDelegate(LoginModel lm)
        {
            return
                           await _unitOfWork.Users.SingleOrDefault(i => i.Login == lm.Login && i.Password == lm.Password && i.UserType == UserType.Delegue && i.Active == 0);
        }
        /* public async Task Delete(User User)
         {
             _unitOfWork.Users.Remove(User);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<User>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Users
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<User> GetById(int id)
        {
            return
                        await _unitOfWork.Users.SingleOrDefault(i => i.IdUser == id && i.Active == 0);
        }
   
        public async Task Update(User UserToBeUpdated, User User)
        {
            UserToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            User.Version = UserToBeUpdated.Version + 1;
            User.IdUser = UserToBeUpdated.IdUser;
            User.Status = Status.Approuved;
            User.Active = 0;
            User.CreatedOn = UserToBeUpdated.CreatedOn;
            User.UpdatedOn = DateTime.UtcNow;
            await _unitOfWork.Users.Add(User);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePassword(int Id,string Password)
        {
            var UserInDB= await _unitOfWork.Users.SingleOrDefault(i => i.IdUser == Id && i.Active == 0);
            UserInDB.Active = 1;
            await _unitOfWork.CommitAsync();

            await _unitOfWork.CommitAsync();
            User User = new User();
            User= UserInDB;
            User.Version = UserInDB.Version + 1;
            User.IdUser = UserInDB.IdUser;
            User.Status = Status.Approuved;
            User.Active = 0;
            User.Password = Password ;
            await _unitOfWork.Users.Add(User);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(User User)
        {
            //User musi =  _unitOfWork.Users.SingleOrDefaultAsync(x=>x.Id == UserToBeUpdated.Id);
            User.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<User> User)
        {
            foreach (var item in User)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllActif()
        {
            return
                             await _unitOfWork.Users.GetAllActif();
        }

        public async Task<IEnumerable<User>> GetAllInActif()
        {
            return
                             await _unitOfWork.Users.GetAllInActif();
        }

        public async Task<IEnumerable<User>> GetAllDelegateByIdBu(int IdBu)
        {
            var list = await _unitOfWork.BuUsers.Find(i=>i.IdBu==IdBu);
            List<User> listUsers = new List<User>();
            foreach(var item in list)
            {
                var User = await _unitOfWork.Users.SingleOrDefault(o => o.IdUser== item.IdUser && o.Active == 0);

                listUsers.Add(User);
            }
            return listUsers;
        }

        public async Task<IEnumerable<User>> GetAllDelegateByIdBu(List<int> Bu)
        {
            List<User> listUsers = new List<User>();

            foreach(var i in Bu){
                var list = await _unitOfWork.BuUsers.Find(o => o.IdBu == i && o.Active == 0);
                foreach (var item in list)
                {
                    var User = await _unitOfWork.Users.SingleOrDefault(o => o.UserType == UserType.Delegue && o.Active==0);

                    listUsers.Add(User);
                }
            }
            return listUsers;
        }

        public async Task UpdatePhoto(int Id, string FileName)
        {
            var UserDB = await _unitOfWork.Users.SingleOrDefault(o => o.IdUser== Id && o.Active == 0);

            UserDB.Active = 1;
            await _unitOfWork.CommitAsync();

            User User = new User();
            User = UserDB;
            User.Version = UserDB.Version + 1;
            User.IdUser = UserDB.IdUser;
            User.Status = Status.Approuved;
            User.Active = 0;
            User.CreatedOn = UserDB.CreatedOn;
            User.UpdatedOn = DateTime.UtcNow;
            User.Photo = FileName;
            await _unitOfWork.Users.Add(User);
            await _unitOfWork.CommitAsync();
        }
        //public Task<User> CreateUser(User newUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteUser(User User)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<User> GetUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<User>> GetUsersByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateUser(User UserToBeUpdated, User User)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
