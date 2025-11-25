namespace Iter3Task2
{
    public class FileLoger : ILoger
    {
        private const string FileName="notification.log";        
        public void Log(string recipient)
        {
            File.AppendAllText("log.txt", $"Отправлено уведомление для {recipient}");
        }
    }
}
