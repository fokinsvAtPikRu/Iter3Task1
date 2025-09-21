using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task1
{
    public static class Discount
    {
        private const decimal RegularCustomerDiscountRate = 0.05m;
        public static decimal RegularCustomerDiscount(decimal totalPrice) => totalPrice * RegularCustomerDiscountRate;
        public static decimal CalculateDiscount(decimal totalPrice, string customerType)
        {
            decimal discount = 0;
            switch (customerType)
            {
                case "Regular":
                    return discount = RegularCustomerDiscount(totalPrice);
            }
            throw new ArgumentException("Неверный тип покупателя");
        }
    }
}
