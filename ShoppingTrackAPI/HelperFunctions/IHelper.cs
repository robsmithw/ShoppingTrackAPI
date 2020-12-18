using System;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.HelperFunctions
{
    public interface IHelper
    {
        public bool SignIn(User userInDatabase, User userSupplied);
        public string CalculateHash(string toHash);
        public void AddErrorToLog(ShoppingTrackContext _db, Exception  error);
    }
}
