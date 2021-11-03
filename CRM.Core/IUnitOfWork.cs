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
        ICycleSectorWeekDoctorsRepository CycleSectorWeekDoctors { get; }
        IDelegateManagerRepository DelegateManagers { get; }
        IDoctorRepository Doctors{ get; }
        IEstablishmentDoctorRepository EstablishmentDoctors { get; }
        IEstablishmentLocalityRepository EstablishmentLocalitys { get; }
        IEstablishmentRepository Establishments { get; }
        IEstablishmentServiceRepository EstablishmentServices { get; }
        IEstablishmentUserRepository EstablishmentUsers { get; }
        IUserRepository Users { get; }

        IEstablishmentTypeRepository EstablishmentTypes { get; }
        IInfoRepository Infos { get; }
        ILocalityRepository Localitys { get; }
        IPharmacyLocalityRepository PharmacyLocalitys { get; }
        IPharmacyRepository Pharmacys { get; }
        IPhoneRepository Phones { get; }
        IPotentielCycleRepository PotentielCycles { get; }
        ISectorLocalityRepository SectorLocalitys { get; }
        IPotentielRepository Potentiels { get; }
        ISectorRepository Sectors { get; }
        IServiceDoctorRepository ServiceDoctors { get; }
        IServiceRepository Services { get; }
        ISpecialityDoctorRepository SpecialityDoctors { get; }
        ISpecialtyRepository Specialtys { get; }

        IWeekInCycleRepository WeekInCycles { get; }
        IWeekInYearRepository WeekInYears { get; }
        IWholeSalerLocalityRepository WholeSalerLocalitys { get; }
        IWholeSalerRepository WholeSales { get; }
        IWeekSectorCycleInYearRepository WeekSectorCycleInYears { get; }

        Task<int> CommitAsync();
    }
}
