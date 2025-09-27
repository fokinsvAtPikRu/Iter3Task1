namespace Iter3Task2
{
    public class NotificationService
    {
        private readonly INotificationSender _notificationSender;

        public NotificationService(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender; // Нарушение: жесткая привязка
        }

        public void SendNotification(string message, string recipient)
        {
            // Логика подготовки уведомления
            string formattedMessage = $"Уведомление: {message}";

            // Отправка уведомления
            _notificationSender.SendNotification(recipient, formattedMessage);
            
        }
    }   
}
