namespace Iter3Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<decimal,int> order = new Dictionary<decimal,int>();
            order.Add(1, 2); // 2
            order.Add(3, 4); // 12
            order.Add(5, 6); // 30 total = 44
            var shoppingCenter = new ShoppingCartService();
            var shoppingCenterRefactoring= new ShoppingCartServisecRefactoring();
            shoppingCenter.CalculateTotalPriceWithQuantities("Regular", order);
            shoppingCenterRefactoring.CalculateTotalPriceWithQuantities("Regular", order);
        }
    }
}
