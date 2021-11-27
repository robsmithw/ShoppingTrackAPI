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
    public class GetStores
    {
        public class Query : IRequest<List<Stores>> { }

        public class Handler : IRequestHandler<Query, List<Stores>>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<GetStores> _logger;

            public Handler(ShoppingTrackContext context, ILogger<GetStores> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<List<Stores>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Stores.ToListAsync(cancellationToken);
            }
        }

    }
}
