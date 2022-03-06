using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Prices
{
    public class GetEstimate
    {
        public class Query : IRequest<string> 
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetEstimate> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetEstimate> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = request.UserId;
                decimal estimate = 0;

                //get all items that for user that are not purchased or deleted.
                var purchasedItemIds = await _context.Items
                    .Where(item => item.UserId == userId && !item.Purchased && !item.IsDeleted)
                    .Select(item => item.Id)
                    .ToListAsync(cancellationToken);
                var prices = await _context.Prices
                    .Where(price => purchasedItemIds.Contains(price.ItemId))
                    .ToListAsync(cancellationToken);
                foreach(var price in prices)
                {
                    estimate += price.CurrentPrice;
                }

                var estimateJson = JsonConvert.SerializeObject(estimate);

                return estimateJson;
            }
        }

    }
}
