using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ITagsDoctorService
    {
        Task<IEnumerable<TagsDoctor>> GetByIdActif(int id);
        Task<TagsDoctor> Create(TagsDoctor newTagsDoctor);
        Task<List<TagsDoctor>> CreateRange(List<TagsDoctor> newTagsDoctor);
        Task Update(TagsDoctor TagsDoctorToBeUpdated, TagsDoctor TagsDoctor);
        Task Delete(TagsDoctor TagsDoctorToBeDeleted);
        Task DeleteRange(List<TagsDoctor> TagsDoctor);
        Task<TagsDoctor> GetByIdActif(int idDoctor, int IdTag);

        Task<IEnumerable<TagsDoctor>> GetAll();
        Task<IEnumerable<TagsDoctor>> GetAllActif();
        Task<IEnumerable<TagsDoctor>> GetAllInActif();

    }
}
