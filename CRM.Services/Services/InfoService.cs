using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class InfoService : IInfoService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public InfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Info> Create(Info newInfo)
        {

            await _unitOfWork.Infos.Add(newInfo);
            await _unitOfWork.CommitAsync();
            return newInfo;
        }
        public async Task<List<Info>> CreateRange(List<Info> newInfo)
        {

            await _unitOfWork.Infos.AddRange(newInfo);
            await _unitOfWork.CommitAsync();
            return newInfo;
        }
        public async Task<IEnumerable<Info>> GetAll()
        {
            return
                           await _unitOfWork.Infos.GetAll();
        }

       /* public async Task Delete(Info Info)
        {
            _unitOfWork.Infos.Remove(Info);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Info>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Infos
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Info> GetById(int id)
        {
             return
               await _unitOfWork.Infos.SingleOrDefault(i => i.IdInf == id && i.Active == 0);
        }
   
        public async Task Update(Info InfoToBeUpdated, Info Info)
        {
            InfoToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Info.Version = InfoToBeUpdated.Version + 1;
            Info.IdInf = InfoToBeUpdated.IdInf;
            Info.Status = Status.Pending;
            Info.Active = 0;

            await _unitOfWork.Infos.Add(Info);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Info Info)
        {
            //Info musi =  _unitOfWork.Infos.SingleOrDefaultAsync(x=>x.Id == InfoToBeUpdated.Id);
            Info.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Info> Info)
        {
            foreach (var item in Info)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Info>> GetAllActif()
        {
            return
                             await _unitOfWork.Infos.GetAllActif();
        }

        public async Task<IEnumerable<Info>> GetAllInActif()
        {
            return
                             await _unitOfWork.Infos.GetAllInActif();
        }

        public async Task<IEnumerable<Info>> GetByIdDoctor(int id)
        {
            return
                            await _unitOfWork.Infos.GetByIdDoctor(id);
        }

        public async Task<Info> GetBy(string Type, string Data)
        {
            //Info musi =  _unitOfWork.Infos.SingleOrDefaultAsync(x=>x.Id == InfoToBeUpdated.Id);

return await _unitOfWork.Infos.SingleOrDefault(x => x.Data == Data&& x.Datatype==Type);
        }
        //public Task<Info> CreateInfo(Info newInfo)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteInfo(Info Info)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Info> GetInfoById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Info>> GetInfosByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateInfo(Info InfoToBeUpdated, Info Info)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
