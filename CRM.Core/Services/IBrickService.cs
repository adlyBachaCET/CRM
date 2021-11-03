using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IBrickService
    {
        Task<Brick> GetById(int id);
        Task<Brick> Create(Brick newBrick);
        Task<List<Brick>> CreateRange(List<Brick> newBrick);
        Task Update(Brick BrickToBeUpdated, Brick Brick);
        Task Delete(Brick BrickToBeDeleted);
        Task DeleteRange(List<Brick> Brick);

        Task<IEnumerable<Brick>> GetAll();
        Task<IEnumerable<Brick>> GetAllActif();
        Task<IEnumerable<Brick>> GetAllInActif();
        Task<Brick> GetByIdActif(int id);

    }
}
