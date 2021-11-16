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
        [HttpPost("Range")]
        public async Task<ActionResult<TagsDoctor>> CreateTagsDoctor(List<SaveTagsDoctorResource> SaveTagsDoctorResource)
        {
            //*** Mappage ***
            var TagsDoctor = _mapperService.Map<List<SaveTagsDoctorResource>, TagsDoctor>(SaveTagsDoctorResource);
            //*** Creation dans la base de données ***
            var NewTagsDoctor = await _TagsDoctorService.Create(TagsDoctor);
            //*** Mappage ***
            var TagsDoctorResource = _mapperService.Map<TagsDoctor, TagsDoctorResource>(NewTagsDoctor);
            return Ok(TagsDoctorResource);
        }





    }
}
