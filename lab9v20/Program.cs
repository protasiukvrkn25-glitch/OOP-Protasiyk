using System;
using System.Collections.Generic;

abstract class SearchEngine
{
    public string Name { get; set; }

    protected SearchEngine(string name)
    {
        Name = name;
    }

    public abstract List<string> Search(string query);
}

class LocalSearch : SearchEngine
{
    private List<string> files;

    public LocalSearch(string name, List<string> files) : base(name)
    {
        this.files = files;
    }

    public override List<string> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Запит не може бути порожнім.");

        List<string> results = new List<string>();

        foreach (string file in files)
        {
            if (file.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(file);
            }
        }

        return results;
    }
}

class DatabaseSearch : SearchEngine
{
    private List<string> records;

    public DatabaseSearch(string name, List<string> records) : base(name)
    {
        this.records = records;
    }

    public override List<string> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Запит до бази даних не може бути порожнім.");

        List<string> results = new List<string>();

        foreach (string record in records)
        {
            if (record.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(record);
            }
        }

        return results;
    }
}

class WebSearch : SearchEngine
{
    private List<string> pages;

    public WebSearch(string name, List<string> pages) : base(name)
    {
        this.pages = pages;
    }

    public override List<string> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Пошуковий запит не може бути порожнім.");

        List<string> results = new List<string>();

        foreach (string page in pages)
        {
            if (page.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(page);
            }
        }

        return results;
    }
}

class SearchService
{
    public void SearchAll(List<SearchEngine> engines, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Запит не може бути порожнім.");

        foreach (SearchEngine engine in engines)
        {
            try
            {
                Console.WriteLine($"\n--- {engine.Name} ---");

                List<string> results = engine.Search(query);

                if (results.Count == 0)
                {
                    Console.WriteLine("Результатів не знайдено.");
                }
                else
                {
                    foreach (string result in results)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        List<string> files = new List<string>
        {
            "document.txt",
            "photo.jpg",
            "program.cs",
            "report.docx",
            "music.mp3"
        };

        List<string> records = new List<string>
        {
            "Student: Vadym",
            "Course: C#",
            "Subject: OOP",
            "Laboratory work 9",
            "Group: KN 2/2"
        };

        List<string> pages = new List<string>
        {
            "C# programming tutorial",
            "Object-Oriented Programming",
            "C# documentation",
            "Unity game development",
            "GitHub programming projects"
        };

        List<SearchEngine> searchEngines = new List<SearchEngine>
        {
            new LocalSearch("Локальний пошук", files),
            new DatabaseSearch("Пошук у базі даних", records),
            new WebSearch("Веб-пошук", pages)
        };

        SearchService service = new SearchService();

        Console.WriteLine("=== Лабораторна робота №9 ===");
        Console.WriteLine("Варіант 20 — Пошук даних");

        Console.WriteLine("\nПошук за запитом: \"C#\"");
        service.SearchAll(searchEngines, "C#");

        Console.WriteLine("\nПошук за запитом: \"program\"");
        service.SearchAll(searchEngines, "program");

        Console.WriteLine("\n=== Демонстрація обробки помилки ===");

        try
        {
            service.SearchAll(searchEngines, "");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nПрограму завершено.");
    }
}