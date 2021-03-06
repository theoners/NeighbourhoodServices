﻿namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        bool Update(string name, string description, string id);

        bool Delete(string id);

        Task<int> Add(string name, string description);
    }
}
