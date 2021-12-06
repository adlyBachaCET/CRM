using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class DoctorService : IDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Doctor> Create(Doctor newDoctor)
        {

            await _unitOfWork.Doctors.Add(newDoctor);
            await _unitOfWork.CommitAsync();
            return newDoctor;
        }
        public async Task<IEnumerable<Doctor>> GetByExistantPhoneNumberActif(int PhoneNumber)
        {
            return
                                 await _unitOfWork.Doctors.GetByExistantPhoneNumberActif(PhoneNumber);
        }
        public async Task<List<Specialty>> GetByIdDoctor(int idDoctor)
        {
        

            return await _unitOfWork.Doctors.GetByIdDoctor(idDoctor);
        }
        public async Task<List<Doctor>> CreateRange(List<Doctor> newDoctor)
        {

            await _unitOfWork.Doctors.AddRange(newDoctor);
            await _unitOfWork.CommitAsync();
            return newDoctor;
        }
        public async Task Approuve(Doctor DoctorToBeUpdated, Doctor Doctor)
        {
            DoctorToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Doctor = DoctorToBeUpdated;
            Doctor.Version = DoctorToBeUpdated.Version + 1;
            Doctor.IdDoctor = DoctorToBeUpdated.IdDoctor;
            Doctor.Status = Status.Approuved;
            Doctor.UpdatedOn = System.DateTime.UtcNow;
            Doctor.CreatedOn = DoctorToBeUpdated.CreatedOn;

            Doctor.Active = 0;

            await _unitOfWork.Doctors.Add(Doctor);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Doctor DoctorToBeUpdated, Doctor Doctor)
        {
            DoctorToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Doctor = DoctorToBeUpdated;
            Doctor.Version = DoctorToBeUpdated.Version + 1;
            Doctor.IdDoctor = DoctorToBeUpdated.IdDoctor;
            Doctor.Status = Status.Rejected;
            Doctor.UpdatedOn = System.DateTime.UtcNow;
            Doctor.CreatedOn = DoctorToBeUpdated.CreatedOn;

            Doctor.Active = 0;

            await _unitOfWork.Doctors.Add(Doctor);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return
                           await _unitOfWork.Doctors.GetAll();
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsByBu(int Id)
        {

            return
                              await _unitOfWork.Doctors.GetAllDoctorsByBu(Id);
        }

        public async Task<DoctorExiste> GetExist(string FirstName,string LastName,string Email)
        {
            return
                             await _unitOfWork.Doctors.GetExist(FirstName,LastName,Email);
        }

        public async Task<Doctor> GetById(int? id)
        {
            return
                 await _unitOfWork.Doctors.GetById(id);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAssignedByBu(int Id)
        {

            return
                 await _unitOfWork.Doctors.GetDoctorsAssignedByBu(Id);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsNotAssignedByBu(int Id)
        {
            return
              await _unitOfWork.Doctors.GetDoctorsNotAssignedByBu(Id);
        }
        public async Task<IEnumerable<Doctor>> GetMyDoctorsWithoutAppointment(int Id)
        {
            return
               await _unitOfWork.Doctors.GetMyDoctorsWithoutAppointment(Id);
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAssigned()
        {
            return
             await _unitOfWork.Doctors.GetDoctorsAssigned();
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsNotAssigned()
        {
            return
               await _unitOfWork.Doctors.GetDoctorsNotAssigned();
        }
        public async Task Update(Doctor DoctorToBeUpdated, Doctor Doctor)
        {
           DoctorToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

           Doctor.Version =DoctorToBeUpdated.Version + 1;
           Doctor.IdDoctor =DoctorToBeUpdated.IdDoctor;
           Doctor.Status = Status.Approuved;
           Doctor.Active = 0;
           Doctor.CreatedOn =DoctorToBeUpdated.CreatedOn;
           Doctor.UpdatedOn = DateTime.UtcNow;
            await _unitOfWork.Doctors.Add(Doctor);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Doctor Doctor)
        {
            Doctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Doctor> Doctor)
        {
            foreach (var item in Doctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllActif()
        {
            return
                             await _unitOfWork.Doctors.GetAllAcceptedActif();
        }

        public async Task<IEnumerable<Doctor>> GetAllInActif()
        {
            return
                        await _unitOfWork.Doctors.GetAllInActif();
        }

        public async Task<IEnumerable<Service>> GetServiceByIdLocationActif(int IdLocation)
        {
            return
                             await _unitOfWork.Doctors.GetServiceByIdLocationActif(IdLocation);
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByLocalities(List<int> IdLocalities)
        {
            return
                                 await _unitOfWork.Doctors.GetDoctorsByLocalities(IdLocalities);
        }

        public async Task<List<Locality>> GetLocalitiesFromDoctors(List<Doctor> DoctorList)
        {
            return
                                             await _unitOfWork.Doctors.GetLocalitiesFromDoctors( DoctorList);   
        }
    }
}
