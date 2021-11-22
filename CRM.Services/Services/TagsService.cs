using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class TagsService : ITagsService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public TagsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tags> Create(Tags newTags)
        {

            await _unitOfWork.Tagss.Add(newTags);
            await _unitOfWork.CommitAsync();
            return newTags;
        }
        public async Task<List<Tags>> CreateRange(List<Tags> newTags)
        {

            await _unitOfWork.Tagss.AddRange(newTags);
            await _unitOfWork.CommitAsync();
            return newTags;
        }
        public async Task<IEnumerable<Tags>> GetAll()
        {
            return
                           await _unitOfWork.Tagss.GetAll();
        }

       /* public async Task Delete(Tags Tags)
        {
            _unitOfWork.Tagss.Remove(Tags);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Tags>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Tagss
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Tags> GetById(int id)
        {
            return
                 await _unitOfWork.Tagss.SingleOrDefault(i => i.IdTags == id && i.Active == 0);
        }
        public async Task<Tags> GetBy(string name)
        {
            return
                 await _unitOfWork.Tagss.SingleOrDefault(i => i.Name == name && i.Active == 0);
        }
        public async Task Update(Tags TagsToBeUpdated, Tags Tags)
        {
            TagsToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Tags.Version = TagsToBeUpdated.Version + 1;
            Tags.IdTags = TagsToBeUpdated.IdTags;
            Tags.Status = Status.Pending;
            Tags.Active = 0;

            await _unitOfWork.Tagss.Add(Tags);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Tags Tags)
        {
            //Tags musi =  _unitOfWork.Tagss.SingleOrDefaultAsync(x=>x.Id == TagsToBeUpdated.Id);
            Tags.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Tags> Tags)
        {
            foreach (var item in Tags)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Tags>> GetAllActif()
        {
            return
                             await _unitOfWork.Tagss.GetAllActif();
        }

        public async Task<IEnumerable<Tags>> GetAllInActif()
        {
            return
                             await _unitOfWork.Tagss.GetAllInActif();
        }

        public async  Task<Tags> GetByExistantActif(string Name)
        {
            return
                                       await _unitOfWork.Tagss.GetByExistantActif(Name);
        }
        //public Task<Tags> CreateTags(Tags newTags)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteTags(Tags Tags)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Tags> GetTagsById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Tags>> GetTagssByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateTags(Tags TagsToBeUpdated, Tags Tags)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
