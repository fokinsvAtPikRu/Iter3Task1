namespace Iter3Task2
{
    public class SmsSender : INotificationSender
    {
        public void SendNotification(string message, string recipient)
        {
            // Симуляция отправки sms
            Console.WriteLine($"SMS для {recipient}: {message}");
        }
    }
}
