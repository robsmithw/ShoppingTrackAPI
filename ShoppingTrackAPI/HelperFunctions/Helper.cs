using ShoppingTrackAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingTrackAPI.HelperFunctions
{
    public class Helper
    {
        public static bool SignIn(User userInDatabase, User userSupplied)
        {
            bool success = false;
            using (SHA512 shaM = new SHA512Managed())
            {
                var passwordInBytes = Encoding.UTF8.GetBytes(userSupplied.Password);
                var hash = shaM.ComputeHash(passwordInBytes);
                var hashPass = Encoding.Default.GetString(hash);
                if (userInDatabase.Password == hashPass && userSupplied.Username == userInDatabase.Username)
                {
                    return true;
                }
            }
            return success;
        }
    }
}
