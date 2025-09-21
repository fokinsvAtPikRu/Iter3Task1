using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task1
{
    // класс не содержит полей и свойств, поэтому все экземпляры этого класса будут одинаковыми
    // поэтому нет смысла делать несколько экземпляров этого класса
    // я бы сделал класс статическим
    // единственое что оправдывает делать класс динамическим - экономия ресурсов, если этот класс не будет
    // использоваться - он не будет создан, под него не выделятся ресурсы
    public class ShoppingCartService
    {
               
        public decimal CalculateTotalPrice(string customerType, List<decimal> itemPrices)
        {
            decimal baseTotal = 0;
            // лишний цикл, сумму покупок можно было посчитать в предыдущем цикле
            // лучше использовать не цикл а LINQ
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
        // KISS лучше чтобы метод принимал в качестве параметра скидку или метод для вычисления скидки, а не тип клиента 
        public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
        {
            // не понял почему покупки храним в словаре
            // навряд ли список покупок одного клиента настолько велик и нам нужен поиск по покупкам за 
            // линейное время, я бы использовал сразу List
            // тогда не потребовалорсь бы переводить словарь в список
            // и этот метод был бы не нужен
            List<decimal> allPrices = new List<decimal>();
            // в цикле словарь мапится в список без полезной фунциональности
            // в этом цикле сразу можно посчитать сумму покупок
            // лучше использовать LINQ
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
