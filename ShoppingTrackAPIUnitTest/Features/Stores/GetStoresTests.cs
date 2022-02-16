using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ShoppingTrackAPIUnitTest.Setup;
using ShoppingTrackAPI.Controllers;
using ShoppingTrackAPI.Models;

using Xunit;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ShoppingTrackAPIUnitTest.Features.Stores
{
    public class GetStoresTests : UnitTestBase<GetStores>
    {
        [Fact]
        public async Task OnGetStores_WithNoStores_ShouldReturnAllStores()
        {
            // Arrange
            var mocker = GetDefaultMocker();
            await using var context = GetDataContext();
            mocker.Use(typeof(ShoppingTrackContext), context);

            var handler = mocker.CreateInstance<GetStores.Handler>();

            // Act
            List<ShoppingTrackAPI.Models.Stores> result = await handler.Handle(new GetStores.Query(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task OnGetStores_WithStoresPopulated_ShouldReturnAllStores()
        {
            // Arrange
            var mocker = GetDefaultMocker();
            await using var context = GetDataContext();
            mocker.Use(typeof(ShoppingTrackContext), context);

            var handler = mocker.CreateInstance<GetStores.Handler>();

            await context.Stores.AddAsync(new ShoppingTrackAPI.Models.Stores
            {
                StoreId = 1,
                Name = "Kroger"
            }, CancellationToken.None);

            await context.SaveChangesAsync(CancellationToken.None);

            // Act
            List<ShoppingTrackAPI.Models.Stores> result = await handler.Handle(new GetStores.Query(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);

            var store = result[0];
            store.Should().NotBeNull();
            store.Name.Should().Be("Kroger");
        }
    }
}
