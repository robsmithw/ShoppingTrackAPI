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
    public class DeletePrice
    {
        public class Command : IRequest<Price> 
        {
            public Command(Guid id)
            {
                PriceId = id;
            }
            public Guid PriceId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Price>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<DeletePrice> _logger;

            public Handler(ShoppingTrackContext context, ILogger<DeletePrice> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Price> Handle(Command request, CancellationToken cancellationToken)
            {
                var price = await _context.Prices
                    .FirstOrDefaultAsync(x => x.Id == request.PriceId, cancellationToken);

                if (price is not null)
                {
                    _context.Remove(price);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return price;
            }
        }

    }
}
