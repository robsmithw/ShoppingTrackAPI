using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Stores
{
    public class DeleteStore
    {
        public class Command : IRequest<Unit> 
        {
            public Command(Guid id)
            {
                StoreId = id;
            }
            public Guid StoreId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<DeleteStore> _logger;

            public Handler(ShoppingTrackContext context, ILogger<DeleteStore> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == request.StoreId, cancellationToken);
                
                if (store is not null)
                {
                    _context.Stores.Remove(store);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }

    }
}
