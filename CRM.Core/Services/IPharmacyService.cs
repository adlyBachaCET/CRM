﻿using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface IPharmacyService
    {
        Task<Pharmacy> GetById(int id);
        Task<Pharmacy> Create(Pharmacy newPharmacy);
        Task<List<Pharmacy>> CreateRange(List<Pharmacy> newPharmacy);
        Task Update(Pharmacy PharmacyToBeUpdated, Pharmacy Pharmacy);
        Task Delete(Pharmacy PharmacyToBeDeleted);
        Task DeleteRange(List<Pharmacy> Pharmacy);

        Task<IEnumerable<Pharmacy>> GetAll();
        Task<IEnumerable<Pharmacy>> GetAllActif();
        Task<IEnumerable<Pharmacy>> GetAllInActif();

    }
}
