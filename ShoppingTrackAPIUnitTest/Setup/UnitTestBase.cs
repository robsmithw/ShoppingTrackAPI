using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

using ShoppingTrackAPI.Models;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingTrackAPIUnitTest.Setup
{

    public abstract class UnitTestBase<T>
    {
        public AutoMocker GetDefaultMocker()
        {
            var mocker = new AutoMocker();

            mocker.GetMock<ILogger<T>>();

            return mocker;
        }

        public static ShoppingTrackContext GetDataContext()
        {
            return new ShoppingTrackContext(GetDataContextOptions());
        }

        private static DbContextOptions<ShoppingTrackContext> GetDataContextOptions()
        {
            Environment.SetEnvironmentVariable("IS_UNIT_TESTING", "true");
            // https://stackoverflow.com/questions/52810039/moq-and-setting-up-db-context
            var dbName = Guid.NewGuid().ToString().Substring(0, 5);
            return new DbContextOptionsBuilder<ShoppingTrackContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
    }
}
