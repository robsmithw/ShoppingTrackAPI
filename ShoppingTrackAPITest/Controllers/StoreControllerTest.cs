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

namespace ShoppingTrackAPITest
{
    [Collection("api")]
    public class StoreControllerTest
    {
        private readonly TestContext _testContext;

        public StoreControllerTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task AddStoreWithoutItemId_ReturnsCreatedStore()
        {
            const string storeName = "testStore";
            var storeToAdd = GetDefaultStore(storeName);
            var stringContent = new StringContent(JsonSerializer.Serialize(storeToAdd), Encoding.UTF8, "application/json");
            var addstoreResponse = await _testContext.Client.PostAsync("api/Stores", stringContent);
            Assert.Equal(HttpStatusCode.Created, addstoreResponse.StatusCode);
            var addstoreJsonString = await addstoreResponse.Content.ReadAsStringAsync();
            var storeCreated = JsonSerializer.Deserialize<Stores>(addstoreJsonString);
            Assert.NotNull(storeCreated);
            Assert.Equal(storeName, storeCreated.Name);
            // Ensure the itemId that is created is greater than 0
            Assert.InRange(storeCreated.StoreId, 1, int.MaxValue);
            var storeFromDatabase = await _testContext.DbContext.Stores.FirstOrDefaultAsync(i => i.StoreId == storeCreated.StoreId);
            Assert.Null(storeFromDatabase);
            Assert.Equal(storeCreated.Name, storeFromDatabase.Name);
        }

        [Fact]
        public async Task GetStores_ReturnsAllStores()
        {
            var response = await _testContext.Client.GetAsync("api/Stores");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var storesRetrieved = JsonSerializer.Deserialize<List<Stores>>(jsonString);
            Assert.NotNull(storesRetrieved);
        }

        private Stores GetDefaultStore(string name) =>
            new Stores()
            {
                Name = name,
                PictureFileName = string.Empty
            };
    }
}