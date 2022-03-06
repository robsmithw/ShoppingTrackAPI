using ShoppingTrackAPI.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ShoppingTrackAPI.HelperFunctions
{
    public class Helper : IHelper
    {
        public async void AddErrorToLog(ShoppingTrackContext context, Exception error)
        {
            ErrorLog errorLog = new ErrorLog()
            {
                Location = nameof(AddErrorToLog),
                CallStack = error.StackTrace
            };
            context.ErrorLog.Add(errorLog);
            await context.SaveChangesAsync();
        }

        public CancellationToken GetCancellationToken(int cancelAfterMs)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(cancelAfterMs);
            return cancellationTokenSource.Token;
        }
    }
}
