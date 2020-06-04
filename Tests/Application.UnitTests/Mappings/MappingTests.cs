using AutoMapper;
using EventsCore.Application.EventTypes.Queries.GetEventTypesList;
using EventsCore.Domain.Entities;
using Shouldly;
using Xunit;

namespace EventsCore.Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void Should_Have_Valid_Configuration()
        {
            _configuration.AssertConfigurationIsValid();
        } 
        [Fact]
        public void Should_Map_EventType_To_EventTypeDto()
        {
            var entity = new EventType("Test");
            var result = _mapper.Map<EventTypeDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<EventTypeDto>();
        }
    }
}
