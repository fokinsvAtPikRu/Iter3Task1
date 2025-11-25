namespace Iter3Task2
{
    public class EmailSender : INotificationSender
    {        
        public void SendNotification(string message, string recipient)
        {
            // Симуляция отправки email
            Console.WriteLine($"Email для {recipient}: {message}");
        }
    }
}
