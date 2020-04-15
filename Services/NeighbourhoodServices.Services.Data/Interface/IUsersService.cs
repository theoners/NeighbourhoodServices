using System.Threading.Tasks;

namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    
    public interface IUsersService
    {
        int GetUserCount();

        T GetUser<T>(string id);

        T SearchUser<T>(string userName, string city);

        IEnumerable<T> GetTopUsers<T>();
    }
}
