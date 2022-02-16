using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

using Microsoft.EntityFrameworkCore;

using ShoppingTrackAPI.Models;
using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Controllers;
using static ShoppingTrackAPI.Controllers.AddStore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace ShoppingTrackAPITest.Features.Stores
{
    [Collection("api")]
    public class AddStoreTest
    {
        private readonly TestContext _testContext;
        private readonly Handler _handler;

        public AddStoreTest(TestContext context)
        {
            _testContext = context;
            _handler = new AddStore.Handler(_testContext.DbContext, new LoggerFactory().CreateLogger<AddStore>());
        }

        [Fact]
        public async Task AddStoreWithoutStoreId_ReturnsCreatedStore()
        {
            // Arrange
            const string storeName = "testStore";
            var storeToAdd = GetDefaultStore(storeName);
            var command = new AddStore.Command(storeToAdd);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            var storeCreated = await _testContext.DbContext.Stores
                .FirstOrDefaultAsync(x => x.Name == storeToAdd.Name, CancellationToken.None);
            Assert.NotNull(storeCreated);
            Assert.Equal(storeName, storeCreated.Name);
            // Ensure the storeId that is created is greater than 0
            Assert.InRange(storeCreated.StoreId, 1, int.MaxValue);
        }

        private ShoppingTrackAPI.Models.Stores GetDefaultStore(string name) =>
            new ShoppingTrackAPI.Models.Stores()
            {
                Name = name
            };
    }
}