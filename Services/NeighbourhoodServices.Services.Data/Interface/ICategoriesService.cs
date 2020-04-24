using System.Threading.Tasks;

namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> Update(string name, string description, string id);
    }
}
