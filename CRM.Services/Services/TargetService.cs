using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class TargetService : ITargetService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public TargetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Target> Create(Target newTarget)
        {

            await _unitOfWork.Target.Add(newTarget);
            await _unitOfWork.CommitAsync();
            return newTarget;
        }
        public async Task<List<Target>> CreateRange(List<Target> newTarget)
        {

            await _unitOfWork.Target.AddRange(newTarget);
            await _unitOfWork.CommitAsync();
            return newTarget;
        }
        public async Task<IEnumerable<Target>> GetAll()
        {
            return
                           await _unitOfWork.Target.GetAll();
        }
        public async Task<Doctor> GetDoctor(int IdDoctor)
        {
            var a=await _unitOfWork.Target.SingleOrDefault(i=>i.IdDoctor==IdDoctor);
            return a.IdDoctorNavigation;
        }
      

        public async Task<Target> GetById(int id)
        {
            return
                await _unitOfWork.Target.GetById(id);
        }
        public async Task<PotentielTotalSector> PotentielTotalSector(List<PharmacyResource>Pharmacys,List<DoctorResource> Doctors,int IdSector)
        {
            PotentielTotalSector PotentielTotalSector = new PotentielTotalSector();
            var Potentiel = await _unitOfWork.PotentielSectors.Find(i => i.IdSector == IdSector && i.Active == 0);
            List<Potentiel> Potentiels = new List<Potentiel>();
            List<object> list = new List<object>();
            list.Add(Doctors);
            list.Add(Pharmacys);
            PotentielTotalSector.NB = list.Count();
          foreach (var item in Potentiels) { 
            foreach (var doc in Doctors)
            {
                var Pot = await _unitOfWork.Potentiels.SingleOrDefault(i => i.IdPotentiel == doc.Potentiel.IdPotentiel && i.Active == 0);
                Potentiels.Add(Pot);
            }
            }
            return null;

        }

            public async Task<IEnumerable<Target>> GetByNumTarget(int id)
        {
            return
                await _unitOfWork.Target.Find(i=>i.NumTarget==id);
        }
        public async Task<IEnumerable<Sector>> GetSectorsByNumTarget(int id)
        {var Target = await _unitOfWork.Target.Find(i => i.NumTarget == id
        && i.Active==0 
        && i.IdPharmacyNavigation==null 
        && i.IdDoctorNavigation==null );
            List<Sector> Sectors = new List<Sector>();

            foreach (var item in Target)
            {
                Sector Sector = await _unitOfWork.Sectors.GetByIdActif(item.IdSector);
                if(!Sectors.Contains(Sector))
                {
                    Sectors.Add(Sector);
                }

            }
            return
                Sectors;
        }
        public async Task<IEnumerable<Sector>> GetFullSectorsByNumTarget(int id)
        {
          
            var AllTargets = await _unitOfWork.Target.Find(i => i.NumTarget == id
          && i.Active == 0
          && (i.IdPharmacyNavigation == null
          || i.IdDoctorNavigation == null));
            List<Sector> Sectors = new List<Sector>();

            foreach (var item in AllTargets)
            {
                Sector Sector = await _unitOfWork.Sectors.GetByIdActif(item.IdSector);
                if (!Sectors.Contains(Sector))
                {
                    Sectors.Add(Sector);
                }

            }
            return
                Sectors;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsByNumTarget(int NumTarget, int IdSector)
        {
            var Target = await _unitOfWork.Target.Find(i => i.NumTarget == NumTarget && i.IdSector == IdSector);
            List<Doctor> Doctors = new List<Doctor>();

            foreach (var item in Target)
            {   
                Doctor Doctor = await _unitOfWork.Doctors.SingleOrDefault(i=>i.IdDoctor==item.IdDoctor);
                if (!Doctors.Contains(Doctor) && Doctor!=null)
                {
                    Doctors.Add(Doctor);
                }

            }
            return
                Doctors;
        }
        public async Task<IEnumerable<Pharmacy>> GetPharmacysByNumTarget(int NumTarget,int IdSector)
        {
            var Target = await _unitOfWork.Target.Find(i => i.NumTarget == NumTarget&&i.IdSector==IdSector);
            List<Pharmacy> Pharmacys = new List<Pharmacy>();

            foreach (var item in Target)
            {
                Pharmacy Pharmacy = await _unitOfWork.Pharmacys.SingleOrDefault(i => i.IdPharmacy == item.IdPharmacy);
                if (!Pharmacys.Contains(Pharmacy) && Pharmacy != null)
                {
                    Pharmacys.Add(Pharmacy);
                }

            }
            return
                Pharmacys;
        }
        public async Task<Cycle> GetCycleByNumTarget(int id)
        {
            var Target = await _unitOfWork.Target.SingleOrDefault(i => i.NumTarget == id);
            Cycle Cycle = new Cycle();


            Cycle = await _unitOfWork.Cycles.GetByIdActif(Target.IdCycle);
         

            
            return
                Cycle;
        }
        public async Task<User> GetUserByNumTarget(int id)
        {
            var Target = await _unitOfWork.Target.SingleOrDefault(i => i.NumTarget == id);
            User User = new User();


            User = await _unitOfWork.Users.GetByIdActif(Target.IdUser);



            return
                User;
        }
        public async Task<IEnumerable<Target>> GetTargetsByIdUser(int id)
        {
            var Target = await _unitOfWork.Target.Find(i => i.IdUser == id
           && i.Active == 0
           && (i.IdPharmacyNavigation != null
           || i.IdDoctorNavigation != null));
            List<Target> DistinctTarget = new List<Target>();
            foreach(var item in Target)
            {
                if(!DistinctTarget.Contains(item))
                {
                    DistinctTarget.Add(item);
                }
            }
            return
                DistinctTarget;
        }
        public async Task<IEnumerable<Target>> GetTargets()
        {
            var Target = await _unitOfWork.Target.Find(i => i.Active == 0
           && (i.IdPharmacyNavigation != null
           || i.IdDoctorNavigation != null));
            List<Target> DistinctTarget = new List<Target>();
            foreach (var item in Target)
            {
                if (!DistinctTarget.Contains(item))
                {
                    DistinctTarget.Add(item);
                }
            }
            return
                DistinctTarget;
        }
            public async Task Update(Target CycleSectorWeekDoctorsToBeUpdated, Target CycleSectorWeekDoctors)
        {
            CycleSectorWeekDoctors.Active = 1;
            await _unitOfWork.CommitAsync();
        }
        public async Task WeekSwap(WeekSwap WeekSwap)
        {
            var Week2 = await _unitOfWork.Target.Find(i => i.NumTarget == WeekSwap.NumTarget && i.Active == 0
          && i.IdSector == WeekSwap.IdSector2 && (i.IdPharmacyNavigation != null || i.IdDoctorNavigation != null));

            var Week1 = await _unitOfWork.Target.Find(i => i.NumTarget == WeekSwap.NumTarget && i.Active== 0
            && i.IdSector== WeekSwap.IdSector1 && (i.IdPharmacyNavigation!=null||i.IdDoctorNavigation!=null) );
           
            IEnumerable<Target> Target1 = Week1;
            IEnumerable<Target> Target2 = Week2;
            List<Target> Tagets1Updated = new List<Target>();
            List<Target> Tagets2Updated = new List<Target>();

     
            foreach(var item in Target1)
            {
                item.IdSector = WeekSwap.IdSector2;
                Tagets1Updated.Add(item);
            }
            foreach (var item in Target2)
            {
                item.IdSector = WeekSwap.IdSector1;
                Tagets2Updated.Add(item);
            }
            _unitOfWork.Target.RemoveRange(Week1);
            await _unitOfWork.CommitAsync();
            _unitOfWork.Target.RemoveRange(Week2);
            await _unitOfWork.CommitAsync();

            await _unitOfWork.Target.AddRange(Tagets1Updated);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.Target.AddRange(Tagets2Updated);
            await _unitOfWork.CommitAsync();

    
            // foreach(var item in)
      
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteWeek(WeekDeletion WeekDeletion)
        {
            var Week1 = await _unitOfWork.Target.Find(i => i.NumTarget == WeekDeletion.NumTarget && i.Active == 0
            && i.IdSector == WeekDeletion.IdSector1
            && (i.IdPharmacyNavigation != null || i.IdDoctorNavigation != null));
            foreach(var item in Week1)
                    {
                        item.Active = 1;
                        await _unitOfWork.CommitAsync();
                    }
        }
        public async Task RemoveAll()
        {
            var Week1 = await _unitOfWork.Target.GetAll();
       
                _unitOfWork.Target.RemoveRange(Week1);
                await _unitOfWork.CommitAsync();

           

        }
        public async Task UpdateWeek(WeekUpdate WeekUpdate)
        {
           


        }
        public async Task Delete(Target Target)
        {
            //CycleSectorWeekDoctors musi =  _unitOfWork.CycleSectorWeekDoctors.SingleOrDefaultAsync(x=>x.Id == CycleSectorWeekDoctorsToBeUpdated.Id);
            Target.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Target> CycleSectorWeekDoctors)
        {
            foreach (var item in CycleSectorWeekDoctors)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Target>> GetAllActif()
        {
            return
                             await _unitOfWork.Target.GetAllActif();
        }

        public async Task<IEnumerable<Target>> GetAllInActif()
        {
            return
                             await _unitOfWork.Target.GetAllInActif();
        }
        //public Task<CycleSectorWeekDoctors> CreateCycleSectorWeekDoctors(CycleSectorWeekDoctors newTarget)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteCycleSectorWeekDoctors(CycleSectorWeekDoctors CycleSectorWeekDoctors)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<CycleSectorWeekDoctors> GetCycleSectorWeekDoctorsById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<CycleSectorWeekDoctors>> GetCycleSectorWeekDoctorssByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateCycleSectorWeekDoctors(CycleSectorWeekDoctors CycleSectorWeekDoctorsToBeUpdated, CycleSectorWeekDoctors CycleSectorWeekDoctors)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
