using System;
using System.Collections.Generic;

namespace OOP_Protasiyk.lab5v20
{
    public class ShoppingCart
    {
        private Dictionary<string, decimal> _items = new Dictionary<string, decimal>();

        public decimal this[string productName]
        {
            get
            {
                if (_items.ContainsKey(productName))
                    return _items[productName];

                return 0;
            }
            set
            {
                _items[productName] = value;
            }
        }

        public void Add(string productName, decimal price)
        {
            _items[productName] = price;
        }

        public decimal Total()
        {
            decimal sum = 0;

            foreach (decimal price in _items.Values)
            {
                sum += price;
            }

            return sum;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public static ShoppingCart operator +(ShoppingCart a, ShoppingCart b)
        {
            ShoppingCart result = new ShoppingCart();

            foreach (var item in a._items)
            {
                result[item.Key] = item.Value;
            }

            foreach (var item in b._items)
            {
                result[item.Key] = item.Value;
            }

            return result;
        }

        public static bool operator >(ShoppingCart a, ShoppingCart b)
        {
            return a.Total() > b.Total();
        }

        public static bool operator <(ShoppingCart a, ShoppingCart b)
        {
            return a.Total() < b.Total();
        }

        public override string ToString()
        {
            string result = "Товари у кошику:\n";

            foreach (var item in _items)
            {
                result += $"{item.Key}: {item.Value} грн\n";
            }

            result += $"Загальна сума: {Total()} грн";

            return result;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ShoppingCart other)
                return false;

            if (_items.Count != other._items.Count)
                return false;

            foreach (var item in _items)
            {
                if (!other._items.ContainsKey(item.Key) ||
                    other._items[item.Key] != item.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Total().GetHashCode();
        }
    }

    class Program
    {
        static void Main()
        {
            ShoppingCart cart1 = new ShoppingCart();
            ShoppingCart cart2 = new ShoppingCart();

            cart1.Add("Клавіатура", 1200);
            cart1.Add("Мишка", 800);

            cart2.Add("Навушники", 1500);
            cart2.Add("Килимок", 500);

            Console.WriteLine("КОШИК №1");
            Console.WriteLine(cart1);

            Console.WriteLine("\nКОШИК №2");
            Console.WriteLine(cart2);

            Console.WriteLine("\n--- Індексатор ---");

            Console.WriteLine($"Ціна мишки: {cart1["Мишка"]} грн");

            cart1["Мишка"] = 900;

            Console.WriteLine($"Нова ціна мишки: {cart1["Мишка"]} грн");

            ShoppingCart cart3 = cart1 + cart2;

            Console.WriteLine("\n--- Оператор + ---");
            Console.WriteLine("Об'єднаний кошик:");
            Console.WriteLine(cart3);

            Console.WriteLine("\n--- Оператори > та < ---");

            if (cart1 > cart2)
                Console.WriteLine("Кошик №1 дорожчий за кошик №2.");

            if (cart1 < cart2)
                Console.WriteLine("Кошик №1 дешевший за кошик №2.");

            Console.WriteLine("\n--- Clear() ---");

            cart2.Clear();

            Console.WriteLine("Кошик №2 очищено.");
            Console.WriteLine($"Сума кошика №2: {cart2.Total()} грн");
        }
    }
}