
using System;
using System.Collections.Generic;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Score { get; set; }

    public Person(string name, int age, double score)
    {
        Name = name;
        Age = age;
        Score = score;
    }

    public override string ToString()
    {
        return $"{Name}, вік: {Age}, бал: {Score}";
    }
}

static class FunctionalUtils
{
    public static List<TResult> Map<T, TResult>(
        List<T> list, Func<T, TResult> mapper)
    {
        List<TResult> result = new List<TResult>();

        foreach (T item in list)
            result.Add(mapper(item));

        return result;
    }

    public static List<T> Filter<T>(
        List<T> list, Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (T item in list)
            if (predicate(item))
                result.Add(item);

        return result;
    }

    public static T MaxBy<T, TKey>(
        List<T> list, Func<T, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        if (list.Count == 0)
            throw new ArgumentException("Список порожній.");

        T maxItem = list[0];

        foreach (T item in list)
            if (keySelector(item).CompareTo(keySelector(maxItem)) > 0)
                maxItem = item;

        return maxItem;
    }

    public static T MinBy<T, TKey>(
        List<T> list, Func<T, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        if (list.Count == 0)
            throw new ArgumentException("Список порожній.");

        T minItem = list[0];

        foreach (T item in list)
            if (keySelector(item).CompareTo(keySelector(minItem)) < 0)
                minItem = item;

        return minItem;
    }

    public static List<T> OrderBy<T, TKey>(
        List<T> list, Func<T, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        List<T> result = new List<T>(list);

        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = 0; j < result.Count - i - 1; j++)
            {
                if (keySelector(result[j]).CompareTo(
                    keySelector(result[j + 1])) > 0)
                {
                    T temp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = temp;
                }
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Лабораторна робота №13 ===");
        Console.WriteLine("Варіант 20: Generic-методи\n");

        List<int> numbers = new List<int> { 8, 3, 12, 5, 1, 10 };

        Console.WriteLine("1. Map для int:");
        var squares = FunctionalUtils.Map(numbers, x => x * x);
        Console.WriteLine(string.Join(", ", squares));

        Console.WriteLine("\n2. Filter для int:");
        var even = FunctionalUtils.Filter(numbers, x => x % 2 == 0);
        Console.WriteLine(string.Join(", ", even));

        Console.WriteLine("\n3. MaxBy для int:");
        Console.WriteLine(FunctionalUtils.MaxBy(numbers, x => x));

        Console.WriteLine("\n4. MinBy для int:");
        Console.WriteLine(FunctionalUtils.MinBy(numbers, x => x));

        Console.WriteLine("\n5. OrderBy для int:");
        Console.WriteLine(string.Join(", ",
            FunctionalUtils.OrderBy(numbers, x => x)));

        List<string> words = new List<string>
        {
            "яблуко", "кіт", "програмування", "дім"
        };

        Console.WriteLine("\n=== Перевірка зі string ===");
        Console.WriteLine(string.Join(", ",
            FunctionalUtils.Map(words, word => word.Length)));

        Console.WriteLine(string.Join(", ",
            FunctionalUtils.Filter(words, word => word.Length > 3)));

        Console.WriteLine("Найдовше слово: " +
            FunctionalUtils.MaxBy(words, word => word.Length));

        Console.WriteLine("Найкоротше слово: " +
            FunctionalUtils.MinBy(words, word => word.Length));

        Console.WriteLine(string.Join(", ",
            FunctionalUtils.OrderBy(words, word => word.Length)));

        List<Person> people = new List<Person>
        {
            new Person("Олег", 19, 85.5),
            new Person("Ірина", 18, 96.0),
            new Person("Андрій", 20, 72.5),
            new Person("Марія", 19, 91.0)
        };

        Console.WriteLine("\n=== Перевірка з класом Person ===");

        Console.WriteLine("Імена:");
        Console.WriteLine(string.Join(", ",
            FunctionalUtils.Map(people, person => person.Name)));

        Console.WriteLine("\nСтуденти з балом від 90:");
        foreach (Person person in
            FunctionalUtils.Filter(people, person => person.Score >= 90))
            Console.WriteLine(person);

        Console.WriteLine("\nНайвищий бал:");
        Console.WriteLine(FunctionalUtils.MaxBy(people, person => person.Score));

        Console.WriteLine("\nНайнижчий бал:");
        Console.WriteLine(FunctionalUtils.MinBy(people, person => person.Score));

        Console.WriteLine("\nСортування за віком:");
        foreach (Person person in
            FunctionalUtils.OrderBy(people, person => person.Age))
            Console.WriteLine(person);
    }
}
