using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
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
        public async Task<List<Doctor>> CreateRange(List<Doctor> newDoctor)
        {

            await _unitOfWork.Doctors.AddRange(newDoctor);
            await _unitOfWork.CommitAsync();
            return newDoctor;
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return
                           await _unitOfWork.Doctors.GetAll();
        }

       /* public async Task Delete(Doctor Doctor)
        {
            _unitOfWork.Doctors.Remove(Doctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Doctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Doctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Doctor> GetById(int id)
        {
            return
                 await _unitOfWork.Doctors.SingleOrDefault(i => i.IdDoctor == id && i.Active==0);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAssigned()
        {
            List<Doctor> doctors = new List<Doctor>();
            var a = await _unitOfWork.CycleSectorWeekDoctors.Find(i => i.IdDoctorNavigation.Active == 0 && i.IdDoctorNavigation.LinkedId == null);
            foreach (var item in a)
            {
                doctors.Add(item.IdDoctorNavigation);
            }
            return doctors;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsNotAssigned()
        {
            List<Doctor> DoctorsAssigned = new List<Doctor>();
            var list = await _unitOfWork.CycleSectorWeekDoctors.Find(i => i.IdDoctorNavigation.Active == 0 && i.IdDoctorNavigation.LinkedId == null);
            foreach (var item in list)
            {
                DoctorsAssigned.Add(item.IdDoctorNavigation);
            }
            var Alldoctors = await _unitOfWork.Doctors.Find(i => i.Status == Status.Approuved && i.Active == 0);
            var result = Alldoctors.Except(DoctorsAssigned).ToList();
            return DoctorsAssigned;
        }
        public async Task Update(Doctor DoctorToBeUpdated, Doctor Doctor)
        {
            DoctorToBeUpdated.Active = 0;
            await _unitOfWork.CommitAsync();

            Doctor.Version = DoctorToBeUpdated.Version + 1;
            Doctor.IdDoctor = DoctorToBeUpdated.IdDoctor;
            Doctor.Status = Status.Pending;
            Doctor.Active = 1;

            await _unitOfWork.Doctors.Add(Doctor);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Doctor Doctor)
        {
            //Doctor musi =  _unitOfWork.Doctors.SingleOrDefaultAsync(x=>x.Id == DoctorToBeUpdated.Id);
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
                             await _unitOfWork.Doctors.Find(i => i.Status == Status.Approuved || i.Status == Status.Pending && i.Active == 0);
        }

        public async Task<IEnumerable<Doctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.Doctors.GetAllInActif();
        }

        //public Task<Doctor> CreateDoctor(Doctor newDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteDoctor(Doctor Doctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Doctor> GetDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Doctor>> GetDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateDoctor(Doctor DoctorToBeUpdated, Doctor Doctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
