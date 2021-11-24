using CRM.Core;
using CRM.Core.Models;
using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Services
{
    public class ActivityService : IActivityService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Activity> Create(Activity newActivity)
        {

            await _unitOfWork.Activitys.Add(newActivity);
            await _unitOfWork.CommitAsync();
            return newActivity;
        }
        public async Task<List<Activity>> CreateRange(List<Activity> newActivity)
        {

            await _unitOfWork.Activitys.AddRange(newActivity);
            await _unitOfWork.CommitAsync();
            return newActivity;
        }
  
        public async Task<IEnumerable<Activity>> GetAll()
        {
            return
                           await _unitOfWork.Activitys.GetAll();
        }
       
        /* public async Task Delete(Activity Activity)
         {
             _unitOfWork.Activitys.Remove(Activity);
             await _unitOfWork.CommitAsync();
         }*/

        //public async Task<IEnumerable<Activity>> GetAllWithArtiste()
        //{
        //    return await _unitOfWork.Activitys
        //          .GetAllWithArtisteAsync();
        //}

        public async Task<Activity> GetById(int? id)
        {
            return
                      await _unitOfWork.Activitys.SingleOrDefault(i => i.IdActivity == id && i.Active == 0);
        }
        public async Task<List<Activity>> GetByIdUser(int id)
        {   List<Activity> Activities = new List<Activity>();
            var List = await _unitOfWork.ActivityUsers.Find(i => i.IdUser == id && i.Active == 0);
            foreach(var item in List)
            {
                var Activity = await _unitOfWork.Activitys.SingleOrDefault(i => i.IdActivity == item.IdActivity && i.Active == 0);

                Activities.Add(Activity);
            }
                return
                      Activities;
        }
        public async Task<List<Activity>> GetByIdUserByToday(int id)
        {
            List<Activity> Activities = new List<Activity>();
            var List = await _unitOfWork.ActivityUsers.Find(i => i.IdUser == id && i.Active == 0);
            foreach (var item in List)
            {
                var Activity = await _unitOfWork.Activitys.SingleOrDefault(i => i.IdActivity == item.IdActivity && i.Active == 0 &&i.Start.Date==DateTime.Now.Date);

                Activities.Add(Activity);
            }
            return
                  Activities;
        }
        public async Task Update(Activity ActivityToBeUpdated, Activity Activity)
        {
            ActivityToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Activity.Version = ActivityToBeUpdated.Version + 1;
            Activity.IdActivity = ActivityToBeUpdated.IdActivity;
            Activity.Status = Status.Pending;
            Activity.Active = 0;

            await _unitOfWork.Activitys.Add(Activity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Approuve(Activity ActivityToBeUpdated, Activity Activity)
        {
            ActivityToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();
            Activity = ActivityToBeUpdated;
            Activity.Version = ActivityToBeUpdated.Version + 1;
            Activity.IdActivity = ActivityToBeUpdated.IdActivity;
            Activity.Status = Status.Rejected;
            Activity.UpdatedOn = System.DateTime.UtcNow;
            Activity.CreatedOn = ActivityToBeUpdated.CreatedOn;

            Activity.Active = 0;

            await _unitOfWork.Activitys.Add(Activity);
            await _unitOfWork.CommitAsync();

        }
        public async Task Reject(Activity ActivityToBeUpdated, Activity Activity)
        {
            ActivityToBeUpdated.Active = 1;
            await _unitOfWork.CommitAsync();

            Activity.Version = ActivityToBeUpdated.Version + 1;
            Activity.IdActivity = ActivityToBeUpdated.IdActivity;
            Activity.Status = Status.Rejected;
            Activity.Active = 1;

            await _unitOfWork.Activitys.Add(Activity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Activity Activity)
        {
            //Activity musi =  _unitOfWork.Activitys.SingleOrDefaultAsync(x=>x.Id == ActivityToBeUpdated.Id);
            Activity.Active = 1;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRange(List<Activity> Activity)
        {
            foreach (var item in Activity)
            {
                item.Active = 1;

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Activity>> GetAllActif()
        {
            return
                             await _unitOfWork.Activitys.GetAllActif();
        }

        public async Task<IEnumerable<Activity>> GetAllInActif()
        {
            return
                             await _unitOfWork.Activitys.GetAllInActif();
        }



        public Task<IEnumerable<Activity>> GetActivitysNotAssignedByBu(int Id)
        {
            throw new NotImplementedException();
        }
        //public Task<Activity> CreateActivity(Activity newActivity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteActivity(Activity Activity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Activity> GetActivityById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Activity>> GetActivitysByArtisteId(int artiste)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateActivity(Activity ActivityToBeUpdated, Activity Activity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
