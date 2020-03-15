using System.Collections.Generic;

namespace NeighbourhoodServices.Services.Data
{
   public interface IAnnouncementService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
