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

    public class TagsController : ControllerBase
    {
        public IList<Tags> Tagss;

        private readonly ITagsService _TagsService;

        private readonly IMapper _mapperService;
        public TagsController(ITagsService TagsService, IMapper mapper)
        {
            _TagsService = TagsService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<TagsResource>> CreateTags(SaveTagsResource SaveTagsResource)
        {
            try { 
            var Exist = await _TagsService.GetByExistantActif(SaveTagsResource.Name);
            if(Exist == null){ 
            //*** Mappage ***
            var Tags = _mapperService.Map<SaveTagsResource, Tags>(SaveTagsResource);
                Tags.CreatedOn = DateTime.UtcNow;
                Tags.UpdatedOn = DateTime.UtcNow;
                Tags.Active = 0;
                Tags.Version = 0;
                Tags.CreatedBy = 0;
                Tags.UpdatedBy = 0;
                //*** Creation dans la base de données ***
                var NewTags = await _TagsService.Create(Tags);
            //*** Mappage ***
            var TagsResource = _mapperService.Map<Tags, TagsResource>(NewTags);
            return Ok(TagsResource);
            }
            else
            {
                var genericResult = new { Exist = "Already exists", Location = Exist };
                return Ok(genericResult);
            }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<TagsResource>> GetAllTagss()
        {
            try
            {
                var Employe = await _TagsService.GetAll();
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
        public async Task<ActionResult<TagsResource>> GetAllActifTagss()
        {
            try
            {
                var Employe = await _TagsService.GetAllActif();
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
        public async Task<ActionResult<TagsResource>> GetAllInactifTagss()
        {
            try
            {
                var Employe = await _TagsService.GetAllInActif();
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
        public async Task<ActionResult<TagsResource>> GetTagsById(int Id)
        {
            try
            {
                var Tagss = await _TagsService.GetById(Id);
                if (Tagss == null) return NotFound();
                var TagsRessource = _mapperService.Map<Tags, TagsResource>(Tagss);
                return Ok(TagsRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<TagsResource>> UpdateTags(int Id, SaveTagsResource SaveTagsResource)
        {
            try { 
            var TagsToBeModified = await _TagsService.GetById(Id);
            if (TagsToBeModified == null) return BadRequest("Le Tags n'existe pas");
            var Tags = _mapperService.Map<SaveTagsResource, Tags>(SaveTagsResource);
            Tags.UpdatedOn = DateTime.UtcNow;
            Tags.CreatedOn = TagsToBeModified.CreatedOn;
            Tags.Active = 0;
            Tags.Status = 0;
            Tags.UpdatedBy = 0;
            Tags.CreatedBy = TagsToBeModified.CreatedBy;
            await _TagsService.Update(TagsToBeModified, Tags);

            var TagsUpdated = await _TagsService.GetById(Id);

            var TagsResourceUpdated = _mapperService.Map<Tags, TagsResource>(TagsUpdated);

            return Ok(TagsResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTags(int Id)
        {
            try
            {

                var sub = await _TagsService.GetById(Id);
                if (sub == null) return BadRequest("Le Tags  n'existe pas"); 
                await _TagsService.Delete(sub);
                
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
                List<Tags> empty = new List<Tags>();
                foreach (var item in Ids)
                {
                    var sub = await _TagsService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Tags  n'existe pas"); 

                }
                await _TagsService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
