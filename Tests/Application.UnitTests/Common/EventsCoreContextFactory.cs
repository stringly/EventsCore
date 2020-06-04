using EventsCore.Domain.Entities;
using EventsCore.Domain.ValueObjects;
using EventsCore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventsCore.Application.UnitTests.Common
{
    public class EventsCoreContextFactory
    {        
        public static EventsCoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EventsCoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new EventsCoreDbContext(options);
            context.Database.EnsureCreated();
            context.EventTypes.AddRange(new[]
            {
                new EventType("Training"),
                new EventType("Overtime")                
            });
            context.EventSeries.AddRange(new[]
            {
                new EventSeries("Event Series 1", "The first series of Events"),
                new EventSeries("Event Series 2", "The second series of Events")
            });
            context.Ranks.AddRange(new[]
            {
                new Rank("Private", "Pvt."),
                new Rank("Private First Class", "PFC"),
                new Rank("Specialist", "Spc.")
            });
            context.SaveChanges();
            context.Users.AddRange(new[]
            {
                new User("user123", 1, "Bob", "Jones", "1234", "bobjones@mail.com", "1234567890", 1),
                new User("user234", 2, "Steve", "Smith", "2222", "bobjones@mail.com", "1112223333", 2)
            });
            context.Events.Add(
                new Event(
                    "Test Event 1", 
                    "The first test event", 
                    new EventDates(
                        new DateTime(3000, 2, 1), 
                        new DateTime(3000, 2, 2),
                        new DateTime(3000, 1, 1),
                        new DateTime(3000, 1, 2),
                        new DateTimeTestProvider()),
                    new EventRegistrationRules(10),
                    1, 
                    1)
                );
            context.SaveChanges();
            return context;
        }
        public static void Destroy(EventsCoreDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
