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

namespace ShoppingTrackAPITest
{
    [Collection("api")]
    public class AddItemTest
    {
        private readonly TestContext _testContext;
        private readonly AddItem.Handler _handler;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private readonly string _itemExistErrorMessage = "The item already exist for this user and cannot be added twice.";

        public AddItemTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task AddItemWithoutItemId_ReturnsCreatedItem()
        {
            const string itemName = "testItem";
            var itemToAdd = GetDefaultItem(itemName);

            var response = await _handler.Handle(new AddItem.Command(itemToAdd), CancellationToken.None);
            
            Assert.NotNull(response);
            Assert.True(response.Successful);
            var itemCreated = response.Item;
            Assert.NotNull(itemCreated);
            Assert.Equal(itemName, itemCreated.Name);
            // Ensure the itemId that is created
            Assert.NotNull(itemCreated.Id);
            var itemFromDatabase = await _testContext.DbContext.Items.FirstOrDefaultAsync(i => i.Id == itemCreated.Id);
            Assert.NotNull(itemFromDatabase);
            Assert.Equal(itemCreated.Name, itemFromDatabase.Name);
        }

        [Fact]
        public async Task AddItemWithItemId_ReturnsCreatedItem()
        {
            //arrange
            const string itemName = "secondTest";
            var itemToAdd = GetDefaultItem(itemName);
            itemToAdd.Id = Guid.NewGuid();
            
            //act
            var response = await _handler.Handle(new AddItem.Command(itemToAdd), CancellationToken.None);
            
            //assert
            Assert.NotNull(response);
            Assert.True(response.Successful);
            var itemCreated = response.Item;
            Assert.NotNull(itemCreated);
            Assert.Equal(itemName, itemCreated.Name);
            Assert.Equal(itemToAdd.Id, itemCreated.Id);
        }

        [Fact]
        public async Task AddItemThatExistAndNotDeleted_ReturnsBadRequest()
        {
            // Create Item
            //Arrange
            const string itemName = "BadRequestTest";
            var itemToAdd = GetDefaultItem(itemName);
            await _testContext.DbContext.Items.AddAsync(itemToAdd, _cancellationToken);
            await _testContext.DbContext.SaveChangesAsync(_cancellationToken);

            //Act
            var response = await _handler.Handle(new AddItem.Command(itemToAdd), CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.Successful);
            var itemCreated = response.Item;
            Assert.Null(itemCreated);
            var errorMessage = response.Error;
            Assert.NotNull(errorMessage);
            Assert.Equal(_itemExistErrorMessage, errorMessage);
        }

        //item exist for user, but is deleted
        [Fact]
        public async Task AddItemThatExistAndDeleted_ReturnsCreatedAndMarksAsActive()
        {
            // Create Item as deleted
            //Arrange
            const string itemName = "ExistingDeletedTest";
            var itemToAdd = GetDefaultItem(itemName);
            itemToAdd.IsDeleted = true;
            await _testContext.DbContext.Items.AddAsync(itemToAdd, _cancellationToken);
            await _testContext.DbContext.SaveChangesAsync(_cancellationToken);
            
            //Act
            var response = await _handler.Handle(new AddItem.Command(itemToAdd), CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Successful);
            var itemAfterRequest = response.Item;
            Assert.NotNull(itemAfterRequest);
            Assert.False(itemAfterRequest.IsDeleted);
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