using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class TagsDoctorService : ITagsDoctorService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public TagsDoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TagsDoctor> Create(TagsDoctor newTagsDoctor)
        {

            await _unitOfWork.TagsDoctors.Add(newTagsDoctor);
            await _unitOfWork.CommitAsync();
            return newTagsDoctor;
        }
        public async Task<List<TagsDoctor>> CreateRange(List<TagsDoctor> newTagsDoctor)
        {

            await _unitOfWork.TagsDoctors.AddRange(newTagsDoctor);
            await _unitOfWork.CommitAsync();
            return newTagsDoctor;
        }
        public async Task<IEnumerable<TagsDoctor>> GetAll()
        {
            return
                           await _unitOfWork.TagsDoctors.GetAll();
        }

       /* public async Task Delete(TagsDoctor TagsDoctor)
        {
            _unitOfWork.TagsDoctors.Remove(TagsDoctor);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<TagsDoctor>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.TagsDoctors
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<IEnumerable<TagsDoctor>> GetByIdActif(int id)
        {
            return
                await _unitOfWork.TagsDoctors.GetByIdActif(id);
        }
   
        public async Task Update(TagsDoctor TagsDoctorToBeUpdated, TagsDoctor TagsDoctor)
        {
            TagsDoctorToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            TagsDoctor.Version = TagsDoctorToBeUpdated.Version + 1;
            TagsDoctor.IdTags = TagsDoctorToBeUpdated.IdTags;
            TagsDoctor.Status = Status.Pending;
            TagsDoctor.Active = 0;

            await _unitOfWork.TagsDoctors.Add(TagsDoctor);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(TagsDoctor TagsDoctor)
        {
            //TagsDoctor musi =  _unitOfWork.TagsDoctors.SingleOrDefaultAsync(x=>x.Id == TagsDoctorToBeUpdated.Id);
            TagsDoctor.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<TagsDoctor> TagsDoctor)
        {
            foreach (var item in TagsDoctor)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllActif()
        {
            return
                             await _unitOfWork.TagsDoctors.GetAllActif();
        }

        public async Task<IEnumerable<TagsDoctor>> GetAllInActif()
        {
            return
                             await _unitOfWork.TagsDoctors.GetAllInActif();
        }

        public async Task<TagsDoctor> GetByIdActif(int idDoctor, int IdTag)
        {
            return
                  await _unitOfWork.TagsDoctors.GetByIdActif(idDoctor, IdTag);
        }
        //public Task<TagsDoctor> CreateTagsDoctor(TagsDoctor newTagsDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteTagsDoctor(TagsDoctor TagsDoctor)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<TagsDoctor> GetTagsDoctorById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<TagsDoctor>> GetTagsDoctorsByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateTagsDoctor(TagsDoctor TagsDoctorToBeUpdated, TagsDoctor TagsDoctor)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
