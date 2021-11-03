using CRM.Core;
using CRM.Core.Repository;
using CRM.Data.Repositories;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
       

        private IBrickRepository _BrickRepository;
        private IBrickLocalityRepository _BrickLocalityRepository;
        private IBuDoctorRepository _BuDoctorRepository;
        private IBusinessUnitRepository _BusinessUnitRepository;
        private IBuUserRepository _BuUserRepository;
        private ICycleBuRepository _CycleBuRepository;
        private ICycleRepository _CycleRepository;
        private ICycleSectorWeekDoctorsRepository _CycleSectorWeekDoctorRepository;
        private IDelegateManagerRepository _DelegateManagerRepository;
        private IDoctorRepository _DoctorRepository;
        private IEstablishmentDoctorRepository _EstablishmentDoctorRepository;
        private IEstablishmentLocalityRepository _EstablishmentLocalityRepository;
        private IEstablishmentRepository _EstablishmentRepository;
        private IEstablishmentServiceRepository _EstablishmentServiceRepository;
        private IEstablishmentUserRepository _EstablishmentUserRepository;
        private IEstablishmentTypeRepository _EstablishmentTypeRepository;
        private IInfoRepository _InfoRepository;
        private ILocalityRepository _LocalityRepository;
        private IPharmacyLocalityRepository _PharmacyLocalityRepository;
        private IPharmacyRepository _PharmacyRepository;
        private IPhoneRepository _PhoneRepository;
        private ISectorLocalityRepository _SectorLocalityRepository;
        private IPotentielRepository _PotentielRepository;
        private IPotentielCycleRepository _PotentielCycleRepository;

        private ISectorRepository _SectorRepository;
        private IServiceDoctorRepository _ServiceDoctorRepository;
        private IServiceRepository _ServiceRepository;
        private ISpecialtyRepository _SpecialtyRepository;
        private ISpecialityDoctorRepository _SpecialityDoctorRepository;

        private IWeekInCycleRepository _WeekInCycleRepository;
        private IWeekInYearRepository _WeekInYearRepository;
        private IWholeSalerLocalityRepository _WholeSalerLocalityRepository;
        private IWholeSalerRepository _WholeSaleRepository;
        private IUserRepository _UserRepository;
        private IWeekSectorCycleInYearRepository _WeekSectorCycleInYearRepository;



        //        public IUserRepository Users => throw new NotImplementedException();
        public IBrickRepository Bricks => _BrickRepository = _BrickRepository ?? new BrickRepository(_context);
        public IWeekSectorCycleInYearRepository WeekSectorCycleInYears => _WeekSectorCycleInYearRepository = _WeekSectorCycleInYearRepository ?? new WeekSectorCycleInYearRepository(_context);

        public IBrickLocalityRepository BrickLocalitys =>_BrickLocalityRepository = _BrickLocalityRepository ?? new BrickLocalityRepository(_context);

        public IBuDoctorRepository BuDoctors =>_BuDoctorRepository = _BuDoctorRepository ?? new BuDoctorRepository(_context);

        public IBusinessUnitRepository BusinessUnits =>_BusinessUnitRepository = _BusinessUnitRepository ?? new BusinessUnitRepository(_context);

        public IBuUserRepository BuUsers =>_BuUserRepository = _BuUserRepository ?? new BuUserRepository(_context);

        public ICycleBuRepository CycleBus =>_CycleBuRepository = _CycleBuRepository ?? new CycleBuRepository(_context);

        public ICycleRepository Cycles =>_CycleRepository = _CycleRepository ?? new CycleRepository(_context);

        public ICycleSectorWeekDoctorsRepository CycleSectorWeekDoctors =>_CycleSectorWeekDoctorRepository = _CycleSectorWeekDoctorRepository ?? new CycleSectorWeekDoctorsRepository(_context);

        public IDelegateManagerRepository DelegateManagers =>_DelegateManagerRepository = _DelegateManagerRepository ?? new DelegateManagerRepository(_context);

        public IDoctorRepository Doctors =>_DoctorRepository = _DoctorRepository ?? new DoctorRepository(_context);

        public IEstablishmentDoctorRepository EstablishmentDoctors =>_EstablishmentDoctorRepository = _EstablishmentDoctorRepository ?? new EstablishmentDoctorRepository(_context);

        public IEstablishmentLocalityRepository EstablishmentLocalitys =>_EstablishmentLocalityRepository = _EstablishmentLocalityRepository ?? new EstablishmentLocalityRepository(_context);

        public IEstablishmentRepository Establishments =>_EstablishmentRepository = _EstablishmentRepository ?? new EstablishmentRepository(_context);

        public IEstablishmentServiceRepository EstablishmentServices =>_EstablishmentServiceRepository = _EstablishmentServiceRepository ?? new EstablishmentServiceRepository(_context);
        public IUserRepository Users => _UserRepository = _UserRepository ?? new UserRepository(_context);

        public IEstablishmentUserRepository EstablishmentUsers =>_EstablishmentUserRepository = _EstablishmentUserRepository ?? new EstablishmentUserRepository(_context);

        public IEstablishmentTypeRepository EstablishmentTypes =>_EstablishmentTypeRepository = _EstablishmentTypeRepository ?? new EstablishmentTypeRepository(_context);

        public IInfoRepository Infos =>_InfoRepository = _InfoRepository ?? new InfoRepository(_context);

        public ILocalityRepository Localitys =>_LocalityRepository = _LocalityRepository ?? new LocalityRepository(_context);

        public IPharmacyLocalityRepository PharmacyLocalitys =>_PharmacyLocalityRepository = _PharmacyLocalityRepository ?? new PharmacyLocalityRepository(_context);

        public IPharmacyRepository Pharmacys =>_PharmacyRepository = _PharmacyRepository ?? new PharmacyRepository(_context);

        public IPhoneRepository Phones =>_PhoneRepository = _PhoneRepository ?? new PhoneRepository(_context);

        public IPotentielCycleRepository PotentielCycles =>_PotentielCycleRepository = _PotentielCycleRepository ?? new PotentielCycleRepository(_context);

        public ISectorLocalityRepository SectorLocalitys =>_SectorLocalityRepository = _SectorLocalityRepository ?? new SectorLocalityRepository(_context);

        public IPotentielRepository Potentiels =>_PotentielRepository = _PotentielRepository ?? new PotentielRepository(_context);

        public ISectorRepository Sectors =>_SectorRepository = _SectorRepository ?? new SectorRepository(_context);

        public IServiceDoctorRepository ServiceDoctors =>_ServiceDoctorRepository = _ServiceDoctorRepository ?? new ServiceDoctorRepository(_context);

        public IServiceRepository Services =>_ServiceRepository = _ServiceRepository ?? new ServiceRepository(_context);

        public ISpecialtyRepository Specialtys =>_SpecialtyRepository = _SpecialtyRepository ?? new SpecialtyRepository(_context);
        public ISpecialityDoctorRepository SpecialityDoctors => _SpecialityDoctorRepository = _SpecialityDoctorRepository ?? new SpecialityDoctorRepository(_context);

        public IWeekInCycleRepository WeekInCycles =>_WeekInCycleRepository = _WeekInCycleRepository ?? new WeekInCycleRepository(_context);

        public IWeekInYearRepository WeekInYears =>_WeekInYearRepository = _WeekInYearRepository ?? new WeekInYearRepository(_context);

        public IWholeSalerLocalityRepository WholeSalerLocalitys =>_WholeSalerLocalityRepository = _WholeSalerLocalityRepository ?? new WholeSalerLocalityRepository(_context);

        public IWholeSalerRepository WholeSales =>_WholeSaleRepository = _WholeSaleRepository ?? new WholeSalerRepository(_context);

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
