﻿using CRM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
    public interface ILocationService
    {
        Task<Location> GetById(int id);
        Task<Location> Create(Location newEstablishment);
        Task<List<Location>> CreateRange(List<Location> newEstablishment);
        Task Update(Location EstablishmentToBeUpdated, Location Establishment);
        Task Delete(Location EstablishmentToBeDeleted);
        Task DeleteRange(List<Location> Establishment);

        Task<IEnumerable<Location>> GetAll();
        Task<IEnumerable<Location>> GetAllActif();
        Task<IEnumerable<Location>> GetAllInActif();

    }
}