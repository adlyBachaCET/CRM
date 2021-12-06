using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class CycleBuService : ICycleBuService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CycleBuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CycleBu> Create(CycleBu newCycleBu)
        {

            await _unitOfWork.CycleBus.Add(newCycleBu);
            await _unitOfWork.CommitAsync();
            return newCycleBu;
        }
        public async Task<List<CycleBu>> CreateRange(List<CycleBu> newCycleBu)
        {

            await _unitOfWork.CycleBus.AddRange(newCycleBu);
            await _unitOfWork.CommitAsync();
            return newCycleBu;
        }
        public async Task<IEnumerable<CycleBu>> GetAll()
        {
            return
                           await _unitOfWork.CycleBus.GetAll();
        }

       

        public async Task<CycleBu> GetById(int id)
        {
            return
                await _unitOfWork.CycleBus.GetById(id);
        }
   
        public async Task Update(CycleBu CycleBuToBeUpdated, CycleBu CycleBu)
        {
            CycleBu.Active = 1;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(CycleBu CycleBu)
        {
            CycleBu.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<CycleBu> CycleBu)
        {
            foreach (var item in CycleBu)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CycleBu>> GetAllActif()
        {
            return
                             await _unitOfWork.CycleBus.GetAllActif();
        }

        public async Task<IEnumerable<CycleBu>> GetAllInActif()
        {
            return
                             await _unitOfWork.CycleBus.GetAllInActif();
        }
        
    }
}
