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

        public async Task<int> Add(string name, string description)
        {

            var category = new Category()
            {
                Name = name,
                Description = description,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
            return category.Id;
        }

        public bool Delete(string id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == int.Parse(id));
            if (category != null)
            {
                this.categoriesRepository.Delete(category);
                this.categoriesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.AllAsNoTracking().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool Update(string name, string description, string id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == int.Parse(id));

            if (category == null)
            {
                return false;
            }

            category.Description = description;
            category.Name = name;
            var result = this.categoriesRepository.SaveChangesAsync().Result;

            return result > 0 ? true : false;
        }
    }
}
