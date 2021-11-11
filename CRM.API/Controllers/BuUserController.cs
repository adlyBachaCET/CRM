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

    public class BuUserController : ControllerBase
    {
        public IList<BuUser> BuUsers;

        private readonly IBuUserService _BuUserService;

        private readonly IMapper _mapperService;
        public BuUserController(IBuUserService BuUserService, IMapper mapper)
        {
            _BuUserService = BuUserService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BuUser>> CreateBuUser(SaveBuUserResource SaveBuUserResource)
        {
            //*** Mappage ***
            var BuUser = _mapperService.Map<SaveBuUserResource, BuUser>(SaveBuUserResource);
            //*** Creation dans la base de données ***
            var NewBuUser = await _BuUserService.Create(BuUser);
            //*** Mappage ***
            var BuUserResource = _mapperService.Map<BuUser, BuUserResource>(NewBuUser);
            return Ok(BuUserResource);
        }
        [HttpGet]
        public async Task<ActionResult<BuUserResource>> GetAllBuUsers()
        {
            try
            {
                var Employe = await _BuUserService.GetAll();
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
        public async Task<ActionResult<BuUserResource>> GetAllActifBuUsers()
        {
            try
            {
                var Employe = await _BuUserService.GetAllActif();
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
        public async Task<ActionResult<BuUserResource>> GetAllInactifBuUsers()
        {
            try
            {
                var Employe = await _BuUserService.GetAllInActif();
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
        public async Task<ActionResult<BuUserResource>> GetBuUserById(int Id)
        {
            try
            {
                var BuUsers = await _BuUserService.GetById(Id);
                if (BuUsers == null) return NotFound();
                var BuUserRessource = _mapperService.Map<BuUser, BuUserResource>(BuUsers);
                return Ok(BuUserRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpPut("{Id}")]
        public async Task<ActionResult<BuUser>> UpdateBuUser(int Id, SaveBuUserResource SaveBuUserResource)
        {

            var BuUserToBeModified = await _BuUserService.GetById(Id);
            if (BuUserToBeModified == null) return BadRequest("Le BuUser n'existe pas"); //NotFound();
            var BuUsers = _mapperService.Map<SaveBuUserResource, BuUser>(SaveBuUserResource);
            //var newBuUser = await _BuUserService.Create(BuUsers);

            await _BuUserService.Update(BuUserToBeModified, BuUsers);

            var BuUserUpdated = await _BuUserService.GetById(Id);

            var BuUserResourceUpdated = _mapperService.Map<BuUser, BuUserResource>(BuUserUpdated);

            return Ok();
        }*/


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBuUser(int Id)
        {
            try
            {

                var sub = await _BuUserService.GetById(Id);
                if (sub == null) return BadRequest("Le BuUser  n'existe pas"); //NotFound();
                await _BuUserService.Delete(sub);
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
                List<BuUser> empty = new List<BuUser>();
                foreach (var item in Ids)
                {
                    var sub = await _BuUserService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le BuUser  n'existe pas"); //NotFound();

                }
                await _BuUserService.DeleteRange(empty);
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
