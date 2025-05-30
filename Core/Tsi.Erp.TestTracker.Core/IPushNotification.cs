
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Core
{
    public interface IPushNotification
    {
        public Task PushAsync(Notification notification);
    }
}