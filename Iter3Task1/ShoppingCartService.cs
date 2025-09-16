using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task1
{
    public class ShoppingCartService
    {
        // KISS лучше чтобы метод принимал в качестве параметра скидку или метод для вычисления скидки, а не тип клиента        
        public decimal CalculateTotalPrice(string customerType, List<decimal> itemPrices)
        {
            decimal baseTotal = 0;            
            for (int i = 0; i < itemPrices.Count; i++)
            {
                baseTotal += itemPrices[i];
            }

            decimal discount = 0;

            // KISS слишком большой if - функциональность вынести в отдельные методы или классы
            if (customerType == "Regular")
            {
                // DRY расчет скидки нужно вынести в отдельный метод
                discount = baseTotal * 0.05m; // 5%
            }
            // YAGNI излишняя функциональность
            else if (customerType == "Premium")
            {
                // DRY расчет скидки нужно вынести в отдельный метод
                discount = baseTotal * 0.15m; // 15%
                if (discount > 1000)
                {
                    discount = 1000 + (discount - 1000) * 0.1m;
                }
            }
            // YAGNI излишняя функциональность
            else if (customerType == "VIP")
            {
                // DRY расчет скидки нужно вынести в отдельный метод
                discount = baseTotal * 0.20m; // 20%
            }

            decimal finalPrice = baseTotal - discount;

            // KISS вывод вынести в отдельный метод SRP
            Console.WriteLine($"Base: {baseTotal}, Discount: {discount}, Final: {finalPrice}");
            return finalPrice;
        }

        public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
        {
            List<decimal> allPrices = new List<decimal>();
            foreach (var item in itemsWithQuantities)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    allPrices.Add(item.Key);
                }
            }
            return CalculateTotalPrice(customerType, allPrices);
        }
    }
}
