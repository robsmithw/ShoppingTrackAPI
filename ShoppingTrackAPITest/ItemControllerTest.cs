using NUnit.Framework;
using ShoppingTrackAPI.Models;
using ShoppingTrackAPI.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShoppingTrackAPITest
{
    public class ItemControllerTest
    {
        const int newItemId = int.MaxValue;
        static ShoppingTrackContext _context = new ShoppingTrackContext();
        static ILogger<ItemsController> logger;
        Items _testItem;
        ItemsController _controller = new ItemsController(_context, logger);

        //[Test] this will need to be changed for soft delete
        //public void AddAndGetItems()
        //{
        //    var itemToAdd = new Items();
        //    itemToAdd.ItemId = newItemId;
        //    itemToAdd.Name = "test";
        //    itemToAdd.User_Id = 0;
        //    itemToAdd.Previous_Price = (decimal)1.98;
        //    itemToAdd.Last_Store_Id = 1;
        //    var task = _controller.PostItems(itemToAdd);
        //    task.Wait(120000);
        //    _testItem = _controller.GetItems(newItemId).Result.Value;
        //    //var temp2 = temp.Result.Value;
        //    Assert.IsNotNull(_testItem);
        //}

        [Test]
        public void GetAllItems()
        {
            var items = _controller.GetItems();
            items.Wait();
            Assert.IsNotNull(items.Result.Value);
        }

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