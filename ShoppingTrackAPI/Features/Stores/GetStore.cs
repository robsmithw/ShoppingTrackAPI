using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Stores
{
    public class GetStore
    {
        public class Query : IRequest<Store> 
        {
            public Query(Guid storeId)
            {
                StoreId = storeId;
            }

            public Guid StoreId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Store>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetStore> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetStore> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Store> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Stores.FirstOrDefaultAsync(x => x.Id == request.StoreId, cancellationToken);
            }
        }

    }
}
