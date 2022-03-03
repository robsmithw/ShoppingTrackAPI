using System;
using System.Threading;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.HelperFunctions
{
    public interface IHelper
    {
        CancellationToken GetCancellationToken(int cancelAfterMs);
        void AddErrorToLog(ShoppingTrackContext context, Exception error);
    }
}
