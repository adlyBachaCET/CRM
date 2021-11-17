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

        public async Task<IEnumerable<Pharmacy>> GetAllActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllInActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1).ToListAsync();
            return result;
        }
        public async Task<Pharmacy> GetByIdActif(int id)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.IdPharmacy == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 1 && a.Status == Status.Approuved).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllPending()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetAllRejected()
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Status == Status.Pending).ToListAsync();
            return result;
        }
        public async Task<Pharmacy> GetByEmailActif(string id)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Email == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantNameActif(string Name)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Name== Name).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantEmailActif(string Email)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.Email == Email ).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantLastNameActif(string LastName)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && 
            a.LastNameOwner == LastName ).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Pharmacy> GetByExistantFirstNameActif( string FirstName)
        {
            var result = await MyDbContext.Pharmacy.Where(a => a.Active == 0 &&a.FirstNameOwner == FirstName).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<Pharmacy>> GetByExistantPhoneNumberActif(int PhoneNumber)
        {
            var result = await MyDbContext.Phone.Where(a => a.Active == 0 && a.PhoneNumber == PhoneNumber).FirstOrDefaultAsync();

            var Pharmacy = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.IdPharmacy == result.IdPharmacy).ToListAsync();
            return Pharmacy;
        }
        public async Task<IEnumerable<Pharmacy>> GetByNearByActif(string Locality1,string Locality2,int CodePostal)
        {
            var Pharmacy = await MyDbContext.Pharmacy.Where(a => a.Active == 0 && a.NameLocality1 == Locality1

            && a.NameLocality2 == Locality2 && 
            a.PostalCode == CodePostal).ToListAsync();
            return Pharmacy;
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmacysAssigned()
        {
            var Target = await MyDbContext.Target.Where(a => a.Active == 0 && a.IdPharmacy !=null).Select(a=>a.IdPharmacy).ToListAsync();
            return null;
        }
        //public async Task<IEnumerable<Pharmacy>> GetAllWithArtisteAsync()
        //{
        //    return await MyPharmacyDbContext.Pharmacys
        //        .Include(x => x.Artiste).ToListAsync();
        //}
    }
}
