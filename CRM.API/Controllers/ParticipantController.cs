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

    public class ParticipantController : ControllerBase
    {
        public IList<Participant> Participants;

        private readonly IParticipantService _ParticipantService;
        private readonly IDoctorService _DoctorService;
        private readonly IUserService _UserService;
        private readonly IPharmacyService _PharmacyService;

        private readonly IRequestRpService _RequestRpService;

        private readonly IMapper _mapperService;
        public ParticipantController(IRequestRpService RequestRpService,
            IUserService UserService, IPharmacyService PharmacyService,
            IDoctorService DoctorService,IParticipantService ParticipantService, IMapper mapper)
        {
            _RequestRpService = RequestRpService;
            _UserService = UserService;
            _PharmacyService = PharmacyService;

            _DoctorService = DoctorService;
           _ParticipantService = ParticipantService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<ParticipantResource>> CreateParticipant(SaveParticipantResource SaveParticipantResource)
        {
            try { 
                //*** Mappage ***
            var Participant = _mapperService.Map<SaveParticipantResource, Participant>(SaveParticipantResource);
            Participant.UpdatedOn = DateTime.UtcNow;
            Participant.CreatedOn = DateTime.UtcNow;
            Participant.Active = 0;
            Participant.Status = 0;
            Participant.UpdatedBy = 0;
            Participant.CreatedBy = 0;
            var Doctor = await _DoctorService.GetById(SaveParticipantResource.IdDoctor);

            var Pharmacy = await _PharmacyService.GetById(SaveParticipantResource.IdPharmacy);

            if (Pharmacy != null)
            {
                Participant.IdPharmacy = Pharmacy.Id;

                Participant.IdPharmacyNavigation = Pharmacy;
                Participant.VersionPharmacy = Pharmacy.Version;
                Participant.StatusPharmacy = Pharmacy.Status;
                Participant.IdDoctorNavigation = null;
                Participant.VersionDoctor = null;
                Participant.StatusDoctor = null;

            }
            if (Doctor != null)
            {
                Participant.IdDoctor = Doctor.IdDoctor;

                Participant.IdDoctorNavigation = Doctor;
                Participant.VersionDoctor = Doctor.Version;
                Participant.StatusDoctor = Doctor.Status;
                Participant.IdPharmacy = null;

                Participant.IdPharmacyNavigation = null;
                Participant.VersionPharmacy = null;
                Participant.StatusPharmacy = null;
            }

            var RequestRp = await _RequestRpService.GetById(SaveParticipantResource.IdRequestRp);
            Participant.IdRequestRpNavigation = RequestRp;
            Participant.VersionRequestRp = RequestRp.Version;
            Participant.StatusRequestRp = RequestRp.Status;

            var User = await _UserService.GetById(SaveParticipantResource.IdUser);
            Participant.IdUserNavigation = User;
            Participant.VersionUser = User.Version;
            Participant.StatusUser = User.Status;
            Participant.IdUser = User.IdUser;

            //*** Creation dans la base de donn??es ***
            var NewParticipant = await _ParticipantService.Create(Participant);
            //*** Mappage ***
            var ParticipantResource = _mapperService.Map<Participant, ParticipantResource>(NewParticipant);
            return Ok(ParticipantResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Range")]
        public async Task<ActionResult<ParticipantResource>> CreateRangeParticipant(ListSaveParticipantResource SaveParticipantResource)
        {
            try { 
            if (SaveParticipantResource.IdDoctor.Count>0)
            {
                foreach(var item in SaveParticipantResource.IdDoctor) { 
                //*** Mappage ***
                var Participant = new Participant();
                Participant.UpdatedOn = DateTime.UtcNow;
                Participant.CreatedOn = DateTime.UtcNow;
                Participant.Active = 0;
                Participant.Status = 0;
                Participant.UpdatedBy = 0;
                Participant.CreatedBy = 0;
                var Doctor = await _DoctorService.GetById(item);
                Participant.IdDoctor = Doctor.IdDoctor;
               Participant.IdDoctorNavigation = Doctor;
               Participant.VersionDoctor = Doctor.Version;
               Participant.StatusDoctor = Doctor.Status;
               Participant.IdPharmacy = null;
               Participant.IdPharmacyNavigation = null;
               Participant.VersionPharmacy = null;
               Participant.StatusPharmacy = null;
               Participant.IdUserNavigation = null;
               Participant.VersionUser = null;
               Participant.StatusUser = null;
               Participant.IdUser = null;

            var RequestRp = await _RequestRpService.GetById(SaveParticipantResource.IdRequestRp);
               Participant.IdRequestRpNavigation = RequestRp;
               Participant.VersionRequestRp = RequestRp.Version;
               Participant.StatusRequestRp = RequestRp.Status;
                    Participant.IdRequestRp = RequestRp.IdRequestRp;


                    //*** Creation dans la base de donn??es ***
                    var NewParticipant = await _ParticipantService.Create(Participant);
                var ParticipantResource = _mapperService.Map<Participant, ParticipantResource>(NewParticipant);
                }
            }

            if (SaveParticipantResource.IdPharmacy.Count > 0)
            {
                foreach (var item in SaveParticipantResource.IdPharmacy)
                {
                    //*** Mappage ***
                    var Participant = new Participant();
                    Participant.UpdatedOn = DateTime.UtcNow;
                    Participant.CreatedOn = DateTime.UtcNow;
                    Participant.Active = 0;
                    Participant.Status = 0;
                    Participant.UpdatedBy = 0;
                    Participant.CreatedBy = 0;
                    var Pharmacy = await _PharmacyService.GetById(item);
                    Participant.IdDoctor =null;
                    Participant.IdDoctorNavigation = null;
                    Participant.VersionDoctor = null;
                    Participant.StatusDoctor = null;
                    Participant.IdPharmacy = Pharmacy.Id;
                    Participant.IdPharmacyNavigation = Pharmacy;
                    Participant.VersionPharmacy = Pharmacy.Version;
                    Participant.StatusPharmacy = Pharmacy.Status;
                    Participant.IdUserNavigation = null;
                    Participant.VersionUser = null;
                    Participant.StatusUser = null;
                    Participant.IdUser = null;

                    var RequestRp = await _RequestRpService.GetById(SaveParticipantResource.IdRequestRp);
                    Participant.IdRequestRpNavigation = RequestRp;
                    Participant.VersionRequestRp = RequestRp.Version;
                    Participant.StatusRequestRp = RequestRp.Status;
                    Participant.IdRequestRp = RequestRp.IdRequestRp;



                    //*** Creation dans la base de donn??es ***
                    var NewParticipant = await _ParticipantService.Create(Participant);
                    var ParticipantResource = _mapperService.Map<Participant, ParticipantResource>(NewParticipant);
                }
            }
            if (SaveParticipantResource.IdUser.Count > 0)
            {
                foreach (var item in SaveParticipantResource.IdUser)
                {
                    //*** Mappage ***
                    var Participant = new Participant();
                    Participant.UpdatedOn = DateTime.UtcNow;
                    Participant.CreatedOn = DateTime.UtcNow;
                    Participant.Active = 0;
                    Participant.Status = 0;
                    Participant.UpdatedBy = 0;
                    Participant.CreatedBy = 0;
                    var User = await _UserService.GetById(item);
                    Participant.IdDoctor = null;
                    Participant.IdDoctorNavigation = null;
                    Participant.VersionDoctor = null;
                    Participant.StatusDoctor = null;
                    Participant.IdPharmacy = null;
                    Participant.IdPharmacyNavigation = null;
                    Participant.VersionPharmacy = null;
                    Participant.StatusPharmacy = null;
                    Participant.IdUserNavigation = User;
                    Participant.VersionUser = User.Version;
                    Participant.StatusUser = User.Status;
                    Participant.IdUser = User.IdUser;

                    var RequestRp = await _RequestRpService.GetById(SaveParticipantResource.IdRequestRp);
                    Participant.IdRequestRpNavigation = RequestRp;
                    Participant.VersionRequestRp = RequestRp.Version;
                    Participant.StatusRequestRp = RequestRp.Status;
                    Participant.IdRequestRp = RequestRp.IdRequestRp;


                    //*** Creation dans la base de donn??es ***
                    var NewParticipant = await _ParticipantService.Create(Participant);
                    var ParticipantResource = _mapperService.Map<Participant, ParticipantResource>(NewParticipant);
                }
            }
            //*** Mappage ***

            return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ParticipantResource>> GetAllParticipants()
        {
            try
            {
                var Employe = await _ParticipantService.GetAll();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Actif")]
        public async Task<ActionResult<ParticipantResource>> GetAllActifParticipants()
        {
            try
            {
                var Employe = await _ParticipantService.GetAllActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("InActif")]
        public async Task<ActionResult<ParticipantResource>> GetAllInactifParticipants()
        {
            try
            {
                var Employe = await _ParticipantService.GetAllInActif();
                if (Employe == null) return NotFound();
                return Ok(Employe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ParticipantResource>> GetParticipantById(int Id)
        {
            try
            {
                var Participants = await _ParticipantService.GetById(Id);
                if (Participants == null) return NotFound();
                var ParticipantRessource = _mapperService.Map<Participant, ParticipantResource>(Participants);
                return Ok(ParticipantRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ParticipantResource>> UpdateParticipant(int Id, SaveParticipantResource SaveParticipantResource)
        {
            try { 
            var ParticipantToBeModified = await _ParticipantService.GetById(Id);
            if (ParticipantToBeModified == null) return BadRequest("Le Participant n'existe pas"); 
            var Participants = _mapperService.Map<SaveParticipantResource, Participant>(SaveParticipantResource);

            await _ParticipantService.Update(ParticipantToBeModified, Participants);

            var ParticipantUpdated = await _ParticipantService.GetById(Id);

            var ParticipantResourceUpdated = _mapperService.Map<Participant, ParticipantResource>(ParticipantUpdated);

            return Ok(ParticipantResourceUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteParticipant(int Id)
        {
            try
            {

                var sub = await _ParticipantService.GetById(Id);
                if (sub == null) return BadRequest("Le Participant  n'existe pas");
                await _ParticipantService.Delete(sub);
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
                List<Participant> empty = new List<Participant>();
                foreach (var item in Ids)
                {
                    var sub = await _ParticipantService.GetById(item);
                    empty.Add(sub);
                    if (sub == null) return BadRequest("Le Participant  n'existe pas");

                }
                await _ParticipantService.DeleteRange(empty);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
