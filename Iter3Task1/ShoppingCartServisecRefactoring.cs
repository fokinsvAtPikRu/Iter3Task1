namespace Iter3Task1
{
    public class ShoppingCartServisecRefactoring
    {
        public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
        {
            if (itemsWithQuantities == null)
                throw new NullReferenceException("Список покупок не создан");
            decimal totalPrice = itemsWithQuantities.Sum(s => s.Key * s.Value);
            decimal discount = Discount.CalculateDiscount(totalPrice, customerType);
            decimal finalPrice = totalPrice - discount;
            Logger.OrderPriceConsoleLogger(totalPrice, discount, finalPrice);
            return finalPrice;
        }
    }
}
