using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public DoctorRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Doctor>> GetAllActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0).Include(i => i.Commande)
                .Include(i => i.Objection)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)

                    .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Doctor>> GetByExistantPhoneNumberActif(int PhoneNumber)
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0 && a.PhoneNumber == PhoneNumber).FirstOrDefaultAsync();

            var Doctor = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.IdDoctor == result.IdDoctor)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)

                    .ToListAsync();
            return Doctor;
        }
        public async Task<IEnumerable<Doctor>> GetAllInActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 1)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                     .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .ToListAsync();
            return result;
        }
        public async Task<Doctor> GetByIdActif(int id)
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.IdDoctor == id)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.Linked)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Doctor> GetById(int? id)
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.IdDoctor == id)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAllPending()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Status == Status.Pending)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i=>i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Doctor>> GetAllRejected()
        {
            var result = await MyDbContext.Doctor.Where(a => a.Status == Status.Rejected)
                    .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i=>i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.LocationDoctor).ThenInclude(i => i.Location)
                    .Include(i => i.LocationDoctor).ThenInclude(i => i.Service)

                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Service>> GetServiceByIdLocationActif(int IdLocation)
        {
            List<Service> list = new List<Service>();
            var result = await MyDbContext.LocationDoctor.Where(a => a.Active == 0 && a.IdLocation== IdLocation)
          
                .ToListAsync();
            foreach(var item in result)
            {
                var Service=await MyDbContext.Service.Where(a => a.Active == 0 && a.IdService == item.IdService).FirstOrDefaultAsync();
                list.Add(Service);
            }
            return list;
        }

        public async  Task<List<Specialty>> GetByIdDoctor(int idDoctor)
        {
            List<Specialty> list = new List<Specialty>();
            var result = await MyDbContext.SpecialtyDoctor.Where(a => a.Active == 0 && a.IdDoctor == idDoctor).Include(i=>i.Specialty).ToListAsync();
            foreach (var item in result)
            {
                var Service = await MyDbContext.Specialty.Where(a => a.Active == 0 && a.IdSpecialty == item.IdSpecialty).FirstOrDefaultAsync();
                list.Add(Service);
            }
            return list;
        }

        //public async Task<IEnumerable<Doctor>> GetAllWithArtisteAsync()
        //{
        //    return await MyDoctorDbContext.Doctors
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
