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
    public class AddItem
    {
        public class Command : IRequest<AddItemResultDto> 
        { 
            public Command(Item requestedItem)
            {
                RequestedItem = requestedItem;
            }
            public Item RequestedItem { get; set; }
        }

        public class AddItemResultDto
        {
            public AddItemResultDto(bool successful, string error)
            {
                Successful = successful;
                Error = error;
            }
            public bool Successful { get; set; }
            public string Error { get; set; }
            public Item Item { get; set; }
        }

        public class Handler : IRequestHandler<Command, AddItemResultDto>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<AddItem> _logger;

            public Handler(ShoppingTrackContext context, ILogger<AddItem> logger)
            {
                _context = context;
                _logger = logger;
            }
            
            public async Task<AddItemResultDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var resultDto = new AddItemResultDto(false, "Unknown error.");
                var requestedItem = request.RequestedItem;
                var item = await _context.Items
                    .FirstOrDefaultAsync(x => 
                        x.Name == requestedItem.Name 
                        && x.UserId == requestedItem.UserId, cancellationToken);
                
                //item doesnt exist for user
                if (item == null)
                {
                    if (requestedItem.Id == default) requestedItem.Id = Guid.NewGuid();
                    await _context.Items.AddAsync(requestedItem, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                
                //item exist for user and is NOT deleted (bad request)
                if(item != null && !item.IsDeleted)
                {
                    var error = "The item already exist for this user and cannot be added twice.";
                    resultDto.Error = error;
                    return resultDto;
                }
                
                //item exist for user, but is deleted
                if(item != null && item.IsDeleted)
                {
                    item.IsDeleted = false;
                    item.ModifiedDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync(cancellationToken);
                }
                resultDto.Error = string.Empty;
                resultDto.Successful = true;
                resultDto.Item = item ?? requestedItem;

                return resultDto;
            }
        }

    }
}
