using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SupportService : ISupportService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;

        public SupportService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }
        public string CreatePasswordHash(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        // Generate a random password of a given length (optional)  
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomNumber(int from,int to)
        {
            // Generate a random number  
            Random random = new Random();
            // Any random integer   
            string num = random.Next().ToString();
            return num;

        }
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public async Task<bool> Send(string Name, string EmailLogin)
        {
            bool Existe = false;

          
            //Get the user
            var UserInDB = await _unitOfWork.Users.SingleOrDefault(i => (i.Email == EmailLogin || i.Login == EmailLogin) && i.Active == 0);
            if (UserInDB != null) { 
            //Generate Token 
            var token = GenerateJSONWebToken(UserInDB.IdUser);

            //Update the user's password with th newly created token
            UserInDB.Active = 1;
            await _unitOfWork.CommitAsync();

            await _unitOfWork.CommitAsync();
            User User = new User();
            User = UserInDB;
            User.Version = UserInDB.Version + 1;
            User.IdUser = UserInDB.IdUser;
            User.Status = Status.Approuved;
            User.Active = 0;
            User.GeneratedPassword = token;
            await _unitOfWork.Users.Add(User);
            await _unitOfWork.CommitAsync();

            Support Support = new Support();
      
                Support = await _unitOfWork.Support.SingleOrDefault(a => a.Name == Name);
              
             
                if (Support != null)
                {
                    string from = Support.Email, to = User.Email, subject = "", html = "https://localhost:3000/verify-token/"+token;

                    // create message
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(from));
                    email.To.Add(MailboxAddress.Parse(to));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = html };
                    //Get mail Adresse

                    // send email
                    var smtp = new SmtpClient();
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                 
                      smtp.Connect(Support.Host, Support.Port, false);

                      smtp.Authenticate(Support.Email, Support.Password);
                   
                 
                    
                    smtp.Send(email);

                    smtp.Disconnect(true);

                        return Existe;
                }
                    else
                    {
                        return Existe;
                    }
           
            }
            else
            {
                return Existe;
            }
        }



        public string GenerateJSONWebToken(int Id)
        {
            var claims = new[] {
                           new Claim("Id", Id.ToString())};
            ///var details = JObject.Parse(userInfo.ToString());
           // string json = JsonConvert.SerializeObject(userInfo);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<ClaimsPrincipal> getPrincipal(string token)
        {
            bool SameInDB = false;
            var User = await _unitOfWork.Users.GetByToken(token);
            if(User!= null)
            {
                SameInDB = true;
            }
            if (SameInDB == true) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

          
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Issuer"],
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;



            }
            else {
            return null;
            }

        }




        public async Task<Support> Create(Support newSupport)
        {

            await _unitOfWork.Support.Add(newSupport);
            await _unitOfWork.CommitAsync();
            return newSupport;
        }
        public async Task<List<Support>> CreateRange(List<Support> newSupport)
        {

            await _unitOfWork.Support.AddRange(newSupport);
            await _unitOfWork.CommitAsync();
            return newSupport;
        }
        public async Task<IEnumerable<Support>> GetAll()
        {
            return
                           await _unitOfWork.Support.GetAll();
        }

       /* public async Task Delete(Support Support)
        {
            _unitOfWork.Supports.Remove(Support);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Support>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Supports
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Support> GetById(int? id)
        {
            return
                      await _unitOfWork.Support.SingleOrDefault(i => i.IdSupport == id && i.Active == 0);
        }
   
        public async Task Update(Support SupportToBeUpdated, Support Support)
        {
            SupportToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Support.Version = SupportToBeUpdated.Version + 1;
            Support.IdSupport = SupportToBeUpdated.IdSupport;
            Support.Status = Status.Pending;
            Support.Active = 0;

            await _unitOfWork.Support.Add(Support);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Support Support)
        {
            //Support musi =  _unitOfWork.Supports.SingleOrDefaultAsync(x=>x.Id == SupportToBeUpdated.Id);
            Support.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Support> Support)
        {
            foreach (var item in Support)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Support>> GetAllActif()
        {
            return
                             await _unitOfWork.Support.GetAllActif();
        }

        public async Task<IEnumerable<Support>> GetAllInActif()
        {
            return
                             await _unitOfWork.Support.GetAllInActif();
        }

        public async Task<Support> GetByNameActif(string Name)
        {
            return
                                      await _unitOfWork.Support.GetByNameActif(Name);
        }
        //public Task<Service> CreateService(Service newService)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteService(Service Service)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Service> GetServiceById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Service>> GetServicesByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateService(Service ServiceToBeUpdated, Service Service)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
