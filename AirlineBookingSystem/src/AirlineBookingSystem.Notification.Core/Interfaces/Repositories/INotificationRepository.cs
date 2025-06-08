using AirlineBookingSystem.Notifications.Core.Entities;

namespace AirlineBookingSystem.Notifications.Core.Interfaces.Repositories;

public interface INotificationRepository
{
    Task LogNotificationAsync(Notification notification);
}
