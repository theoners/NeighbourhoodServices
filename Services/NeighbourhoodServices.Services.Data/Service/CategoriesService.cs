namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> Update(string name, string description, string id)
        {
            var result = -1;
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == int.Parse(id));

            if (category == null)
            {
                return result;
            }

            category.Description = description;
            category.Name = name;
            result = await this.categoriesRepository.SaveChangesAsync();

            return result;
        }
    }
}