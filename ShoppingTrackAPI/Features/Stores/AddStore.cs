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
                _context.Stores.Add(request.Store);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }

    }
}
