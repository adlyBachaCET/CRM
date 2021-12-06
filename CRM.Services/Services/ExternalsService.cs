
using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ExternalsService : IExternalsService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ExternalsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Externals> Create(Externals newExternals)
        {

            await _unitOfWork.Externalss.Add(newExternals);
            await _unitOfWork.CommitAsync();
            return newExternals;
        }
        public async Task<List<Externals>> CreateRange(List<Externals> newExternals)
        {

            await _unitOfWork.Externalss.AddRange(newExternals);
            await _unitOfWork.CommitAsync();
            return newExternals;
        }
        public async Task<IEnumerable<Externals>> GetAll()
        {
            return
                           await _unitOfWork.Externalss.GetAll();
        }

        
   
        public async Task<Externals> GetById(int id)
        {
            return
               await _unitOfWork.Externalss.GetByIdActif(id);
        }

     
  
        
        public async Task<IEnumerable<Externals>> GetAllById(int id)
        {
            return
               await _unitOfWork.Externalss.GetAllById(id);
        }
        public async Task Update(Externals ExternalsToBeUpdated, Externals Externals)
        {
            ExternalsToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Externals.Version = ExternalsToBeUpdated.Version + 1;
            Externals.Status = Status.Pending;
            Externals.Active = 0;

            await _unitOfWork.Externalss.Add(Externals);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Externals Externals)
        {
            Externals.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Externals> Externals)
        {
            foreach (var item in Externals)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Externals>> GetAllActif()
        {
            return
                             await _unitOfWork.Externalss.GetAllActif();
        }

        public async Task<IEnumerable<Externals>> GetAllInActif()
        {
            return
                             await _unitOfWork.Externalss.GetAllInActif();
        }








      
    }
}
