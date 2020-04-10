using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeighbourhoodServices.Services.Data
{
    public interface IUsersService
    {
        int GetUserCount();

        T GetUser<T>(string id);
    }
}
