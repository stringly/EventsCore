using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Persistence;
using System;
using Xunit;

namespace EventsCore.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public EventsCoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set;}
        public QueryTestFixture()
        {
            Context = EventsCoreContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<MappingProfile>();
            });
            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            EventsCoreContextFactory.Destroy(Context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
