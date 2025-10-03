using Microsoft.Extensions.DependencyInjection;
namespace Iter3Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            bool isChecked = false;
            while (!isChecked)
            {
                Console.Clear();
                Console.Write("Выберите способ уведомления\n1 - email\n2 - SMS\nВаш выбор:");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            services.AddSingleton<INotificationSender, EmailSender>();
                            isChecked = true;
                            break;
                        case 2:
                            services.AddSingleton<INotificationSender, SmsSender>();
                            isChecked = true;
                            break;
                    }
                }
            }

            
            services.AddSingleton<NotificationService>();

            services.AddSingleton<ILoger, FileLoger>();
            services.AddSingleton<LoginService>();


            var provider = services.BuildServiceProvider();

            var service = provider.GetRequiredService<NotificationService>();
            var loger = provider.GetRequiredService<LoginService>();


            //var service = new NotificationService();
            //ILoger loger = new FileLoger("notification.log");
            service.SendNotification("Ваш заказ готов", "user@example.com");
            loger.Log("user@example.com");
        }
    }
}
