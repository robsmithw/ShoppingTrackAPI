using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Items
{
    public class GetItems
    {
        public class Query : IRequest<List<Item>> 
        { 
            public Guid? UserId { get; set; }
            public Guid? StoreId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Item>>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetItems> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetItems> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<List<Item>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = request.UserId;
                var storeId = request.StoreId;

                if (userId.HasValue && !storeId.HasValue)
                {
                    return await _context.Items
                        .Where(x => x.UserId == userId && !x.IsDeleted)
                        .ToListAsync();
                }

                if (storeId.HasValue && userId.HasValue)
                {
                    return await _context.Items
                        .Where(x => x.CurrentStoreId == storeId && x.UserId == userId && !x.IsDeleted)
                        .ToListAsync();
                }

                return await _context.Items
                    .Where(x => !x.IsDeleted)
                    .ToListAsync(cancellationToken);
            }
        }

    }
}
