
using System;
using System.Collections.Generic;

class RecommendationSystem
{
    private HashSet<string> products = new HashSet<string>();
    private Queue<string> users = new Queue<string>();
    private Stack<string> history = new Stack<string>();

    public void AddProduct(string product)
    {
        if (products.Add(product))
            Console.WriteLine($"Додано продукт: {product}");
        else
            Console.WriteLine($"Продукт {product} вже існує.");
    }

    public void RemoveProduct(string product)
    {
        if (products.Remove(product))
            Console.WriteLine($"Видалено продукт: {product}");
        else
            Console.WriteLine($"Продукт {product} не знайдено.");
    }

    public void AddUser(string user)
    {
        users.Enqueue(user);
        Console.WriteLine($"Користувача додано в чергу: {user}");
    }

    public void ProcessUser()
    {
        if (users.Count > 0)
            Console.WriteLine($"Обробляємо користувача: {users.Dequeue()}");
        else
            Console.WriteLine("Черга користувачів порожня.");
    }

    public void AddHistory(string recommendation)
    {
        history.Push(recommendation);
        Console.WriteLine($"Переглянуто рекомендацію: {recommendation}");
    }

    public void UndoHistory()
    {
        if (history.Count > 0)
            Console.WriteLine($"Видалено з історії: {history.Pop()}");
        else
            Console.WriteLine("Історія порожня.");
    }

    public void ShowProducts()
    {
        Console.WriteLine("Унікальні продукти: " +
            string.Join(", ", products));
    }

    public void ShowQueue()
    {
        Console.WriteLine("Наступний користувач: " +
            (users.Count > 0 ? users.Peek() : "черга порожня"));
    }

    public void ShowHistory()
    {
        Console.WriteLine("Остання рекомендація: " +
            (history.Count > 0 ? history.Peek() : "історія порожня"));
    }

    public static void Main()
    {
        RecommendationSystem system = new RecommendationSystem();

        Console.WriteLine("=== СИСТЕМА РЕКОМЕНДАЦІЙ ===");

        Console.WriteLine("\n--- HashSet: продукти ---");
        system.AddProduct("The Witcher 3");
        system.AddProduct("Cyberpunk 2077");
        system.AddProduct("The Witcher 3");
        system.ShowProducts();

        Console.WriteLine("Перевірка наявності Cyberpunk 2077: " +
            system.products.Contains("Cyberpunk 2077"));

        system.RemoveProduct("Cyberpunk 2077");
        system.ShowProducts();

        Console.WriteLine("\n--- Queue: користувачі ---");
        system.AddUser("Вадим");
        system.AddUser("Олексій");
        system.AddUser("Андрій");

        system.ShowQueue();
        system.ProcessUser();
        system.ProcessUser();
        system.ShowQueue();
        system.ProcessUser();

        Console.WriteLine("\n--- Stack: історія рекомендацій ---");
        system.AddHistory("The Witcher 3");
        system.AddHistory("Minecraft");
        system.AddHistory("Counter-Strike 2");

        system.ShowHistory();
        system.UndoHistory();
        system.ShowHistory();
        system.UndoHistory();

        Console.WriteLine("\n=== ПРОГРАМУ ЗАВЕРШЕНО ===");
    }
}
