using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class SellingObjectivesService : ISellingObjectivesService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public SellingObjectivesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SellingObjectives> Create(SellingObjectives newSellingObjectives)
        {
            if (newSellingObjectives.IdDoctor != 0)
            {
                var Doctor = await _unitOfWork.Doctors.SingleOrDefault(a => a.IdDoctor == newSellingObjectives.IdDoctor && a.Active == 0);
                newSellingObjectives.IdDoctor = Doctor.IdDoctor;
                newSellingObjectives.VersionDoctor = Doctor.Version;
                newSellingObjectives.StatusDoctor = Doctor.Status;
                newSellingObjectives.Doctor = Doctor;

            }
            if (newSellingObjectives.IdPharmacy != 0)
            {
                var Pharmacy = await _unitOfWork.Pharmacys.SingleOrDefault(a => a.IdPharmacy == newSellingObjectives.IdPharmacy && a.Active == 0);
                newSellingObjectives.IdPharmacy = Pharmacy.IdPharmacy;
                newSellingObjectives.VersionPharmacy = Pharmacy.Version;
                newSellingObjectives.StatusPharmacy = Pharmacy.Status;
                newSellingObjectives.Pharmacy = Pharmacy;
            }
            if (newSellingObjectives.IdUser!= 0)
            {
                var User= await _unitOfWork.Users.SingleOrDefault(a => a.IdUser== newSellingObjectives.IdUser&& a.Active == 0);
                newSellingObjectives.IdUser= User.IdUser;
                newSellingObjectives.VersionUser= User.Version;
                newSellingObjectives.StatusUser= User.Status;
                newSellingObjectives.User = User;
            }
            await _unitOfWork.SellingObjectivess.Add(newSellingObjectives);
            await _unitOfWork.CommitAsync();
            return newSellingObjectives;
        }
        public async Task<List<SellingObjectives>> CreateRange(List<SellingObjectives> newSellingObjectives)
        {

            await _unitOfWork.SellingObjectivess.AddRange(newSellingObjectives);
            await _unitOfWork.CommitAsync();
            return newSellingObjectives;
        }
        public async Task<IEnumerable<SellingObjectives>> GetAll()
        {
            return
                           await _unitOfWork.SellingObjectivess.GetAll();
        }

       /* public async Task Delete(SellingObjectives SellingObjectives)
        {
            _unitOfWork.SellingObjectivess.Remove(SellingObjectives);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<SellingObjectives>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.SellingObjectivess
        //          .GetAllWithArtisteAsync();
        //}

  
        public async Task<List<SellingObjectives>> GetByIdUser(int id)
        {
    
            
            return null;
              
        }
        public async Task Update(SellingObjectives SellingObjectivesToBeUpdated, SellingObjectives SellingObjectives)
        {
            SellingObjectivesToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(SellingObjectives SellingObjectives)
        {
            //SellingObjectives musi =  _unitOfWork.SellingObjectivess.SingleOrDefaultAsync(x=>x.Id == SellingObjectivesToBeUpdated.Id);
            SellingObjectives.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<SellingObjectives> SellingObjectives)
        {
            foreach (var item in SellingObjectives)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllActif()
        {
            return
                             await _unitOfWork.SellingObjectivess.GetAllActif();
        }

        public async Task<IEnumerable<SellingObjectives>> GetAllInActif()
        {
            return
                             await _unitOfWork.SellingObjectivess.GetAllInActif();
        }

     
        public async Task<IEnumerable<SellingObjectives>> GetByIdPharmacy(int id)
        {
            return
                            null;
        }

        public Task<SellingObjectives> GetById(int? id)
        {
            throw new System.NotImplementedException();
        }

        //public Task<SellingObjectives> CreateSellingObjectives(SellingObjectives newSellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteSellingObjectives(SellingObjectives SellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<SellingObjectives> GetSellingObjectivesById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<SellingObjectives>> GetSellingObjectivessByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateSellingObjectives(SellingObjectives SellingObjectivesToBeUpdated, SellingObjectives SellingObjectives)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
