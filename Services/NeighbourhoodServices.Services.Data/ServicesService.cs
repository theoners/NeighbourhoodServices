using System.Collections.Generic;
using System.Linq;
using NeighbourhoodServices.Data.Common.Repositories;
using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Services.Mapping;

namespace NeighbourhoodServices.Services.Data
{
    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public IEnumerable<T> GetAll<T>(string name , int? count = null)
        {
            IQueryable<Service> query =
                this.servicesRepository.All().OrderBy(x => x.CreatedOn).Where(x => x.Category.Name == name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}