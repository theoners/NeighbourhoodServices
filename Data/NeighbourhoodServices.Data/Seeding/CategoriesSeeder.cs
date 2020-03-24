namespace NeighbourhoodServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using NeighbourhoodServices.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>()
            {
                new Category() { Name = "Ремонт", Description = "Ремонт"},
                new Category() { Name = "Градинарство" },
                new Category() { Name = "Преместване" },
                new Category() { Name = "Подръжка-ремонт на кола" },
                new Category() { Name = "Деца" },
                new Category() { Name = "Животни" },
                new Category() { Name = "Информационни Услуги" },
                new Category() { Name = "Курсове" },
                new Category() { Name = "Административни" },
                new Category() { Name = "Мода Здраве" },
                new Category() { Name = "Спорт" },
            };

            await dbContext.AddRangeAsync(categories);
        }
    }
}
