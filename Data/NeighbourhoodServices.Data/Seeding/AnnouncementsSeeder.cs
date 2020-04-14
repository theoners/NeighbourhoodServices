namespace NeighbourhoodServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NeighbourhoodServices.Data.Models;

    class AnnouncementsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Announcements.Count() > 100)
            {
               return;
            }

            var place = new char[8];
            var description = new char[200];
            var title = new char[40];

            var announcements = new List<Announcement>();
            var random = new Random();
            var users = dbContext.Users.ToList();
            for (int i = 0; i < 5; i++)
            {
                var announcement = new Announcement
                {
                    CategoryId = random.Next(5, 16),
                    ServiceType = (ServiceType)random.Next(1, 3),
                    Description = this.RandomString(random.Next(20)),
                    Place = this.RandomString(random.Next(6, 20)),
                    Title = this.RandomString(random.Next(10, 30)),
                    UserId = users[random.Next(users.Count)].Id,
                    Price = 50,
                };

                announcements.Add(announcement);
            }

            await dbContext.AddRangeAsync(announcements);
        }

        private string RandomString(int stringLength)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ?!;,.";
            var random = new Random();

            var randomString = new char[stringLength];
            for (int i = 0; i < stringLength; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomString);
        }
    }
}
