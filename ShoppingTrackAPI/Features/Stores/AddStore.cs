using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Stores
{
    public class AddStore
    {
        public class Command : IRequest<Unit> 
        {
            public Command(Store store)
            {
                Store = store;
            }
            public Store Store { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<AddStore> _logger;

            public Handler(ShoppingTrackContext context, ILogger<AddStore> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Store.Id == default) request.Store.Id = Guid.NewGuid();
                await _context.Stores.AddAsync(request.Store, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
