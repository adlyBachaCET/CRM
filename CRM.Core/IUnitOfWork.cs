using CRM.Core.Repository;
using System;
using System.Threading.Tasks;

namespace CRM.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointementRepository Appointements { get; }

        IPlanificationRepository Planifications { get; }
        IActivityRepository Activitys { get; }
        IActivityUserRepository ActivityUsers { get; }

        IProductPharmacyRepository ProductPharmacys { get; }
        IProductRepository Products { get; }
        IProductSellingObjectivesRepository ProductSellingObjectivess { get; }
        ISellingObjectivesRepository SellingObjectivess { get; }

        IVisitUserRepository VisitUsers { get; }
        IParticipantRepository Participants { get; }
        IRequestRpRepository RequestRps { get; }
        ICommandeRepository Commandes { get; }
        ISupportRepository Support { get; }

        IBrickRepository Bricks { get; }
        IObjectionRepository Objections { get; }
        IRequestDoctorRepository RequestDoctors { get; }

        IVisitReportRepository VisitReports { get; }
        IVisitRepository Visits { get; }

        IBuDoctorRepository BuDoctors { get; }
        IBusinessUnitRepository BusinessUnits { get; }
        IBuUserRepository BuUsers { get; }
        ICycleBuRepository CycleBus { get; }
        ICycleUserRepository CycleUsers { get; }

        ICycleRepository Cycles { get; }
        ITargetRepository Target { get; }
        IDoctorRepository Doctors{ get; }
        ILocationDoctorRepository LocationDoctors { get; }
        IAdresseRepository Adresses { get; }
        IAdresseLocalityRepository AdresseLocalitys { get; }

        ILocationRepository Locations { get; }
        IUserRepository Users { get; }

        ILocationTypeRepository LocationTypes { get; }
        IInfoRepository Infos { get; }
        ILocalityRepository Localitys { get; }
        IPharmacyRepository Pharmacys { get; }
        IPhoneRepository Phones { get; }
        IPotentielCycleRepository PotentielCycles { get; }
        ISectorLocalityRepository SectorLocalitys { get; }
        IPotentielRepository Potentiels { get; }
        IPotentielSectorRepository PotentielSectors { get; }

        ISectorRepository Sectors { get; }
        IServiceRepository Services { get; }
        ISpecialtyRepository Specialtys { get; }
        ITagsDoctorRepository TagsDoctors { get; }
        ITagsRepository Tagss { get; }

        IWeekInYearRepository WeekInYears { get; }
        IWholeSalerLocalityRepository WholeSalerLocalitys { get; }
        IWholeSalerRepository WholeSales { get; }
        ISectorInYearRepository SectorInYear { get; }
        ISectorCycleRepository WeekSectorCycles { get; }

        Task<int> CommitAsync();
    }
}
