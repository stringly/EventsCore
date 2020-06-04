using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Ranks.Commands.UpsertRank
{
    /// <summary>
    /// Implements <see cref="IRequestHandler{TRequest, TResponse}"></see> to handle a request to update/insert a Rank.
    /// </summary>
    public class UpsertRankCommandHandler : IRequestHandler<UpsertRankCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public UpsertRankCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request to insert or update the <see cref="Rank"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the upserted entity.</see></returns>
        /// <exception cref="ValidationException">
        /// Throw when:
        /// <list type="bullet">
        /// <item><description>the "FullName" property of the request parameter is in use by another <see cref="Rank"></see></description></item>
        /// <item><description>the "Abbrev" property of the request parameter is in use by another <see cref="Rank"></see></description></item>
        /// </list>
        /// </exception>
        /// <exception cref="NotFoundException">Throw when the "Id" property of the request parameter is present, but does not match the Id of any existing <see cref="Rank"></see></exception>
        public async Task<int> Handle(UpsertRankCommand request, CancellationToken cancellationToken)
        {
            Rank entity;
            if (request.Id.HasValue)
            {
                var abbrevIsTaken = _context.Ranks.Any(x => x.Abbreviation == request.Abbrev && x.Id != request.Id);
                if (abbrevIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() {new ValidationFailure(nameof(Rank.Abbreviation), $"The abbreviation \"{request.Abbrev}\" is already in use.")});
                }
                var nameIsTaken = _context.Ranks.Any(x => x.FullName == request.FullName && x.Id != request.Id);
                if (nameIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Rank.Abbreviation), $"The Rank name \"{request.FullName}\" is already in use.") });
                }
                entity = await _context.Ranks.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Rank), request.Id);
                }
                entity.UpdateAbbreviation(request.Abbrev);
                entity.UpdateFullName(request.FullName);
            }
            else
            {
                var abbrevIsTaken = _context.Ranks.Any(x => x.Abbreviation == request.Abbrev);
                if (abbrevIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Rank.Abbreviation), $"The abbreviation \"{request.Abbrev}\" is already in use.") });
                }
                var nameIsTaken = _context.Ranks.Any(x => x.FullName == request.FullName);
                if (nameIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Rank.Abbreviation), $"The Rank name \"{request.FullName}\" is already in use.") });
                }
                entity = new Rank(request.FullName, request.Abbrev);
                await _context.Ranks.AddAsync(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
