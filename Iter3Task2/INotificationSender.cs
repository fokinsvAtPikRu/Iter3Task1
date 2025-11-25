using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task2
{
    public interface INotificationSender
    {
        public void SendNotification(string message, string recipient);
    }
}
