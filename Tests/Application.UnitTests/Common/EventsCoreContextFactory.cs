using EventsCore.Domain.Entities;
using EventsCore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace EventsCore.Application.UnitTests.Common
{
    public class EventsCoreContextFactory
    {        
        public static EventsCoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EventsCoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new EventsCoreDbContext(options, new CurrentUserServiceTesting(), new DateTimeTestProvider());
            context.Database.EnsureCreated();
            context.EventSeries.AddRange(new[]
            {
                new EventSeries("Event Series 1", "The first series of Events"),
                new EventSeries("Event Series 2", "The second series of Events")
            });
            context.SaveChanges();
            context.Users.AddRange(new[]
            {
                new User("user123", 1, "Bob", "Jones", "1234", "bobjones@mail.com", "1234567890", 1),
                new User("user234", 2, "Steve", "Smith", "2222", "bobjones@mail.com", "1112223333", 2)
            });
            var user = context.Users.Find(1);
            var role = context.UserRoleTypes.Find(1);
            user.AddToRole(role);
            context.SaveChanges();
            context.Events.AddRange(
                new Event(
                    "Test Event 1", 
                    "The first test event",
                    1,
                    1,
                    new DateTime(3000, 2, 1), 
                    new DateTime(3000, 2, 2),
                    new DateTime(3000, 1, 1),
                    new DateTime(3000, 1, 2),                        
                    10),
                new Event(
                    "Test Event 2",
                    "The second test event",
                    2,
                    new DateTime(3000, 4, 1),
                    new DateTime(3000, 4, 2),
                    new DateTime(3000, 3, 1),
                    new DateTime(3000, 3, 2),
                    10),
                new Event(
                    "Searchable Event 3",
                    "The third test event",
                    2,
                    new DateTime(2020, 2, 1),
                    new DateTime(2020, 2, 2),
                    new DateTime(2020, 1, 1),
                    new DateTime(2020, 1, 30),
                    20)
                );
            var entity = context.Events.Find(3);
            entity.RegisterUser(1, "Bob Jones #1234", "bobjones@mail.com", "1234567890", new DateTimeTestProvider());
            context.SaveChangesAsync(CancellationToken.None);
            return context;
        }
        public static void Destroy(EventsCoreDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
