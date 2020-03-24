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
            if (dbContext.Announcements.Count() > 500)
            {
               return;
            }

            var place = new char[8];
            var description = new char[200];
            var title = new char[40];

            var announcements = new List<Announcement>();
            var random = new Random();
            var users = dbContext.Users.ToList();
            for (int i = 0; i < 500; i++)
            {
                var announcement = new Announcement
                {
                    CategoryId = random.Next(1, 12),
                    ServiceType = (ServiceType)random.Next(1, 2),
                    Description = this.RandomString(random.Next(100, 200)),
                    Place = this.RandomString(random.Next(6, 20)),
                    Title = this.RandomString(random.Next(10, 30)),
                    User = users[random.Next(users.Count)],
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
