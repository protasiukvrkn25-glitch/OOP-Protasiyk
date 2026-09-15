using System;

public class Figure
{
    private string name;
    private double area;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public double Area
    {
        get { return area; }
        set
        {
            if (value >= 0)
            {
                area = value;
            }
            else
            {
                area = 0;
            }
        }
    }

    public Figure(string name, double area)
    {
        this.name = name;
        Area = area;
    }

    ~Figure()
    {
        Console.WriteLine($"Деструктор: об'єкт '{name}' видаляється з пам'яті.");
    }

    public string GetFigure()
    {
        return $"Назва фігури: {name}, Площа: {area}";
    }
}

class Program
{
    static void Main()
    {
        Figure fig1 = new Figure("Кло", 50.27);
        Figure fig2 = new Figure("Прямокутник", 20.0);
        Figure fig3 = new Figure("Трикутник", 15.5);

        Console.WriteLine(fig1.GetFigure());
        Console.WriteLine(fig2.GetFigure());
        Console.WriteLine(fig3.GetFigure());
    }
}