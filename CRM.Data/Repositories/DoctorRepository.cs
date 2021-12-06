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

        public async Task<IEnumerable<Doctor>> GetDoctorsAssignedByBu(int Id)
        {
            var BuUser = await MyDbContext.BuUser.Where(i => i.IdBu == Id).Include(i=>i.Bu).FirstOrDefaultAsync();

            List<Doctor> list = new List<Doctor>();
            var BuList = await MyDbContext.BuDoctor.Where(i => i.IdBu == BuUser.Bu.IdBu).ToListAsync() ;

            foreach (var item in BuList)
            {
                list.Add(await GetByIdActif(item.IdDoctor));
            }
            List<Doctor> doctors = new List<Doctor>();
            foreach (var item in list)
            {
                doctors.Add(item);
            }
            return doctors;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsByLocalities(List<int> IdLocalities)
        {
            List<Doctor> DoctorsList = new List<Doctor>();

            foreach (var item in IdLocalities) { 
            var LocationListByLocality = await MyDbContext.Location.Where(i => i.Active==0&& i.IdLocality1==item)
                    .Include(i=>i.LocationDoctor).ThenInclude(i => i.Doctor)
                    .ToListAsync();
                foreach(var Location in LocationListByLocality)
                {
                    foreach(var LocationDoctor in Location.LocationDoctor)
                    {
                        DoctorsList.Add(LocationDoctor.Doctor);
                     }
                }
            }
    
            return DoctorsList;
        }
        public async Task<List<Locality>> GetLocalitiesFromDoctors(List<Doctor> DoctorList)
        {
            List<Locality> Localitys = new List<Locality>();
            foreach (var item in DoctorList)
            {
                foreach (var LocationDoctor in item.LocationDoctor)
                {if(!Localitys.Contains(LocationDoctor.Location.Locality1))
                    Localitys.Add(LocationDoctor.Location.Locality1); 
                }
            }

            return Localitys;
        }
        public async Task<IEnumerable<Doctor>> GetMyDoctorsWithoutAppointment(int Id)
        {

            List<Doctor> list = new List<Doctor>();
            var Bu = await MyDbContext.BuDoctor.Where(i => i.IdBu == Id && i.Active == 0).ToListAsync();
            foreach (var item in Bu)
            {
                list.Add(await GetById(item.IdDoctor));
            }

            List<Doctor> DoctorsWithoutAppointment = new List<Doctor>();
            foreach (var item in list)
            {
                if (item.Appointement.Count == 0)
                {
                    DoctorsWithoutAppointment.Add(item);
                }
            }
       
            return DoctorsWithoutAppointment;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAssigned()
        {
            List<Doctor> DoctorsAssigned = new List<Doctor>();
            var list1 = await MyDbContext.Target.Where(i => i.IdDoctorNavigation.Active == 0 && i.IdDoctor != null).ToListAsync();
            foreach (var item in list1)
            {
                var Doctor = await GetById(item.IdDoctor);
                DoctorsAssigned.Add(Doctor);
            }
            return DoctorsAssigned;
        }
        public async Task<DoctorExiste> GetExist(string FirstName, string LastName, string Email)
        {
            DoctorExiste DoctorExiste = new DoctorExiste();
            DoctorExiste.ExistDoctorEmail = false;
            DoctorExiste.FirstLastExist = false;
            DoctorExiste.LastFirstExist = false;
            var FisrtLast = await MyDbContext.Doctor.Where(i => i.FirstName.ToUpper() + " " + i.LastName.ToUpper() == FirstName.ToUpper() + " " + LastName.ToUpper() && i.Active == 0)
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
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location)
                   .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phones)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                .FirstOrDefaultAsync();

            var LastFisrt = await MyDbContext.Doctor.Where(i => i.LastName.ToUpper() + " " + i.FirstName.ToUpper() == LastName.ToUpper() + " " + FirstName.ToUpper() && i.Active == 0)

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
                    .Include(i => i.LocationDoctor).ThenInclude(i => i.Location)
                    .Include(i => i.Phones)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .FirstOrDefaultAsync();

            var EmailD = await MyDbContext.Doctor.Where(i => i.Email == Email && i.Active == 0)
                .Include(i => i.SellingObjectives)
                    .Include(i => i.SharedFiles)
                    .Include(i => i.Potentiel)
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                     .Include(i => i.LocationDoctor).ThenInclude(i => i.Location)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phones)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande).FirstOrDefaultAsync();
            DoctorExiste.DoctorEmail = EmailD;
            DoctorExiste.FirstLast = FisrtLast;
            DoctorExiste.LastFirst = LastFisrt;
            if (EmailD != null) DoctorExiste.ExistDoctorEmail = true;
            if (FisrtLast != null) DoctorExiste.FirstLastExist = true;
            if (LastFisrt != null) DoctorExiste.LastFirstExist = true;

            return
                           DoctorExiste;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsNotAssigned()
        {
            List<Doctor> DoctorsAssigned = new List<Doctor>();
            var list1 = await MyDbContext.Target.Where(i => i.IdDoctorNavigation.Active == 0 && i.IdDoctor == null).ToListAsync();
            foreach (var item in list1)
            {
                var Doctor = await GetById(item.IdDoctor);
                DoctorsAssigned.Add(Doctor);
            }
            return DoctorsAssigned;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsNotAssignedByBu(int Id)
        {
            var BuUser = await MyDbContext.BuUser.Where(i => i.IdUser == Id).Include(i => i.Bu).FirstOrDefaultAsync();

            List<Doctor> list = new List<Doctor>();
            var BuList = await MyDbContext.BuDoctor.Where(i => i.IdBu == BuUser.Bu.IdBu).ToListAsync();
            foreach (var item in BuList)
            {
                list.Add(await GetByIdActif(item.IdDoctor));
            }

            List<Doctor> DoctorsAssigned = new List<Doctor>();
            var list1 = await MyDbContext.Target.Where(i => i.IdDoctorNavigation.Active == 0 && i.IdDoctorNavigation != null && i.Active == 0).ToListAsync();
            foreach (var item in list1)
            {
                var Doctor = await MyDbContext.Doctor.Where(i => i.Active == 0 && i.IdDoctor == item.IdDoctor && i.Active == 0).FirstOrDefaultAsync();
                DoctorsAssigned.Add(Doctor);
            }
            var Alldoctors = await MyDbContext.Doctor.Where(i => i.Status == Status.Approuved && i.Active == 0).ToListAsync();
            var result = Alldoctors.Except(DoctorsAssigned).ToList();
            return result;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsByBu(int Id)
        {
            List<Doctor> list = new List<Doctor>();
            var BuDoctors = await MyDbContext.BuDoctor.Where(i => i.IdBu == Id)
                .Include(i => i.IdDoctorNavigation)
                    .Include(i => i.Bu)
                    .ToListAsync();
            foreach (var item in BuDoctors)
            {
                list.Add(item.IdDoctorNavigation);
            }
            return
                           list;
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
                    .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phones)
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
                    .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
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
                     .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                     .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                    .Include(i => i.Phones)
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
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i=>i.Locality1)
                    .Include(i => i.Phones)
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
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
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
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
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
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
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
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
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
                    .Include(i => i.BuDoctor).ThenInclude(i => i.Bu)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.SpecialtyDoctor).ThenInclude(i => i.Specialty)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Info)
                    .Include(i => i.TagsDoctor).ThenInclude(i => i.Tags)
                   .Include(i => i.LocationDoctor).ThenInclude(i => i.Location).ThenInclude(i => i.Locality1)
                    .Include(i => i.Phones)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)

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

     
    }
}
