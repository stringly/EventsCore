using EventsCore.Domain.Entities;
using EventsCore.Persistence;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.WebUI.IntegrationTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(EventsCoreDbContext context)
        {
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
            
            context.SaveChanges();
            var entity = context.Events.Find(3);
            entity.RegisterUser(1, "Bob Jones #1234", "bobjones@mail.com", "1234567890", new DateTimeTestProvider());

            context.SaveChanges();
        }
    }
}
