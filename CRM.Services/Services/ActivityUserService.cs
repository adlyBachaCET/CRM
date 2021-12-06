using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ActivityUserService : IActivityUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ActivityUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActivityUser> Create(ActivityUser newActivityUser)
        {

            await _unitOfWork.ActivityUsers.Add(newActivityUser);
            await _unitOfWork.CommitAsync();
            return newActivityUser;
        }
        public async Task<List<ActivityUser>> CreateRange(List<ActivityUser> newActivityUser)
        {

            await _unitOfWork.ActivityUsers.AddRange(newActivityUser);
            await _unitOfWork.CommitAsync();
            return newActivityUser;
        }
  
        public async Task<IEnumerable<ActivityUser>> GetAll()
        {
            return
                           await _unitOfWork.ActivityUsers.GetAll();
        }
       


        public async Task<ActivityUser> GetById(int? id)
        {
            return
                      await _unitOfWork.ActivityUsers.SingleOrDefault(i => i.IdUser == id && i.Active == 0);
        }
   
        public async Task Update(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser)
        {
            ActivityUserToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ActivityUser.Version = ActivityUserToBeUpdated.Version + 1;
            ActivityUser.Status = Status.Pending;
            ActivityUser.Active = 0;

            await _unitOfWork.ActivityUsers.Add(ActivityUser);
            await _unitOfWork.CommitAsync();
        }
        public async Task Approuve(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser)
        {
            ActivityUserToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            ActivityUser = ActivityUserToBeUpdated;
            ActivityUser.Version = ActivityUserToBeUpdated.Version + 1;
            ActivityUser.Status = Status.Rejected;
            ActivityUser.UpdatedOn = System.DateTime.UtcNow;
            ActivityUser.CreatedOn = ActivityUserToBeUpdated.CreatedOn;

            ActivityUser.Active = 0;

            await _unitOfWork.ActivityUsers.Add(ActivityUser);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(ActivityUser ActivityUserToBeUpdated, ActivityUser ActivityUser)
        {
            ActivityUserToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            ActivityUser.Version = ActivityUserToBeUpdated.Version + 1;
            ActivityUser.Status = Status.Rejected;
            ActivityUser.Active = 1;

            await _unitOfWork.ActivityUsers.Add(ActivityUser);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(ActivityUser ActivityUser)
        {
            ActivityUser.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<ActivityUser> ActivityUser)
        {
            foreach (var item in ActivityUser)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ActivityUser>> GetAllActif()
        {
            return
                             await _unitOfWork.ActivityUsers.GetAllActif();
        }

        public async Task<IEnumerable<ActivityUser>> GetAllInActif()
        {
            return
                             await _unitOfWork.ActivityUsers.GetAllInActif();
        }



        public Task<IEnumerable<ActivityUser>> GetActivityUsersNotAssignedByBu(int Id)
        {
            throw new NotImplementedException();
        }
        
    }
}
