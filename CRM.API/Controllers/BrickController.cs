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
    public class BrickController : ControllerBase
    {
        public IList<Brick> Bricks;

        private readonly IBrickService _BrickService;

        private readonly IMapper _mapperService;
        public BrickController(IBrickService BrickService, IMapper mapper)
        {
            _BrickService = BrickService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Brick>> CreateBrick(SaveBrickResource SaveBrickResource)
        {
            //*** Mappage ***
            var Brick = _mapperService.Map<SaveBrickResource, Brick>(SaveBrickResource);
            //*** Creation dans la base de données ***
            var NewBrick = await _BrickService.Create(Brick);
            //*** Mappage ***
            var BrickResource = _mapperService.Map<Brick, BrickResource>(NewBrick);
            return Ok(BrickResource);
        }
        [HttpGet]
        public async Task<ActionResult<BrickResource>> GetAllBricks()
        {
            try
            {
                var Employe = await _BrickService.GetAll();
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
        public async Task<ActionResult<BrickResource>> GetAllActifBricks()
        {
            try
            {
                var Employe = await _BrickService.GetAllActif();
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
        public async Task<ActionResult<BrickResource>> GetAllInactifBricks()
        {
            try
            {
                var Employe = await _BrickService.GetAllInActif();
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
        public async Task<ActionResult<BrickResource>> GetBrickById(int Id)
        {
            try
            {
                var Bricks = await _BrickService.GetById(Id);
                if (Bricks == null) return NotFound();
                var BrickRessource = _mapperService.Map<Brick, BrickResource>(Bricks);
                return Ok(BrickRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Brick>> UpdateBrick(int Id, SaveBrickResource SaveBrickResource)
        {

            var BrickToBeModified = await _BrickService.GetById(Id);
            if (BrickToBeModified == null) return BadRequest("Le Brick n'existe pas"); //NotFound();
            var Bricks = _mapperService.Map<SaveBrickResource, Brick>(SaveBrickResource);
            //var newBrick = await _BrickService.Create(Bricks);

            await _BrickService.Update(BrickToBeModified, Bricks);

            var BrickUpdated = await _BrickService.GetById(Id);

            var BrickResourceUpdated = _mapperService.Map<Brick, BrickResource>(BrickUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBrick(int Id)
        {
            try
            {

                var sub = await _BrickService.GetById(Id);
                if (sub == null) return BadRequest("Le Brick  n'existe pas"); //NotFound();
                await _BrickService.Delete(sub);
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
                List<Brick> empty = new List<Brick>();
                foreach (var item in Ids)
                {
                    var sub = await _BrickService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Brick  n'existe pas"); //NotFound();

                }
                await _BrickService.DeleteRange(empty);
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
