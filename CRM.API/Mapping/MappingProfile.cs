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

            //Brick
            CreateMap<Brick, BrickResource>();
            CreateMap<Brick, SaveBrickResource>();
            CreateMap<SaveBrickResource, Brick>();
            CreateMap<Brick, IEnumerable<BrickResource>>();
            //BrickLocality
            CreateMap<BrickLocality, BrickLocalityResource>();
            CreateMap<BrickLocality, SaveBrickLocalityResource>();
            CreateMap<SaveBrickLocalityResource, BrickLocality>();
            CreateMap<BrickLocality, IEnumerable<BrickLocalityResource>>();
            //User
            CreateMap<User, UserResource>();
            CreateMap<User, SaveUserResource>();
            CreateMap<SaveUserResource, User>();
            CreateMap<User, IEnumerable<UserResource>>();

            //Doctor
            CreateMap<Doctor, DoctorResource>();
            CreateMap<Doctor, SaveDoctorResource>();
            CreateMap<SaveDoctorResource, Doctor>();
            CreateMap<Doctor, IEnumerable<DoctorResource>>();

            //BusinessUnit
            CreateMap<BusinessUnit, BusinessUnitResource>();
            CreateMap<BusinessUnit, SaveBusinessUnitResource>();
            CreateMap<SaveBusinessUnitResource, BusinessUnit>();
            CreateMap<BusinessUnit, IEnumerable<BusinessUnitResource>>();
            CreateMap<List<SaveBusinessUnitResource>, List<BusinessUnit>>();


            //Establishment
            CreateMap<Establishment, EstablishmentResource>();
            CreateMap<Establishment, SaveEstablishmentResource>();
            CreateMap<SaveEstablishmentResource, Establishment>();
            CreateMap < List<SaveEstablishmentResource>, List<Establishment>>();
            CreateMap<Establishment, IEnumerable<EstablishmentResource>>();
            //Info
            CreateMap<Info, InfoResource>();
            CreateMap<Info, SaveInfoResource>();
            CreateMap<SaveInfoResource, Info>();
            CreateMap<List<SaveInfoResource>, List<Info>>();
            CreateMap<Info, IEnumerable<InfoResource>>();
        }

    }
}
