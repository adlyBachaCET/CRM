using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class PharmacyRepository : Repository<Pharmacy>, IPharmacyRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public PharmacyRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Pharmacy>> GetAll(Status? Status, GrossistePharmacy GrossistePharmacy)
        {
            if (Status != null) return await MyDbContext.Pharmacy.Where(i => i.Status == Status && i.Active == 0
            && i.GrossistePharmacy == GrossistePharmacy)
                           .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande).ToListAsync();

            return  await MyDbContext.Pharmacy.Where(i =>i.Active == 0 && i.GrossistePharmacy == GrossistePharmacy)
                           .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande).ToListAsync();
        }
      
        
        public async Task<Pharmacy> GetById(int Id, Status? Status, GrossistePharmacy GrossistePharmacy)
        {

            if (Status != null) return await MyDbContext.Pharmacy.Where(i => i.Id == Id && i.Status == Status && i.Active == 0
            && i.GrossistePharmacy == GrossistePharmacy)
                           .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande).FirstOrDefaultAsync();

            return await MyDbContext.Pharmacy.Where(i => i.Id == Id && i.Active == 0
            && i.GrossistePharmacy == GrossistePharmacy)
                           .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande).FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<Pharmacy>> GetPharmacysByLocalities(List<int> IdLocalities)
        {
            List<Pharmacy> PharmacysList = new List<Pharmacy>();

            foreach (var item in IdLocalities)
            {
                var PharmacyListByLocality = await MyDbContext.Pharmacy.Where(i => i.Active == 0 && i.IdLocality1 == item)
                     .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                        .ToListAsync();
          
                        PharmacysList.AddRange(PharmacyListByLocality);
                    
                
            }

            return PharmacysList;
        }
        public async Task<IEnumerable<Pharmacy>> GetAllActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0)
                .Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetMyPharmacysWithoutAppointment(int Id)
        {
            List<Pharmacy> PharmacysWithoutAppointment = new List<Pharmacy>();
            var list1 = await MyDbContext.Appointement.Where(i => i.Pharmacy.Active == 0 && i.Pharmacy != null && i.IdUser == Id).Include(a => a.Doctor)
               .Include(a => a.User).Include(a => a.Pharmacy).ToListAsync();
            foreach (var item in list1)
            {
                PharmacysWithoutAppointment.Add(item.Pharmacy);
            }
            var AllPharmacys = await MyDbContext.Pharmacy.Where(i => i.Status == Status.Approuved && i.Active == 0).Include(i => i.SellingObjectives)
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .ToListAsync();

            var result = AllPharmacys.Except(PharmacysWithoutAppointment).ToList();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetAllInActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i=>i.Product)

                    .ToListAsync();
            return result;
        }
        public async Task<Pharmacy> GetByIdActif(int? id)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Id == id)
                     .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                     .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)

                    .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                     .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .Include(i => i.Commande)
                    .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllPending()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending)
                     .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .Include(i => i.Commande)
                    .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetAllRejected()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending)
                     .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .ToListAsync();
            return result;
        }
        public async Task<Pharmacy> GetByEmailActif(string id)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Email == id)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantNameActif(string Name)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Name== Name)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantEmailActif(string Email)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Email == Email )
                    .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .Include(i => i.Commande).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantLastNameActif(string LastName)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && 
            a.LastNameOwner == LastName)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantFirstNameActif( string FirstName)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 &&a.FirstNameOwner == FirstName)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetByExistantPhoneNumberActif(int PhoneNumber)
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0 && a.PhoneNumber == PhoneNumber).FirstOrDefaultAsync();

            var Pharmacy = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Id == result.IdPharmacy)
                .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .Include(i => i.Commande).ToListAsync();
            return Pharmacy;
        }
        public async Task<IEnumerable<Pharmacy>> GetByNearByActif(string Locality1,string Locality2,int CodePostal)
        {
            var Pharmacy = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.NameLocality1 == Locality1

            && a.NameLocality2 == Locality2 && 
            a.PostalCode == CodePostal)
                     .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .ToListAsync();
            return Pharmacy;
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmacysAssigned()
        {
            var Target = await MyDbContext.Target.Where(a => a.Active == 0 && a.IdPharmacy !=0).Distinct().ToListAsync();
            List<Pharmacy> Pharmacies = new List<Pharmacy>();
            foreach (var item in Target)
            {
                var Pharmacy = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Id == item.IdPharmacy)
                         .Include(i => i.Appointement)
                    .Include(i => i.Visit)
                    .Include(i => i.Target)
                    .Include(i => i.Linked)
                    .Include(i => i.Objection)
                    .Include(i => i.Phone)
                    .Include(i => i.Participant)
                    .Include(i => i.Commande)
                    .Include(i => i.ProductPharmacy).ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync();
                Pharmacies.Add(Pharmacy);
            }
            return Pharmacies;
        }
   
    }
}
