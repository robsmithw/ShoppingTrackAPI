using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using Xunit;

using ShoppingTrackAPI.Models;
using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Items;

namespace ShoppingTrackAPITest.Features.Items
{
    [Collection("api")]
    public class GetItemsTest
    {
        private readonly TestContext _testContext;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public GetItemsTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task GetItems_ReturnsAllNotDeletedItems()
        {
            var item = GetDefaultItem("Apples");
            item.IsDeleted = false;
            var deletedItem = GetDefaultItem("Meatballs");
            item.IsDeleted = true;

            await _testContext.DbContext.Items.AddRangeAsync(new List<Item>(){ item, deletedItem }, _cancellationToken);
            await _testContext.DbContext.SaveChangesAsync(_cancellationToken);

            var response = await _testContext.Mediator.Send(new GetItems.Query(), _cancellationToken);

            Assert.NotNull(response);
            Assert.InRange(response.Count, 1, int.MaxValue);
        }

        private Item GetDefaultItem(string name) => 
            new Item()
            {
                Name = name,
                UserId = Guid.NewGuid(),
                PreviousPrice = (decimal)1.98,
                LastStoreId = Guid.NewGuid(),
                CurrentStoreId = Guid.NewGuid()
            };
    }
}