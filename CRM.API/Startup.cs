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

            services.AddTransient<IBrickService, BrickService>();
            services.AddTransient<IPhoneService, PhoneService>();
            services.AddTransient<IAdresseLocalityService, AdresseLocalityService>();

            services.AddTransient<IBrickLocalityService, BrickLocalityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBuUserService, BuUserService>();
            services.AddTransient<ILocalityService, LocalityService>();

            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<ITagsDoctorService, TagsDoctorService>();
            services.AddTransient<ISpecialtyService, SpecialtyService>();
            services.AddTransient<IInfoService, InfoService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ILocationTypeService, LocationTypeService>();

            services.AddTransient<ILocationDoctorService, LocationDoctorService>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IBusinessUnitService, BusinessUnitService>();
            services.AddTransient<IBuDoctorService, BuDoctorService>();
            services.AddTransient<IPharmacyService, PharmacyService>();
            services.AddTransient<IWholeSalerService, WholeSaleservice>();
            services.AddTransient<ICycleService, CycleService>();
            services.AddTransient<ICycleBuService, CycleBuService>();
            services.AddTransient<IPotentielCycleService, PotentielCycleService>();
            services.AddTransient<ISectorCycleService, SectorCycleService>();
            services.AddTransient<ISpecialtyService, SpecialtyService>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:44341"));
            });
            //***** Swagger ****
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "Phalcon", Description = "Galderma" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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
            app.UseCors(options => options.AllowAnyOrigin());

        }
    }
}
