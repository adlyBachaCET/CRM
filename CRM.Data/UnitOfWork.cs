using CRM.Core;
using CRM.Core.Repository;
using CRM.Data.Repositories;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        private IParticipantRepository _ParticipantRepository;
        private IActivityRepository _ActivityRepository;
        private IActivityUserRepository _ActivityUserRepository;
        private IAppointementRepository _AppointementRepository;

        private IProductRepository _ProductRepository;
        private IProductSampleRepository _ProductSampleRepository;
        private IProductPharmacyRepository _ProductPharmacyRepository;
        private IPlanificationRepository _PlanificationRepository;


        private IRequestRpRepository _RequestRpRepository;
        private ICommandeRepository _CommandeRepository;
        private ISupportRepository _SupportRepository;

        private IObjectionRepository _ObjectionRepository;
        private IVisitUserRepository _VisitUserRepository;

        private IBrickRepository _BrickRepository;
        private IVisitReportRepository _VisitReportRepository;
        private IVisitRepository _VisitRepository;
        private IRequestDoctorRepository _RequestDoctorRepository;

        private IBuDoctorRepository _BuDoctorRepository;
        private IBusinessUnitRepository _BusinessUnitRepository;
        private IBuUserRepository _BuUserRepository;
        private ICycleUserRepository _CycleUserRepository;

        private ICycleBuRepository _CycleBuRepository;
        private ICycleRepository _CycleRepository;
        private ITargetRepository _CycleSectorWeekDoctorRepository;
        private IDoctorRepository _DoctorRepository;
        private ILocationDoctorRepository _EstablishmentDoctorRepository;
        private IAdresseRepository _AdresseRepository;

        private ILocationRepository _EstablishmentRepository;
        private ILocationTypeRepository _EstablishmentTypeRepository;
        private IInfoRepository _InfoRepository;
        private ILocalityRepository _LocalityRepository;
        private IAdresseLocalityRepository _AdresseLocalityRepository;

        private IPharmacyRepository _PharmacyRepository;
        private IPhoneRepository _PhoneRepository;
        private ISectorLocalityRepository _SectorLocalityRepository;
        private IPotentielRepository _PotentielRepository;
        private IPotentielCycleRepository _PotentielCycleRepository;
        private IPotentielSectorRepository _PotentielSectorRepository;

        private ISectorRepository _SectorRepository;
        private IServiceRepository _ServiceRepository;
        private ISpecialtyRepository _SpecialtyRepository;
        private ITagsRepository _TagsRepository;
        private ITagsDoctorRepository _TagsDoctorRepository;
        private IWeekInYearRepository _WeekInYearRepository;
        private IWholeSalerLocalityRepository _WholeSalerLocalityRepository;
        private IWholeSalerRepository _WholeSaleRepository;
        private IUserRepository _UserRepository;
        private ISectorCycleInYearRepository _WeekSectorCycleInYearRepository;

        private ISectorCycleRepository _WeekSectorCycleRepository;


        //        public IUserRepository Users => throw new NotImplementedException();
        public IBrickRepository Bricks => _BrickRepository = _BrickRepository ?? new BrickRepository(_context);
        public IVisitReportRepository VisitReports => _VisitReportRepository = _VisitReportRepository ?? new VisitReportRepository(_context);
        public IVisitRepository Visits => _VisitRepository = _VisitRepository ?? new VisitRepository(_context);
        public IRequestDoctorRepository RequestDoctors => _RequestDoctorRepository = _RequestDoctorRepository ?? new RequestDoctorRepository(_context);
        public IProductSampleRepository ProductSamples => _ProductSampleRepository = _ProductSampleRepository ?? new ProductSampleRepository(_context);

        public IObjectionRepository Objections => _ObjectionRepository = _ObjectionRepository ?? new ObjectionRepository(_context);
        public IVisitUserRepository VisitUsers => _VisitUserRepository = _VisitUserRepository ?? new VisitUserRepository(_context);
        public IProductRepository Products => _ProductRepository = _ProductRepository ?? new ProductRepository(_context);
        public IProductPharmacyRepository ProductPharmacys => _ProductPharmacyRepository = _ProductPharmacyRepository ?? new ProductPharmacyRepository(_context);
        public IPlanificationRepository Planifications => _PlanificationRepository = _PlanificationRepository ?? new PlanificationRepository(_context);
        public IActivityRepository Activitys => _ActivityRepository = _ActivityRepository ?? new ActivityRepository(_context);
        public IActivityUserRepository ActivityUsers => _ActivityUserRepository = _ActivityUserRepository ?? new ActivityUserRepository(_context);
        public IAppointementRepository Appointements => _AppointementRepository = _AppointementRepository ?? new AppointementRepository(_context);

        public IParticipantRepository Participants => _ParticipantRepository = _ParticipantRepository ?? new ParticipantRepository(_context);
        public IRequestRpRepository RequestRps => _RequestRpRepository = _RequestRpRepository ?? new RequestRpRepository(_context);
        public ICommandeRepository Commandes => _CommandeRepository = _CommandeRepository ?? new CommandeRepository(_context);
        public ISupportRepository Support => _SupportRepository = _SupportRepository ?? new SupportRepository(_context);

        public ISectorCycleInYearRepository WeekSectorCycleInYears => _WeekSectorCycleInYearRepository = _WeekSectorCycleInYearRepository ?? new SectorCycleInYearRepository(_context);
        public ISectorCycleRepository WeekSectorCycles => _WeekSectorCycleRepository = _WeekSectorCycleRepository ?? new SectorCycleRepository(_context);


        public IBuDoctorRepository BuDoctors =>_BuDoctorRepository = _BuDoctorRepository ?? new BuDoctorRepository(_context);

        public IBusinessUnitRepository BusinessUnits =>_BusinessUnitRepository = _BusinessUnitRepository ?? new BusinessUnitRepository(_context);

        public IBuUserRepository BuUsers =>_BuUserRepository = _BuUserRepository ?? new BuUserRepository(_context);
        public ICycleUserRepository CycleUsers => _CycleUserRepository = _CycleUserRepository ?? new CycleUserRepository(_context);

        public ICycleBuRepository CycleBus =>_CycleBuRepository = _CycleBuRepository ?? new CycleBuRepository(_context);

        public ICycleRepository Cycles =>_CycleRepository = _CycleRepository ?? new CycleRepository(_context);

        public ITargetRepository CycleSectorWeekDoctors =>_CycleSectorWeekDoctorRepository = _CycleSectorWeekDoctorRepository ?? new TargetRepository(_context);


        public IDoctorRepository Doctors =>_DoctorRepository = _DoctorRepository ?? new DoctorRepository(_context);

        public ILocationDoctorRepository LocationDoctors =>_EstablishmentDoctorRepository = _EstablishmentDoctorRepository ?? new LocationDoctorRepository(_context);

        public IAdresseRepository Adresses => _AdresseRepository = _AdresseRepository ?? new AdresseRepository(_context);

        public ILocationRepository Locations =>_EstablishmentRepository = _EstablishmentRepository ?? new LocationRepository(_context);

        public IUserRepository Users => _UserRepository = _UserRepository ?? new UserRepository(_context);


        public ILocationTypeRepository LocationTypes =>_EstablishmentTypeRepository = _EstablishmentTypeRepository ?? new LocationTypeRepository(_context);

        public IInfoRepository Infos =>_InfoRepository = _InfoRepository ?? new InfoRepository(_context);

        public ILocalityRepository Localitys =>_LocalityRepository = _LocalityRepository ?? new LocalityRepository(_context);

        public IAdresseLocalityRepository AdresseLocalitys => _AdresseLocalityRepository = _AdresseLocalityRepository ?? new AdresseLocalityRepository(_context);

        public IPharmacyRepository Pharmacys =>_PharmacyRepository = _PharmacyRepository ?? new PharmacyRepository(_context);

        public IPhoneRepository Phones =>_PhoneRepository = _PhoneRepository ?? new PhoneRepository(_context);

        public IPotentielCycleRepository PotentielCycles =>_PotentielCycleRepository = _PotentielCycleRepository ?? new PotentielCycleRepository(_context);
        public IPotentielSectorRepository PotentielSectors => _PotentielSectorRepository = _PotentielSectorRepository ?? new PotentielSectorRepository(_context);

        public ISectorLocalityRepository SectorLocalitys =>_SectorLocalityRepository = _SectorLocalityRepository ?? new SectorLocalityRepository(_context);

        public IPotentielRepository Potentiels =>_PotentielRepository = _PotentielRepository ?? new PotentielRepository(_context);

        public ISectorRepository Sectors =>_SectorRepository = _SectorRepository ?? new SectorRepository(_context);


        public IServiceRepository Services =>_ServiceRepository = _ServiceRepository ?? new ServiceRepository(_context);
        public ITagsRepository Tagss => _TagsRepository = _TagsRepository ?? new TagsRepository(_context);

        public ISpecialtyRepository Specialtys =>_SpecialtyRepository = _SpecialtyRepository ?? new SpecialtyRepository(_context);
        public ITagsDoctorRepository TagsDoctors => _TagsDoctorRepository = _TagsDoctorRepository ?? new TagsDoctorRepository(_context);


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
