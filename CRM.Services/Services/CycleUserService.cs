using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleUserService : ICycleUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CycleUser> Create(CycleUser newCycleUser)
        {

            await _unitOfWork.CycleUsers.Add(newCycleUser);
            await _unitOfWork.CommitAsync();
            return newCycleUser;
        }
        public async Task<List<CycleUser>> CreateRange(List<CycleUser> newCycleUser)
        {

            await _unitOfWork.CycleUsers.AddRange(newCycleUser);
            await _unitOfWork.CommitAsync();
            return newCycleUser;
        }
        public async Task<IEnumerable<CycleUser>> GetAll()
        {
            return
                           await _unitOfWork.CycleUsers.GetAll();
        }

      

        public async Task<CycleUser> GetById(int id)
        {
            return
                await _unitOfWork.CycleUsers.GetById(id);
        }
        public async Task<List<Cycle>> GetByIdUser(int id)
        {
            return
                  await _unitOfWork.CycleUsers.GetByIdUser(id);
        }

        public async Task Update(CycleUser CycleUserToBeUpdated, CycleUser CycleUser)
        {
            CycleUser.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(CycleUser CycleUser)
        {
            CycleUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<CycleUser> CycleUser)
        {
            foreach (var item in CycleUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CycleUser>> GetAllActif()
        {
            return
                             await _unitOfWork.CycleUsers.GetAllActif();
        }

        public async Task<IEnumerable<CycleUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.CycleUsers.GetAllInActif();
        }

        public async  Task<CycleUser> GetByIdCycleUser(int idCycle, int idUser)
        {
            return
                            await _unitOfWork.CycleUsers.GetByIdCycleUser(idCycle,idUser);
        }
       
    }
}
