using AutoMapper;
using EventsCore.Application.Events.Queries.GetEventsList;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Queries
{
    [Collection("QueryCollection")]
    public class GetEventListQueryTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetEventListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Handle_Get_Event_List_Test()
        {
            // Arrange
            var sut = new GetEventListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventListQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventListVm>();
            result.Events.Count.ShouldBe(3);

        }
        [Fact]
        public async Task Handle_Get_Events_List_With_EventTypeFilter()
        {
            // Arrange            
            var sut = new GetEventListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventListQuery() { EventTypeId = 1 }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventListVm>();
            result.Events.Count.ShouldBe(1);

        }
        [Fact]
        public async Task Handle_Get_Events_With_Paging()
        {
            // Arrange            
            var sut = new GetEventListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventListQuery() { PageSize = 2 }, CancellationToken.None);
            var result2 = await sut.Handle(new GetEventListQuery() { PageSize = 2, CurrentPage = 2 }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventListVm>();
            result.Events.Count.ShouldBe(2);
            result2.ShouldBeOfType<EventListVm>();
            result2.Events.Count.ShouldBe(1);
        }
        [Fact]
        public async Task Handle_Get_Events_With_Search()
        {
            // Arrange
            var sut = new GetEventListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventListQuery() { SearchString = "Searchable" }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventListVm>();
            result.Events.Count.ShouldBe(1);
            Assert.Equal(3, result.Events.First().Id);
        }
    }
}
