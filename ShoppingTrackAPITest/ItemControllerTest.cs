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
    public class ItemControllerTest
    {
        private readonly TestContext _testContext;

        public ItemControllerTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task AddItemWithoutItemId_ReturnsCreatedItem()
        {
            const string itemName = "testItem";
            var itemToAdd = GetDefaultItem(itemName);
            var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
            var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
            Assert.Equal(HttpStatusCode.Created, addItemResponse.StatusCode);
            var addItemJsonString = await addItemResponse.Content.ReadAsStringAsync();
            var itemCreated = JsonSerializer.Deserialize<Items>(addItemJsonString);
            Assert.NotNull(itemCreated);
            Assert.Equal(itemName, itemCreated.Name);
            Assert.NotNull(itemCreated.ItemId);
            // Ensure the itemId that is created is greater than 0
            Assert.InRange(itemCreated.ItemId, 1, int.MaxValue);
            var itemFromDatabase = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.ItemId == itemCreated.ItemId);
            Assert.NotNull(itemFromDatabase);
            Assert.Equal(itemCreated.Name, itemFromDatabase.Name);
        }

        [Fact]
        public async Task AddItemWithItemId_ReturnsCreatedItem()
        {
            const string itemName = "secondTest";
            var itemToAdd = GetDefaultItem(itemName);
            itemToAdd.ItemId = 2;
            var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
            var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
            Assert.Equal(HttpStatusCode.Created, addItemResponse.StatusCode);
            var addItemJsonString = await addItemResponse.Content.ReadAsStringAsync();
            var itemCreated = JsonSerializer.Deserialize<Items>(addItemJsonString);
            Assert.NotNull(itemCreated);
            Assert.Equal(itemName, itemCreated.Name);
            Assert.Equal(2, itemCreated.ItemId);
        }

        [Fact]
        public async Task AddItemThatExistAndNotDeleted_ReturnsBadRequest()
        {
            var itemFromDatabase = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.ItemId == 2);
            if (itemFromDatabase != null)
            {
                var itemToAdd = itemFromDatabase;
                var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
                var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                Assert.Equal(HttpStatusCode.BadRequest, addItemResponse.StatusCode);
            }
            else
            {
                // Create Item
                const string itemName = "BadRequestTest";
                var itemToAdd = GetDefaultItem(itemName);
                var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
                var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                var addItemJsonString = await addItemResponse.Content.ReadAsStringAsync();
                var itemCreated = JsonSerializer.Deserialize<Items>(addItemJsonString);

                // Send a second request to create the item
                addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                Assert.Equal(HttpStatusCode.BadRequest, addItemResponse.StatusCode);
            }
        }

        //item exist for user, but is deleted
        [Fact]
        public async Task AddItemThatExistAndDeleted_ReturnsCreatedAndMarksAsActive()
        {
            var itemFromDatabase = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.ItemId == 2);
            if (itemFromDatabase != null)
            {
                var itemToAdd = itemFromDatabase;
                itemToAdd.Deleted = true;
                var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
                var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                Assert.Equal(HttpStatusCode.BadRequest, addItemResponse.StatusCode);

                var itemAfterRequest = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.ItemId == 2);
                Assert.True(!itemAfterRequest.Deleted);
            }
            else
            {
                // Create Item as deleted
                const string itemName = "ExistingDeletedTest";
                var itemToAdd = GetDefaultItem(itemName);
                itemToAdd.Deleted = true;
                var stringContent = new StringContent(JsonSerializer.Serialize(itemToAdd), Encoding.UTF8, "application/json");
                var addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                var addItemJsonString = await addItemResponse.Content.ReadAsStringAsync();
                var itemCreated = JsonSerializer.Deserialize<Items>(addItemJsonString);

                // Send a second request to create the item
                addItemResponse = await _testContext.Client.PostAsync("api/Items", stringContent);
                Assert.Equal(HttpStatusCode.Created, addItemResponse.StatusCode);
                var itemAfterRequest = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.ItemId == itemCreated.ItemId);
                Console.WriteLine(itemAfterRequest.Name);
                Assert.True(!itemAfterRequest.Deleted);
            }
        }

        [Fact]
        public async Task GetItems_ReturnsAllItems()
        {
            var response = await _testContext.Client.GetAsync("api/Items");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var itemsRetrieved = JsonSerializer.Deserialize<List<Items>>(jsonString);
            Assert.NotNull(itemsRetrieved);
        }

        private Items GetDefaultItem(string name) => 
            new Items()
            {
                Name = name,
                User_Id = 1,
                Previous_Price = (decimal)1.98,
                Last_Store_Id = 1,
                CurrentStoreId = 1
            };

        //[Test] this will need to be changed for soft delete
        //public void DeleteItems()
        //{
        //    if(_testItem != null)
        //    {
        //        var task = _controller.DeleteItems(newItemId);
        //        task.Wait();
        //        var item = _controller.GetItems(newItemId).Result.Value;
        //        Assert.IsNull(item);
        //    }
        //    else
        //    {
        //        Assert.Fail("Item should not be null, please check if Add test failed.");
        //    }
        //}
    }
}