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
    public class DeleteItem
    {
        public class Command : IRequest<Item> 
        { 
            public Command(Guid itemId)
            {
                ItemId = itemId;
            }
            public Guid ItemId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Item>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<DeleteItem> _logger;

            public Handler(ShoppingTrackContext context, ILogger<DeleteItem> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Item> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);
                    
                if (item is not null)
                {
                    item.IsDeleted = true;
                    item.ModifiedDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return item;
            }
        }

    }
}
