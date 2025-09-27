using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task2
{
    public class LoginService
    {
        private ILoger _loger;
        public LoginService(ILoger loger)
        {
            _loger = loger;
        }
        public void Log(string recipient)
        {
            _loger.Log(recipient);
        }
    }
}
