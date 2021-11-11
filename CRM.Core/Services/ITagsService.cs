using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ITagsService
    {
        Task<Tags> GetById(int id);
        Task<Tags> Create(Tags newTags);
        Task<List<Tags>> CreateRange(List<Tags> newTags);
        Task Update(Tags TagsToBeUpdated, Tags Tags);
        Task Delete(Tags TagsToBeDeleted);
        Task DeleteRange(List<Tags> Tags);

        Task<IEnumerable<Tags>> GetAll();
        Task<IEnumerable<Tags>> GetAllActif();
        Task<IEnumerable<Tags>> GetAllInActif();

    }
}
