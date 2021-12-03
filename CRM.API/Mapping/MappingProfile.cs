using AutoMapper;
using CRM.Core.Models;
using CRM_API.Resources;
using System.Collections.Generic;

namespace CRM_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Target
            CreateMap<Target, TargetResource>();
            CreateMap<Target, SaveTargetResource>();
            CreateMap<SaveTargetResource, Target>();
            CreateMap<SaveTargetResource, TargetResource>();
            CreateMap<TargetResource, Target>();

            CreateMap<Target, IEnumerable<TargetResource>>();
            //Target
            CreateMap<Appointement, AppointementResource>().
                ForMember(dest => dest.Pharmacy, opt => opt.MapFrom(src => src.Pharmacy)).
                ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor)).
                ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<Appointement, SaveAppointementResource>();
            CreateMap<Appointement, SaveAppointementResource>();
            CreateMap<SaveAppointementResource, Appointement>();
            CreateMap<Appointement, IEnumerable<AppointementResource>>();
            //VisitUser
            CreateMap<VisitUser, VisitUserResource>();
            CreateMap<VisitUser, SaveVisitUserResource>();
            CreateMap<VisitUser, SaveVisitUserResource>();
            CreateMap<SaveVisitUserResource, VisitUser>();
            CreateMap<VisitUser, IEnumerable<VisitUserResource>>();
            //ActivityUser
            CreateMap<ActivityUser, ActivityUserResource>();
            CreateMap<ActivityUser, SaveActivityUserResource>();
            CreateMap<ActivityUser, SaveActivityUserResource>();
            CreateMap<SaveActivityUserResource, ActivityUser>();
            CreateMap<ActivityUser, IEnumerable<ActivityUserResource>>();
            //Activity
            CreateMap<Activity, ActivityResource>();
            CreateMap<Activity, SaveActivityResource>();
            CreateMap<Activity, SaveActivityResource>();
            CreateMap<SaveActivityResource, Activity>();
            CreateMap<Activity, IEnumerable<ActivityResource>>();
            //Brick
            CreateMap<Brick, BrickResource>();
            CreateMap<Brick, SaveBrickResource>();
            CreateMap<Brick, SaveBrickResource>();
            CreateMap<SaveBrickResource, Brick>();
            CreateMap<Brick, IEnumerable<BrickResource>>();
 
            //ProductPharmacy
            CreateMap<ProductPharmacy, ProductPharmacyResource>();
            CreateMap<ProductPharmacy, SaveProductPharmacyResource>();
            CreateMap<ProductPharmacy, SaveProductPharmacyResource>();
            CreateMap<SaveProductPharmacyResource, ProductPharmacy>();
            CreateMap<ProductPharmacy, IEnumerable<ProductPharmacyResource>>();
            //Product
            CreateMap<Product, ProductResource>();
            CreateMap<Product, SaveProductResource>();
            CreateMap<Product, SaveProductResource>();
            CreateMap<SaveProductResource, Product>();
            CreateMap<Product, IEnumerable<ProductResource>>();
            //SellingObjectives
            CreateMap<SellingObjectives, SellingObjectivesResource>();
            CreateMap<SellingObjectives, SaveSellingObjectivesResource>();
            CreateMap<SellingObjectives, SaveSellingObjectivesResource>();
            CreateMap<SaveSellingObjectivesResource, SellingObjectives>();
            CreateMap<SellingObjectives, IEnumerable<SellingObjectivesResource>>();

            //RequestRp
            CreateMap<RequestRp, RequestRpResource>();
            CreateMap<RequestRp, SaveRequestRpResource>();
            CreateMap<RequestRp, SaveRequestRpResource>();
            CreateMap<SaveRequestRpResource, RequestRp>();
            CreateMap<RequestRp, IEnumerable<RequestRpResource>>();
            //Participant
            CreateMap<Participant, ParticipantResource>();
            CreateMap<Participant, SaveParticipantResource>();
            CreateMap<Participant, SaveParticipantResource>();
            CreateMap<SaveParticipantResource, Participant>();
            CreateMap<Participant, IEnumerable<ParticipantResource>>();

            //Objection
            CreateMap<Objection, ObjectionResource>();
            CreateMap<Objection, SaveObjectionResource>();
            CreateMap<Objection, SaveObjectionResource>();
            CreateMap<SaveObjectionResource, Objection>();
            CreateMap<Objection, IEnumerable<ObjectionResource>>();
            //Visit
            CreateMap<Visit, VisitResource>();
            CreateMap<Visit, SaveVisitResource>();
            CreateMap<Visit, SaveVisitResource>();
            CreateMap<SaveVisitResource, Visit>();
            CreateMap<Visit, IEnumerable<VisitResource>>();
            //VisitReport
            CreateMap<VisitReport, VisitReportResource>();
            CreateMap<VisitReport, SaveVisitReportResource>();
            CreateMap<VisitReport, SaveVisitReportResource>();
            CreateMap<SaveVisitReportResource, VisitReport>();
            CreateMap<VisitReport, IEnumerable<VisitReportResource>>();
            //Specialty
            CreateMap<Specialty, SpecialtyResource>();
            CreateMap<Specialty, SaveSpecialtyResource>();
            CreateMap<Specialty, SaveSpecialtyResource>();
            CreateMap<SaveSpecialtyResource, Specialty>();
            CreateMap<Specialty, IEnumerable<SpecialtyResource>>();
            //Pharmacy
            CreateMap<Pharmacy, PharmacyResource>();
            CreateMap<Pharmacy, SavePharmacyResource>();
            CreateMap<SavePharmacyResource, Pharmacy>();
            CreateMap<Pharmacy, IEnumerable<PharmacyResource>>();
            //Locality
            CreateMap<Locality, LocalityResource>();
            CreateMap<Locality, SaveLocalityResource>();
            CreateMap<SaveLocalityResource, Locality>();
            CreateMap<Locality, IEnumerable<LocalityResource>>();
 
            //BuUser
            CreateMap<BuUser, BuUserResource>();
            CreateMap<BuUser, SaveBuUserResource>();
            CreateMap<SaveBuUserResource, BuUser>();
            CreateMap<BuUser, IEnumerable<BuUserResource>>();
            //CycleUser
            CreateMap<CycleUser, CycleUserResource>();
            CreateMap<CycleUser, SaveCycleUserResource>();
            CreateMap<SaveCycleUserResource, CycleUser>();
            CreateMap<CycleUser, IEnumerable<CycleUserResource>>();
            //User
            CreateMap<User, UserResource>();
            CreateMap<User, UpdateUserResource>();
            CreateMap<UpdateUserResource, User>();
            CreateMap<User, SaveUserResource>();
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveUserResource, SaveUserResourceWithoutPassword>();
            CreateMap< IEnumerable<User>, IEnumerable < UserResource >>();
            CreateMap<IEnumerable<User>, UserResource>();
            CreateMap<SaveUserResourceWithoutPassword, User > ();
            CreateMap<SaveUserResourceWithoutPasswordUpdate, User>();

            CreateMap<User, IEnumerable<UserResource>>();
            CreateMap<IEnumerable<User>, IEnumerable<SaveUserResource>>();

            //Doctor
            CreateMap<Doctor, DoctorResource>();

            CreateMap<Doctor, SaveDoctorResource>();
            CreateMap<SaveDoctorResource, Doctor>();
            CreateMap<SaveDoctorResourceUpdate, Doctor>();

            CreateMap<Doctor, IEnumerable<DoctorResource>>();
            CreateMap<List<Doctor>, List<SaveDoctorResource>>();

            //BusinessUnit
            CreateMap<BusinessUnit, BusinessUnitResource>();
            CreateMap<BusinessUnit, SaveBusinessUnitResource>();
            CreateMap<SaveBusinessUnitResource, BusinessUnit>();
            CreateMap<BusinessUnit, IEnumerable<BusinessUnitResource>>();
            CreateMap<List<SaveBusinessUnitResource>, List<BusinessUnit>>();
            CreateMap< List < BusinessUnit > , List<SaveBusinessUnitResource>>();
            CreateMap<List<BusinessUnit>, BusinessUnitResource>();

            //LocationDoctor
            CreateMap<LocationDoctor, LocationDoctorResource>();
            CreateMap<LocationDoctor, SaveLocationDoctorResource>();
            CreateMap<SaveLocationDoctorResource, LocationDoctor>();
            CreateMap<List<SaveLocationDoctorResource>, List<LocationDoctor>>();
            CreateMap<LocationDoctor, IEnumerable<LocationDoctorResource>>();
            //Commande
            CreateMap<Commande, CommandeResource>();
            CreateMap<Commande, SaveCommandeResource>();
            CreateMap<SaveCommandeResource, Commande>();
            CreateMap<List<SaveCommandeResource>, List<Commande>>();
            CreateMap<Commande, IEnumerable<CommandeResource>>();
            //Location
            CreateMap<Location, LocationResource>();
            CreateMap<LocationAdd, Location>();

            CreateMap<Location, SaveLocationResource>();
            CreateMap<SaveLocationResource, Location>();
            CreateMap < List<SaveLocationResource>, List<Location>>();
            CreateMap<Location, IEnumerable<LocationResource>>();
            //Support
            CreateMap<Support, SupportResource>();
            CreateMap<Support, SaveSupportResource>();
            CreateMap<SaveSupportResource, Support>();
            CreateMap<List<SaveSupportResource>, List<Support>>();
            CreateMap<Support, IEnumerable<SupportResource>>();
            //LocationType
            CreateMap<LocationType, LocationTypeResource>();
            CreateMap<LocationType, SaveLocationTypeResource>();
            CreateMap<SaveLocationTypeResource, LocationType>();
            CreateMap<List<SaveLocationTypeResource>, List<LocationType>>();
            CreateMap<LocationType, IEnumerable<LocationTypeResource>>();
            //Info
            CreateMap<Info, InfoResource>();
            CreateMap<Info, SaveInfoResource>();
            CreateMap<SaveInfoResource, Info>();
            CreateMap<List<SaveInfoResource>, List<Info>>();
            CreateMap<Info, IEnumerable<InfoResource>>();
            //Tags
            CreateMap<Tags, TagsResource>();
            CreateMap<Tags, SaveTagsResource>();
            CreateMap<SaveTagsResource, Tags>();
            CreateMap<List<SaveTagsResource>, List<Tags>>();
            CreateMap<IEnumerable<Tags>, IEnumerable<TagsResource>>();
            CreateMap<IEnumerable<Info>, IEnumerable<InfoResource>>();
            CreateMap<IEnumerable<Visit>, IEnumerable<VisitResource>>();
            CreateMap<IEnumerable<RequestRp>, IEnumerable<RequestRpResource>>();
            CreateMap<IEnumerable<Commande>, IEnumerable<CommandeResource>>();
            CreateMap<IEnumerable<Objection>, IEnumerable<ObjectionResource>>();
            CreateMap<IEnumerable<BusinessUnit>, IEnumerable<BusinessUnitResource>>();
            CreateMap<IEnumerable<LocationDoctor>, IEnumerable<LocationDoctorResource>>();
            CreateMap<IEnumerable<Phone>, IEnumerable<PhoneResource>>();
            CreateMap<IEnumerable<VisitReport>, IEnumerable<VisitReportResource>>();
            CreateMap<IEnumerable<Visit>, IEnumerable<VisitReport>>();

            CreateMap<IEnumerable<User>, IEnumerable<UserResource>>();

            CreateMap< IEnumerable < TagsResource > , IEnumerable<Tags>>();
            CreateMap< IEnumerable < InfoResource > , IEnumerable<Info>>();
            CreateMap< IEnumerable < VisitResource > ,IEnumerable<Visit> >();
            CreateMap< IEnumerable<RequestRpResource> ,IEnumerable<RequestRp>>();
            CreateMap<IEnumerable<CommandeResource> ,IEnumerable<Commande>>();
            CreateMap < IEnumerable < ObjectionResource > ,IEnumerable<Objection>>();
            CreateMap < IEnumerable < BusinessUnitResource > ,IEnumerable <BusinessUnit>>();
            CreateMap< IEnumerable < LocationDoctorResource > ,IEnumerable <LocationDoctor>>();
            CreateMap < IEnumerable < PhoneResource > ,IEnumerable <Phone>>();
            CreateMap<IEnumerable<VisitReportResource>, IEnumerable<VisitReport>>();
            CreateMap < IEnumerable < VisitReport > ,IEnumerable <Visit>>();
            CreateMap < IEnumerable < UserResource >, IEnumerable <User>>();
            CreateMap<Tags, IEnumerable<TagsResource>>();
            CreateMap<TagsResource, Tags>();

            //TagsDoctor
            CreateMap<TagsDoctor, TagsDoctorResource>();
            CreateMap<TagsDoctor, SaveTagsDoctorResource>();
            CreateMap<SaveTagsDoctorResource, TagsDoctor>();
            CreateMap<List<SaveTagsDoctorResource>, List<TagsDoctor>>();
            CreateMap<TagsDoctor, IEnumerable<TagsDoctorResource>>();
            //Service
            CreateMap<Service, ServiceResource>();
            CreateMap<Service, SaveServiceResource>();
            CreateMap<SaveServiceResource, Service>();
            CreateMap<List<SaveServiceResource>, List<Service>>();
            CreateMap<Service, IEnumerable<ServiceResource>>();
  
            //Cycle
            CreateMap<Cycle, CycleResource>();
            CreateMap<Cycle, SaveCycleResource>();
            CreateMap<SaveCycleResource, Cycle>();
            CreateMap<List<SaveCycleResource>, List<Cycle>>();
            CreateMap<Cycle, IEnumerable<CycleResource>>();
            //Sector
            CreateMap<SaveSectorResource, SectorResource>();
            CreateMap<Sector, SectorResource>();
            CreateMap<SectorResource, Sector>();

            CreateMap<Sector, SaveSectorResource>();
            CreateMap<SaveSectorResource, Sector>();
            CreateMap<List<SaveSectorResource>, List<Sector>>();
            CreateMap<Sector, IEnumerable<SectorResource>>();
            //SectorCycle
            CreateMap<SectorCycle, SectorCycleResource>();
            CreateMap<SectorCycle, SaveSectorCycleResource>();
            CreateMap<SaveSectorCycleResource, SectorCycle>();
            CreateMap<List<SaveSectorCycleResource>, List<SectorCycle>>();
            CreateMap<SectorCycle, IEnumerable<SectorCycleResource>>();
            //WeekSectorCycle
            CreateMap<SectorCycle, WeekSectorCycleResource>();
            CreateMap<SectorCycle, SaveWeekSectorCycleResource>();
            CreateMap<SaveWeekSectorCycleResource, SectorCycle>();
            CreateMap<List<SaveWeekSectorCycleResource>, List<SectorCycle>>();
            CreateMap<SectorCycle, IEnumerable<WeekSectorCycleResource>>();
            //CycleBu
            CreateMap<CycleBu, CycleBuResource>();
            CreateMap<CycleBu, SaveCycleBuResource>();
            CreateMap<SaveCycleBuResource, CycleBu>();
            CreateMap<List<SaveCycleBuResource>, List<CycleBu>>();
            CreateMap<CycleBu, IEnumerable<CycleBuResource>>();
            //PotentielCycle
            CreateMap<PotentielCycle, PotentielCycleResource>();
            CreateMap<PotentielCycle, SavePotentielCycleResource>();
            CreateMap<SavePotentielCycleResource, PotentielCycle>();
            CreateMap<List<SavePotentielCycleResource>, List<PotentielCycle>>();
            CreateMap<PotentielCycle, IEnumerable<PotentielCycleResource>>();
            //PotentielSector
            CreateMap<PotentielSector, PotentielSectorResource>();
            CreateMap<PotentielSector, SavePotentielSectorResource>();
            CreateMap<SavePotentielSectorResource, PotentielSector>();
            CreateMap<List<SavePotentielSectorResource>, List<PotentielSector>>();
            CreateMap<PotentielSector, IEnumerable<PotentielSectorResource>>();
            //Potentiel
            CreateMap<Potentiel, PotentielResource>();
            CreateMap<Potentiel, SavePotentielResource>();
            CreateMap<SavePotentielResource, Potentiel>();
            CreateMap<List<SavePotentielResource>, List<Potentiel>>();
            CreateMap<Potentiel, IEnumerable<PotentielResource>>();
            //WeekInYear
            CreateMap<WeekInYear, WeekInYearResource>();
            CreateMap<WeekInYear, SaveWeekInYearResource>();
            CreateMap<SaveWeekInYearResource, WeekInYear>();
            CreateMap<List<SaveWeekInYearResource>, List<WeekInYear>>();
            CreateMap<WeekInYear, IEnumerable<WeekInYearResource>>();
            //WeekSectorCycle
            CreateMap<SectorCycle, WeekSectorCycleResource>();
            CreateMap<SectorCycle, SaveWeekSectorCycleResource>();
            CreateMap<SaveWeekSectorCycleResource, SectorCycle>();
            CreateMap<List<SaveWeekSectorCycleResource>, List<SectorCycle>>();
            CreateMap<SectorCycle, IEnumerable<WeekSectorCycleResource>>();
            //WeekSectorCycle
            CreateMap<SectorCycle, WeekSectorCycleResource>();
            CreateMap<SectorCycle, SaveWeekSectorCycleResource>();
            CreateMap<SaveWeekSectorCycleResource, SectorCycle>();
            CreateMap<List<SaveWeekSectorCycleResource>, List<SectorCycle>>();
            CreateMap<SectorCycle, IEnumerable<WeekSectorCycleResource>>();
            //Phone
            CreateMap<Phone, PhoneResource>();
            CreateMap<Phone, SavePhoneResource>();
            CreateMap<SavePhoneResource, Phone>();
            CreateMap<List<SavePhoneResource>, List<Phone>>();
            CreateMap<Phone, IEnumerable<PhoneResource>>();

        }


    }
}
