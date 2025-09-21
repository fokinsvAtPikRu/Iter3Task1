using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task1
{
    public static class Logger
    {
        public static void OrderPriceConsoleLogger(decimal totalPrice, decimal discount, decimal finalPrice)
        {
            Console.WriteLine($"Base: {totalPrice}, Discount: {discount}, Final: {finalPrice}");
        }        
    }
}
