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
    public class TagsDoctorController : ControllerBase
    {
        public IList<TagsDoctor> TagsDoctors;

        private readonly ITagsDoctorService _TagsDoctorService;

        private readonly IMapper _mapperService;
        public TagsDoctorController(ITagsDoctorService TagsDoctorService, IMapper mapper)
        {
            _TagsDoctorService = TagsDoctorService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<TagsDoctor>> CreateTagsDoctor(SaveTagsDoctorResource SaveTagsDoctorResource)
        {
            //*** Mappage ***
            var TagsDoctor = _mapperService.Map<SaveTagsDoctorResource, TagsDoctor>(SaveTagsDoctorResource);
            //*** Creation dans la base de données ***
            var NewTagsDoctor = await _TagsDoctorService.Create(TagsDoctor);
            //*** Mappage ***
            var TagsDoctorResource = _mapperService.Map<TagsDoctor, TagsDoctorResource>(NewTagsDoctor);
            return Ok(TagsDoctorResource);
        }
        [HttpGet]
        public async Task<ActionResult<TagsDoctorResource>> GetAllTagsDoctors()
        {
            try
            {
                var Employe = await _TagsDoctorService.GetAll();
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
        public async Task<ActionResult<TagsDoctorResource>> GetAllActifTagsDoctors()
        {
            try
            {
                var Employe = await _TagsDoctorService.GetAllActif();
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
        public async Task<ActionResult<TagsDoctorResource>> GetAllInactifTagsDoctors()
        {
            try
            {
                var Employe = await _TagsDoctorService.GetAllInActif();
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
        public async Task<ActionResult<TagsDoctorResource>> GetTagsDoctorById(int Id)
        {
            try
            {
                var TagsDoctors = await _TagsDoctorService.GetById(Id);
                if (TagsDoctors == null) return NotFound();
                var TagsDoctorRessource = _mapperService.Map<TagsDoctor, TagsDoctorResource>(TagsDoctors);
                return Ok(TagsDoctorRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]



        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTagsDoctor(int Id)
        {
            try
            {

                var sub = await _TagsDoctorService.GetById(Id);
                if (sub == null) return BadRequest("Le TagsDoctor  n'existe pas"); //NotFound();
                await _TagsDoctorService.Delete(sub);
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
                List<TagsDoctor> empty = new List<TagsDoctor>();
                foreach (var item in Ids)
                {
                    var sub = await _TagsDoctorService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le TagsDoctor  n'existe pas"); //NotFound();

                }
                await _TagsDoctorService.DeleteRange(empty);
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
