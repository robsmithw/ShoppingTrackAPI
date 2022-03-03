using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Stores
{
    public class GetStores
    {
        public class Query : IRequest<List<Store>> 
        { 
            public Guid? UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Store>>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetStores> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetStores> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<List<Store>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.UserId.HasValue)
                {
                    List<Store> stores = null;
                    var storesWithItemIds = await GetStoresWithItems(request.UserId.Value, cancellationToken);
                    
                    if (storesWithItemIds != null)
                    {
                        stores = await _context.Stores
                            .Where(x => storesWithItemIds.Contains(x.Id))
                            .ToListAsync(cancellationToken);
                    }

                    return stores;
                }

                return await _context.Stores.ToListAsync(cancellationToken);
            }

            public async Task<List<Guid>> GetStoresWithItems(Guid userId, CancellationToken cancellationToken)
            {
                return await _context.Items
                    .Where(x => x.UserId == userId)
                    .Select(x => x.CurrentStoreId)
                    .Distinct()
                    .ToListAsync(cancellationToken);
            }
        }

    }
}
