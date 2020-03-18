namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;

    public interface IServicesService
    {
        IEnumerable<T> GetAll<T>(string name,int? count = null);
    }
}
