using CRM.Core.Models;
using CRM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data.Repositories
{
    public class TargetRepository : Repository<Target>, ITargetRepository
    {
        private MyDbContext MyDbContext
        {
            get
            {
                return _context as MyDbContext;
            }
        }
        public TargetRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Target>> GetAllActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i=>i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)

                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Target>> GetEmptyTargetByNumTarget(int id)
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.NumTarget== id 
            && a.IdPharmacyNavigation==null && a.IdDoctorNavigation==null )
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation).ThenInclude(i => i.PotentielCycle).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdSectorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)

                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Target>> GetByFullTarget(int id)
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.NumTarget == id
            && a.IdPharmacyNavigation != null && a.IdDoctorNavigation != null)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation).ThenInclude(i => i.PotentielCycle).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdSectorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)

                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Target>> GetAllInActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 1)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation).ToListAsync();
            return result;
        }
        public async Task<Target> GetByIdActif(int id)
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.IdCycle == id)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation).FirstOrDefaultAsync();
            return result;
        }
 
        public async Task<IEnumerable<Target>> GetAllAcceptedActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 0 && a.Status == Status.Approuved)
                    .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllAcceptedInactifActif()
        {
            var result = await MyDbContext.Target.Where(a => a.Active == 1 && a.Status == Status.Approuved)
                    .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Target>> GetAllPending()
        {
            var result = await MyDbContext.Target.Where(a => a.Status == Status.Pending)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Target>> GetAllRejected()
        {
            var result = await MyDbContext.Target.Where(a => a.Status == Status.Rejected)
                .Include(i => i.IdPharmacyNavigation).ThenInclude(i => i.Locality1)
                .Include(i => i.IdCycleNavigation)
                .Include(i => i.IdDoctorNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.SectorLocality).ThenInclude(i => i.IdLocalityNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.PotentielSector).ThenInclude(i => i.IdPotentielNavigation)
                .Include(i => i.IdSectorNavigation).ThenInclude(i => i.WeekSectorCycleInYear)
                .Include(i => i.IdUserNavigation)
                .ToListAsync();
            return result;
        }
 
    }
}
