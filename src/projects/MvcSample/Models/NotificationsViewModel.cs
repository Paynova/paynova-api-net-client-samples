namespace MvcSample.Models
{
    public class NotificationsViewModel
    {
        public INotification[] Notifications { get; set; }

        public NotificationsViewModel()
        {
            Notifications = new INotification[0];
        }
    }
}