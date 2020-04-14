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
                new Category() { Name = "Градинарство" , Description = "Ремонт"},
                new Category() { Name = "Преместване" , Description = "Ремонт"},
                new Category() { Name = "Подръжка-ремонт на кола", Description = "Ремонт" },
                new Category() { Name = "Деца" , Description = "Ремонт"},
                new Category() { Name = "Животни" , Description = "Ремонт"},
                new Category() { Name = "Информационни Услуги", Description = "Ремонт" },
                new Category() { Name = "Курсове" , Description = "Ремонт"},
                new Category() { Name = "Административни", Description = "Ремонт" },
                new Category() { Name = "Мода Здраве", Description = "Ремонт" },
                new Category() { Name = "Спорт" , Description = "Ремонт"},
            };

            await dbContext.AddRangeAsync(categories);
        }
    }
}
