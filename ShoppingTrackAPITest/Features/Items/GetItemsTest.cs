using ShoppingTrackAPI.Models;
using System.Text.Json;
using System.Text;
using System;
using ShoppingTrackAPI.Controllers;
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

namespace ShoppingTrackAPITest.Features.Items
{
    [Collection("api")]
    public class GetItemsTest
    {
        private readonly TestContext _testContext;
        //private readonly Handler _handler;

        public GetItemsTest(TestContext context)
        {
            _testContext = context;
            //_handler = new GetItems.Handler(_testContext.DbContext, new LoggerFactory().CreateLogger<AddStore>());
        }

        [Fact(Skip = "Get Items not converted to query yet.")]
        public async Task GetItems_ReturnsAllItems()
        {
            var response = await _testContext.Client.GetAsync("api/Items");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var itemsRetrieved = JsonSerializer.Deserialize<List<ShoppingTrackAPI.Models.Items>>(jsonString);
            Assert.NotNull(itemsRetrieved);
        }

        private ShoppingTrackAPI.Models.Items GetDefaultItem(string name) => 
            new ShoppingTrackAPI.Models.Items()
            {
                Name = name,
                User_Id = 1,
                Previous_Price = (decimal)1.98,
                Last_Store_Id = 1,
                CurrentStoreId = 1
            };
    }
}