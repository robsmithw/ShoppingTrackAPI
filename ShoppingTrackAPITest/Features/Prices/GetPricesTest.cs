using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using Xunit;

using ShoppingTrackAPI.Models;
using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Items;
using ShoppingTrackAPI.Features.Prices;

namespace ShoppingTrackAPITest.Features.Items
{
    [Collection("api")]
    public class GetPricesTest
    {
        private readonly TestContext _testContext;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public GetPricesTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task GetPrices_ReturnsAllPricesForItem()
        {
            // Arrange
            var item = GetDefaultItem("Apples");
            item.IsDeleted = false;
            var price = GetDefaultPrice(item.Id, item.UserId, item.CurrentStoreId);

            await _testContext.DbContext.Items.AddAsync(item, _cancellationToken);
            await _testContext.DbContext.Prices.AddAsync(price, _cancellationToken);
            await _testContext.DbContext.SaveChangesAsync(_cancellationToken);

            // Act
            var response = await _testContext.Mediator.Send(new GetPrices.Query() { ItemId = item.Id }, _cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Count == 1);
        }

        private Price GetDefaultPrice(Guid itemId, Guid userId, Guid storeId) =>
            new Price()
            {
                CurrentPrice = (decimal)2.18,
                ItemId = itemId,
                UserId = userId,
                StoreId = storeId,
                DateOfPrice = DateTime.UtcNow
            };

        private Item GetDefaultItem(string name) => 
            new Item()
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = Guid.NewGuid(),
                PreviousPrice = (decimal)1.98,
                LastStoreId = Guid.NewGuid(),
                CurrentStoreId = Guid.NewGuid()
            };
    }
}