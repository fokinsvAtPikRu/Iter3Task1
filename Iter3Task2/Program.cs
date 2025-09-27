using Microsoft.Extensions.DependencyInjection;
namespace Iter3Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            
            services.AddSingleton<INotificationSender,EmailSender>();
            services.AddSingleton<NotificationService>();

            services.AddSingleton<ILoger, FileLoger>();
            services.AddSingleton<LoginService>();


            var provider=services.BuildServiceProvider();
            
            var service=provider.GetRequiredService<NotificationService>();
            var loger = provider.GetRequiredService<LoginService>();


            //var service = new NotificationService();
            //ILoger loger = new FileLoger("notification.log");
            service.SendNotification("Ваш заказ готов", "user@example.com");
            loger.Log("user@example.com");
        }
    }
}
