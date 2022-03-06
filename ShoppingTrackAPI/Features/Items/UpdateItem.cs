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
    public class UpdateItem
    {
        public class Command : IRequest<Item> 
        { 
            public Command(Guid itemId, Item item)
            {
                ItemId = itemId;
                Item = item;
            }
            public Guid ItemId { get; set; }
            public Item Item { get; set; }
        }

        public class Handler : IRequestHandler<Command, Item>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<UpdateItem> _logger;

            public Handler(ShoppingTrackContext context, ILogger<UpdateItem> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<Item> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedItem = request.Item;
                var item = await _context.Items
                    .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);
                    
                if (IsItemModified(item, updatedItem))
                {
                    item = UpdateItemProperties(item, updatedItem);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return item;
            }

            private Item UpdateItemProperties(Item originalItem, Item updatedItem)
            {
                originalItem.CurrentStoreId = updatedItem.CurrentStoreId;
                originalItem.Name = updatedItem.Name;
                originalItem.LastStoreId = updatedItem.LastStoreId;
                originalItem.Purchased = updatedItem.Purchased;
                originalItem.PreviousPrice = updatedItem.PreviousPrice;
                originalItem.ModifiedDate = DateTime.UtcNow;

                return originalItem;
            }

            private bool IsItemModified(Item originalItem, Item updatedItem)
            {
                return originalItem.CurrentStoreId != updatedItem.CurrentStoreId
                || originalItem.Name != updatedItem.Name
                || originalItem.LastStoreId != updatedItem.LastStoreId
                || originalItem.Purchased != updatedItem.Purchased
                || originalItem.PreviousPrice != updatedItem.PreviousPrice;
            }
        }

    }
}
