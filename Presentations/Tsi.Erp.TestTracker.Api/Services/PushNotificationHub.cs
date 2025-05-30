using Microsoft.AspNetCore.SignalR;
using Tsi.Erp.TestTracker.Core;

namespace Tsi.Erp.TestTracker.Api.Services
{
    public class PushNotificationHub : Hub<IPushNotification>
    {
        public PushNotificationHub()
        {
                
        }
    }
}
