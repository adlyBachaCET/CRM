using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Core;
using CRM.Core.Services;
using CRM.Data;
using CRM.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CRM_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //**** Confiuration de SQL Server ****
            //services.AddDbContext<MyDbContext>(
            //options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("CRM.Data")));
            //options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("MyUser.Data_SqlServer")));
            services.AddDbContext<MyDbContext>(
           options => options.UseNpgsql(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("CRM.Data")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services 
            services.AddScoped<ISellingObjectivesService, SellingObjectivesService>();
            services.AddScoped<ISpecialtyDoctorService, SpecialtyDoctorService>();

            services.AddScoped<IAppointementService, AppointementService>();
            services.AddScoped<ITargetService, TargetService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductPharmacyService, ProductPharmacyService>();
            services.AddScoped<ISupportService, SupportService>();
            services.AddScoped<IActivityUserService, ActivityUserService>();

            services.AddScoped<IActivityService, ActivityService>();

            services.AddScoped<ICommandeService, CommandeService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IVisitUserService, VisitUserService>();

            services.AddScoped<IRequestRpService, RequestRpService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            services.AddScoped<IVisitReportService, VisitReportService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IObjectionService, ObjectionService>();

            services.AddScoped<IBrickService, BrickService>();
            services.AddScoped<IPhoneService, PhoneService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBuUserService, BuUserService>();
            services.AddScoped<ICycleUserService, CycleUserService>();

            services.AddScoped<ILocalityService, LocalityService>();

            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<ITagsDoctorService, TagsDoctorService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationTypeService, LocationTypeService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPotentielService, PotentielService>();

            services.AddScoped<ILocationDoctorService, LocationDoctorService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IBusinessUnitService, BusinessUnitService>();
            services.AddScoped<IBuDoctorService, BuDoctorService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<ICycleService, CycleService>();
            services.AddScoped<ICycleBuService, CycleBuService>(); 
            services.AddScoped<IPotentielSectorService, PotentielSectorService>();

            services.AddScoped<IPotentielCycleService, PotentielCycleService>();
            services.AddScoped<ISectorCycleService, SectorCycleService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<ISectorInYearService, SectorInYearService>();

            services.AddScoped<ISpecialtyService, SpecialtyService>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("token"));
            });
            //***** Swagger ****

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Phalcon Api",
                    Description = ""
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });
   
            // ***** AutoMapper *****
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //***** Swagger middleware****
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My User V1");
            }
                );
            //Cors

        }
    }
}
