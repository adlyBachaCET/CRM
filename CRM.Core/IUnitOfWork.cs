using CRM.Core.Repository;
using System;
using System.Threading.Tasks;

namespace CRM.Core
{
    public interface IUnitOfWork : IDisposable
    {
    
        IBrickRepository Bricks { get; }


        IBrickLocalityRepository BrickLocalitys { get; }
        IBuDoctorRepository BuDoctors { get; }
        IBusinessUnitRepository BusinessUnits { get; }
        IBuUserRepository BuUsers { get; }
        ICycleBuRepository CycleBus { get; }
        ICycleRepository Cycles { get; }
        ITargetRepository CycleSectorWeekDoctors { get; }
        IDoctorRepository Doctors{ get; }
        ILocationDoctorRepository EstablishmentDoctors { get; }
        IAdresseRepository Adresses { get; }
        IAdresseLocalityRepository AdresseLocalitys { get; }

        ILocationRepository Establishments { get; }
        IUserRepository Users { get; }

        ILocationTypeRepository EstablishmentTypes { get; }
        IInfoRepository Infos { get; }
        ILocalityRepository Localitys { get; }
        IPharmacyRepository Pharmacys { get; }
        IPhoneRepository Phones { get; }
        IPotentielCycleRepository PotentielCycles { get; }
        ISectorLocalityRepository SectorLocalitys { get; }
        IPotentielRepository Potentiels { get; }
        ISectorRepository Sectors { get; }
        IServiceRepository Services { get; }
        ISpecialtyRepository Specialtys { get; }
        ITagsDoctorRepository TagsDoctors { get; }
        ITagsRepository Tagss { get; }

        IWeekInYearRepository WeekInYears { get; }
        IWholeSalerLocalityRepository WholeSalerLocalitys { get; }
        IWholeSalerRepository WholeSales { get; }
        ISectorCycleInYearRepository WeekSectorCycleInYears { get; }
        ISectorCycleRepository WeekSectorCycles { get; }

        Task<int> CommitAsync();
    }
}
