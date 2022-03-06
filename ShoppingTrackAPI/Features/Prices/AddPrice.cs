using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Prices
{
    public class AddPrice
    {
        public class Command : IRequest<Unit> 
        {
            public Command(Price price)
            {
                Price = price;
            }
            public Price Price { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<AddPrice> _logger;

            public Handler(ShoppingTrackContext context, ILogger<AddPrice> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.Prices.AddAsync(request.Price, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
