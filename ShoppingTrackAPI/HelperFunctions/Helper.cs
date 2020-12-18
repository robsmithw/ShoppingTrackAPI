using ShoppingTrackAPI.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingTrackAPI.HelperFunctions
{
    public class Helper : IHelper
    {
        public bool SignIn(User userInDatabase, User userSupplied)
        {
            bool success = false;
            using (SHA512 shaM = new SHA512Managed())
            {
                var passwordInBytes = Encoding.UTF8.GetBytes(userSupplied.Password);
                var hash = shaM.ComputeHash(passwordInBytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hash)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }
                var hashPass = hashedInputStringBuilder.ToString();
                if (userInDatabase.Password == hashPass && userSupplied.Username == userInDatabase.Username)
                {
                    return true;
                }
            }
            return success;
        }

        public string CalculateHash(string toHash)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var passwordInBytes = Encoding.UTF8.GetBytes(toHash);
                var hash = shaM.ComputeHash(passwordInBytes);
                if(hash != null)
                {
                    var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                    foreach (var b in hash)
                    {
                        hashedInputStringBuilder.Append(b.ToString("X2"));
                    }
                    return hashedInputStringBuilder.ToString();
                }
            }
            throw new Exception("Hash could not be generated.");
        }

        public async void AddErrorToLog(ShoppingTrackContext _db, Exception error)
        {
            ErrorLog errorLog = new ErrorLog()
            {
                Location = nameof(AddErrorToLog),
                CallStack = error.StackTrace
            };
            _db.ErrorLog.Add(errorLog);
            await _db.SaveChangesAsync();
        }
    }
}
