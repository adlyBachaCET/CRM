using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class BrickService : IBrickService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BrickService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Brick> Create(Brick newBrick)
        {
            await _unitOfWork.Bricks.Add(newBrick);
            await _unitOfWork.CommitAsync();
            return newBrick;
        }
        public async Task<List<Brick>> CreateRange(List<Brick> newBrick)
        {

            await _unitOfWork.Bricks.AddRange(newBrick);
            await _unitOfWork.CommitAsync();
            return newBrick;
        }
        public async Task<IEnumerable<Brick>> GetAll()
        {
            return
                           await _unitOfWork.Bricks.GetAll();
        }

       /* public async Task Delete(Brick Brick)
        {
            _unitOfWork.Bricks.Remove(Brick);
            await _unitOfWork.CommitAsync();
        }*/

        //public async Task<IEnumerable<Brick>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Bricks
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Brick> GetById(int id)
        {
            return
                await _unitOfWork.Bricks.SingleOrDefault(i =>i.IdBrick==id && i.Status==Status.Approuved
                );
        }
   
        public async Task Update(Brick BrickToBeUpdated, Brick Brick)
        {
            BrickToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Brick.Version = BrickToBeUpdated.Version + 1;
            Brick.IdBrick = BrickToBeUpdated.IdBrick;
            Brick.Status = Status.Pending;
            Brick.Active = 0;

            await _unitOfWork.Bricks.Add(Brick);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Brick Brick)
        {
            //Brick musi =  _unitOfWork.Bricks.SingleOrDefaultAsync(x=>x.Id == BrickToBeUpdated.Id);
            Brick.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Brick> Brick)
        {
            foreach (var item in Brick)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Brick>> GetAllActif()
        {
            return
                             await _unitOfWork.Bricks.GetAllActif();
        }

        public async Task<IEnumerable<Brick>> GetAllInActif()
        {
            return
                             await _unitOfWork.Bricks.GetAllInActif();
        }

        public async  Task<Brick> GetByIdActif(int? id)
        {
            return
                          await _unitOfWork.Bricks.GetByIdActif(id);
        }
        //public Task<Brick> CreateBrick(Brick newBrick)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteBrick(Brick Brick)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Brick> GetBrickById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Brick>> GetBricksByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateBrick(Brick BrickToBeUpdated, Brick Brick)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
