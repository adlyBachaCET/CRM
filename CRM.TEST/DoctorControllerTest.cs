using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CRM.TEST
{
    public class DoctorControllerTest
    {
        private readonly ISpecialtyDoctorService _SpecialtyDoctorService;

        private readonly IDoctorService _DoctorService;
        private readonly IVisitReportService _VisitReportService;
        private readonly ILocalityService _LocalityService;
        private readonly IBrickService _BrickService;
        private readonly IBusinessUnitService _BusinessUnitService;
        private readonly IRequestRpService _RequestRpService;

        private readonly IBuDoctorService _BuDoctorService;
        private readonly IServiceService _ServiceService;

        private readonly ILocationDoctorService _LocationDoctorService;
        private readonly ILocationService _LocationService;
        private readonly IPhoneService _PhoneService;
        private readonly IUserService _UserService;
        private readonly ILocationTypeService _LocationTypeService;

        private readonly ITagsDoctorService _TagsDoctorService;
        private readonly ITagsService _TagsService;
        private readonly IObjectionService _ObjectionService;

        private readonly IInfoService _InfoService;
        private readonly IPotentielService _PotentielService;
        private readonly ISpecialtyService _SpecialtyService;
        DoctorController DoctorController;
        private readonly IMapper _mapperService;
        public DoctorControllerTest()
        {

            DoctorController = new DoctorController(_LocationDoctorService, _PhoneService, _UserService, _BrickService,
            _SpecialtyDoctorService, _LocationService, _LocationTypeService,
            _LocalityService, _ServiceService, _DoctorService, _PotentielService, _ObjectionService, _SpecialtyService,
            _BusinessUnitService, _RequestRpService, _InfoService, _TagsService, _TagsDoctorService, _VisitReportService,
            _BuDoctorService, _mapperService);
        }

        #region Property  
        public Mock<DoctorController> mock = new Mock<DoctorController>();
        #endregion

        [Fact]
        public async Task CreateDoctorTest()
        {
            var Docotrs = SaveDoctorDoctor();
            //Arrange
            string Token = "";
            SaveDoctorResource SaveDoctorResource = Docotrs[0];
           
            mock.Setup(p => p.CreateDoctor(Token, SaveDoctorResource));
            //Assert
           
            var CreatedDoctor = await DoctorController.CreateDoctor(Token, SaveDoctorResource);
        
            //Assert
            var result = CreatedDoctor.Result;
            Assert.IsType<CreatedAtRouteResult>(result);
        }
        private List<SaveDoctorResource> SaveDoctorDoctor()
        {    
            SaveInfoResource Info1 = new SaveInfoResource();
            Info1.Datatype = "string";
            Info1.Data = "data";
            Info1.Label = "qq";
            SaveTagsResource Tags1 = new SaveTagsResource();
            Tags1.Name = "mpl";
            Tags1.Description= "mpl";
            Tags1.Name = "mpl";
            Tags1.Description = "mpl";
            SaveLocationSelectResource SaveLocationSelectResource1 = new SaveLocationSelectResource();
            SaveLocationSelectResource1.ChefService = false;
            SaveLocationSelectResource1.IdLocation = 1;
            SaveLocationSelectResource1.IdService = 1;
            SaveLocationSelectResource1.Order = 0;
            SaveLocationSelectResource1.Primary = 1;
            SavePhoneResource SavePhoneResource1 = new SavePhoneResource();
            SavePhoneResource1.Description = "this is a descriprtion";
            SavePhoneResource1.PhoneInfo = "this is info";
            SavePhoneResource1.PhoneType = PhoneType.Mobile ;
            SavePhoneResource1.PhoneNumber = 98654321;
            ListOfCabinetsWithOrder ListOfCabinetsWithOrder = new ListOfCabinetsWithOrder();
            ListOfCabinetsWithOrder.Primary = 1;
            ListOfCabinetsWithOrder.Order = 1;
            LocationAdd LocationAdd1 = new LocationAdd();
            LocationAdd1.Altitude = 0;
            LocationAdd1.Longitude = 0;
            LocationAdd1.Name = "aaaa";
            LocationAdd1.PostalCode = 125656;
            LocationAdd1.tel = 12365478;
            LocationAdd1.IdLocality1 = 1;
            LocationAdd1.IdLocality2 = 1;
            LocationAdd1.IdBrick1 = null;
            LocationAdd1.IdBrick2 = null;
            ListOfCabinetsWithOrder.Cabinet = LocationAdd1;

            SaveInfoResource Info2 = new SaveInfoResource();
            Info2.Datatype = "dzdzdz";
            Info2.Data = "dsfsdfsd";
            Info2.Label = "sdfsdfsd";
            SaveTagsResource Tags2 = new SaveTagsResource();
            Tags2.Name = "mpl";
            Tags2.Description = "mpl";
            Tags2.Name = "mpl";
            Tags2.Description = "mpl";
            SaveLocationSelectResource SaveLocationSelectResource2 = new SaveLocationSelectResource();
            SaveLocationSelectResource2.ChefService = false;
            SaveLocationSelectResource2.IdLocation = 1;
            SaveLocationSelectResource2.IdService = 1;
            SaveLocationSelectResource2.Order = 0;
            SaveLocationSelectResource2.Primary = 1;
            SavePhoneResource SavePhoneResource2 = new SavePhoneResource();
            SavePhoneResource2.Description = "this is a descriprtion";
            SavePhoneResource2.PhoneInfo = "this is info";
            SavePhoneResource2.PhoneType = PhoneType.Mobile;
            SavePhoneResource2.PhoneNumber = 98654321;
            ListOfCabinetsWithOrder ListOfCabinetsWithOrder1 = new ListOfCabinetsWithOrder();
            ListOfCabinetsWithOrder1.Primary = 1;
            ListOfCabinetsWithOrder1.Order = 1;
            LocationAdd LocationAdd2 = new LocationAdd();
            LocationAdd2.Altitude = 0;
            LocationAdd2.Longitude = 0;
            LocationAdd2.Name = "aaaa";
            LocationAdd2.PostalCode = 125656;
            LocationAdd2.tel = 12365478;
            LocationAdd2.IdLocality1 = 1;
            LocationAdd2.IdLocality2 = 1;
            LocationAdd2.IdBrick1 = null;
            LocationAdd2.IdBrick2 = null;
            ListOfCabinetsWithOrder1.Cabinet = LocationAdd2;
            List<SaveDoctorResource> output = new List<SaveDoctorResource>
            {
                new SaveDoctorResource
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    IdPotentiel = 1,
                    Email = "jhon@gmail.com",
                    BusinessUnits={1},
                    Infos={Info1},
                    Tags={Tags1},
                    SpecialtyList={1},
                    Phones={SavePhoneResource1},
                    Location={SaveLocationSelectResource1},
                    Cabinets={ListOfCabinetsWithOrder}
                },
                new SaveDoctorResource
                {
                         FirstName = "Jhon",
                    LastName = "Doe",
                    IdPotentiel = 1,
                    Email = "jhon@gmail.com",
                    BusinessUnits={1},
                    Infos={Info2},
                    Tags={Tags2},
                    SpecialtyList={1},
                    Phones={SavePhoneResource2},
                    Location={SaveLocationSelectResource2},
                    Cabinets={ListOfCabinetsWithOrder1}
                }
            };
            return output;
        }
        private List<Doctor> GetSampleDoctor()
        {
            List<Doctor> output = new List<Doctor>
            {
                new Doctor
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    Active = 0,
                    IdPotentiel = 1,
                    Email = "jhon@gmail.com",
                    CreatedBy = 1,
                    UpdatedBy=1,
                    Version=0,
                    Status=0,
                    CreatedOn=System.DateTime.UtcNow,
                    UpdatedOn=System.DateTime.UtcNow

                },
                new Doctor
                {
                              FirstName = "Jhon",
                    LastName = "Doe",
                    Active = 0,
                    IdPotentiel = 1,
                    Email = "jhon@gmail.com",
                    CreatedBy = 1,
                    UpdatedBy=1,
                    Version=0,
                    Status=0,
                    CreatedOn=System.DateTime.UtcNow,
                    UpdatedOn=System.DateTime.UtcNow
                },
                new Doctor
                {
                                 FirstName = "Jhon",
                    LastName = "Doe",
                    Active = 0,
                    IdPotentiel = 1,
                    Email = "jhon@gmail.com",
                    CreatedBy = 1,
                    UpdatedBy=1,
                    Version=0,
                    Status=0,
                    CreatedOn=System.DateTime.UtcNow,
                    UpdatedOn=System.DateTime.UtcNow,
                }
            };
            return output;
        }
    }

}
