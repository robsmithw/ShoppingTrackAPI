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
    public class GetItem
    {
        public class Query : IRequest<Item> 
        { 
            public Query(Guid itemId)
            {
                ItemId = itemId;
            }
            public Guid ItemId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Item>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetItem> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetItem> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Item> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Items
                    .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);
            }
        }

    }
}
