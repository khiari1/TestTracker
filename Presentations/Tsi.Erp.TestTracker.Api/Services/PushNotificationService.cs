using Microsoft.AspNetCore.SignalR;
using Tsi.Erp.TestTracker.Core;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Services
{
    public class PushNotificationService : IPushNotification
    {
        private readonly IHubContext<PushNotificationHub, IPushNotification> _hubContext;
        private readonly IGenericRepository<Notification> _notificationRepository;
        public PushNotificationService(IHubContext<PushNotificationHub, IPushNotification> hubContext,
                                       IGenericRepository<Notification> notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }
        public async Task PushAsync(Notification notification)
        {
            _notificationRepository.Create(notification);
            await _notificationRepository.SaveAsync();
            await _hubContext.Clients.All.PushAsync(notification);
        }

        public async Task DeleteMyNotifications()
        {
            await _notificationRepository.SaveAsync();
        }
    }
}
