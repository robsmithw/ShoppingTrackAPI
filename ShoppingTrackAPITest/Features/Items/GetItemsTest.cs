using ShoppingTrackAPI.Models;
using System.Text.Json;
using System.Text;
using System;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Items;
using System.Threading;

namespace ShoppingTrackAPITest.Features.Items
{
    [Collection("api")]
    public class GetItemsTest
    {
        private readonly TestContext _testContext;
        private readonly GetItems.Handler _handler;

        public GetItemsTest(TestContext context)
        {
            _testContext = context;
            _handler = new GetItems.Handler(_testContext.DbContext, new LoggerFactory().CreateLogger<GetItems>());
        }

        [Fact]
        public async Task GetItems_ReturnsAllNotDeletedItems()
        {
            var item = GetDefaultItem("Apples");
            item.IsDeleted = false;
            var deletedItem = GetDefaultItem("Meatballs");
            item.IsDeleted = true;

            await _testContext.DbContext.Items.AddRangeAsync(new List<Item>(){ item, deletedItem }, CancellationToken.None);
            await _testContext.DbContext.SaveChangesAsync(CancellationToken.None);

            var response = await _handler.Handle(new GetItems.Query(), CancellationToken.None);

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