using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CRM.Core.Services
{
    public interface ISellingObjectivesService
    {
        Task<SellingObjectives> GetById(int? id);
        Task<SellingObjectives> Create(SellingObjectives newSellingObjectives);
        Task<List<SellingObjectives>> CreateRange(List<SellingObjectives> newSellingObjectives);
        Task Update(SellingObjectives SellingObjectivesToBeUpdated, SellingObjectives SellingObjectives);
        Task Delete(SellingObjectives SellingObjectivesToBeDeleted);
        Task DeleteRange(List<SellingObjectives> SellingObjectives);
    


        Task<IEnumerable<SellingObjectives>> GetAll();
        Task<IEnumerable<SellingObjectives>> GetAllActif();
        Task<IEnumerable<SellingObjectives>> GetAllInActif();


    }
}
