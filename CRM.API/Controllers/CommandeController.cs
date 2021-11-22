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

    public class CommandeController : ControllerBase
    {
        public IList<Commande> Commandes;

        private readonly ICommandeService _CommandeService;
        private readonly IDoctorService _DoctorService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IUserService _UserService;


        private readonly IMapper _mapperService;
        public CommandeController(IUserService UserService, IPharmacyService PharmacyService, IDoctorService DoctorService,ICommandeService CommandeService, IMapper mapper)
        {
            _UserService = UserService;
            _PharmacyService = PharmacyService;

            _CommandeService = CommandeService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<CommandeResource>> CreateCommande(SaveCommandeResource SaveCommandeResource)
        {     
            //*** Mappage ***
            var Commande = _mapperService.Map<SaveCommandeResource, Commande>(SaveCommandeResource);
            Commande.UpdatedOn = DateTime.UtcNow;
            Commande.CreatedOn = DateTime.UtcNow;
            Commande.Active = 0;
            Commande.Status = 0;
            Commande.CreatedBy = 0;
            Commande.UpdatedBy = 0;
            var Doctor = await _DoctorService.GetById(SaveCommandeResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveCommandeResource.IdPharmacy);

            if (Pharmacy != null)
            {
                Commande.Name = Pharmacy.Name;
                Commande.Pharmacy = Pharmacy;
                Commande.VersionPharmacy = Pharmacy.Version;
                Commande.StatusPharmacy = Pharmacy.Status;
                Commande.Doctor = null;
                Commande.VersionDoctor = null;
                Commande.StatusDoctor = null;
            }
            if (Doctor != null)
            {
                Commande.Name = Doctor.Title + " " + Doctor.FirstName + " " + Doctor.LastName;
                Commande.Doctor = Doctor;
                Commande.VersionDoctor = Doctor.Version;
                Commande.StatusDoctor = Doctor.Status;

                Commande.Pharmacy = null;
                Commande.VersionPharmacy = null;
                Commande.StatusPharmacy = null;
            }
            //*** Creation dans la base de données ***
            var NewCommande = await _CommandeService.Create(Commande);
            //*** Mappage ***
            var CommandeResource = _mapperService.Map<Commande, CommandeResource>(NewCommande);
            return Ok(CommandeResource);
      
        }
        [HttpGet]
        public async Task<ActionResult<CommandeResource>> GetAllCommandes()
        {
            try
            {
                var Employe = await _CommandeService.GetAll();
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
        public async Task<ActionResult<CommandeResource>> GetAllActifCommandes()
        {
            try
            {
                var Employe = await _CommandeService.GetAllActif();
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
        public async Task<ActionResult<CommandeResource>> GetAllInactifCommandes()
        {
            try
            {
                var Employe = await _CommandeService.GetAllInActif();
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
        public async Task<ActionResult<CommandeResource>> GetCommandeById(int Id)
        {
            try
            {
                var Commandes = await _CommandeService.GetById(Id);
                if (Commandes == null) return NotFound();
                var CommandeRessource = _mapperService.Map<Commande, CommandeResource>(Commandes);
                return Ok(CommandeRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<CommandeResource>> UpdateCommande(int Id, SaveCommandeResource SaveCommandeResource)
        {

            var CommandeToBeModified = await _CommandeService.GetById(Id);
            if (CommandeToBeModified == null) return BadRequest("Le Commande n'existe pas"); //NotFound();
            var Commandes = _mapperService.Map<SaveCommandeResource, Commande>(SaveCommandeResource);
            //var newCommande = await _CommandeService.Create(Commandes);

            await _CommandeService.Update(CommandeToBeModified, Commandes);

            var CommandeUpdated = await _CommandeService.GetById(Id);

            var CommandeResourceUpdated = _mapperService.Map<Commande, CommandeResource>(CommandeUpdated);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCommande(int Id)
        {
            try
            {

                var sub = await _CommandeService.GetById(Id);
                if (sub == null) return BadRequest("Le Commande  n'existe pas"); //NotFound();
                await _CommandeService.Delete(sub);
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
                List<Commande> empty = new List<Commande>();
                foreach (var item in Ids)
                {
                    var sub = await _CommandeService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Commande  n'existe pas"); //NotFound();

                }
                await _CommandeService.DeleteRange(empty);
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
