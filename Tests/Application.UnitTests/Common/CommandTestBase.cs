using EventsCore.Persistence;
using System;

namespace EventsCore.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly EventsCoreDbContext _context;
        public CommandTestBase()
        {
            _context = EventsCoreContextFactory.Create();
        }
        public void Dispose()
        {
            EventsCoreContextFactory.Destroy(_context);
        }
    }
}
