using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Controllers
{
    public class AddStore
    {
        public class Command : IRequest<Unit> 
        {
            public Command(Stores store)
            {
                Store = store;
            }
            public Stores Store { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetStores> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetStores> logger)
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
