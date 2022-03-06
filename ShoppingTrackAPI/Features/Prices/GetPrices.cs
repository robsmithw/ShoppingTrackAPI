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
    public class GetPrices
    {
        public class Query : IRequest<List<Price>> 
        {
            public Guid? ItemId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Price>>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetPrices> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetPrices> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<List<Price>> Handle(Query request, CancellationToken cancellationToken)
            {
                var itemId = request.ItemId;

                if (itemId.HasValue)
                {
                    return await _context.Prices
                        .Where(price => price.ItemId == itemId)
                        .ToListAsync(cancellationToken);
                }

                return await _context.Prices.ToListAsync(cancellationToken);
            }
        }

    }
}
