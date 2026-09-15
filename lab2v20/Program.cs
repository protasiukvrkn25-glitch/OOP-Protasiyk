using System;

namespace Lab2
{
    public class Figure
    {
        private string _name;
        private string _color;
        private double _area;

        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public string Color
        {
            get => _color;
            set => _color = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public double Area
        {
            get => _area;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Площа не може бути від’ємною!");
                }
                _area = value;
            }
        }

        public Figure(string name, string color, double area)
        {
            Name = name;
            Color = color;
            Area = area;
            Console.WriteLine($"[Конструктор] Створено фігуру: {Name}");
        }

        public Figure() : this("Circle", "Red", 0.0)
        {
        }

        public string GetFigureInfo()
        {
            return $"Фігура: {Name}, Колір: {Color}, Площа: {Area} кв. од.";
        }

        ~Figure()
        {
            Console.WriteLine($"[Деструктор] Фігуру {Name} знищено з пам'яті.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Створення об'єктів ===");

            Figure fig1 = new Figure();
            Console.WriteLine(fig1.GetFigureInfo());

            Console.WriteLine();

            Figure fig2 = new Figure("Rectangle", "Blue", 45.5);
            Console.WriteLine(fig2.GetFigureInfo());

            Console.WriteLine();

            Figure fig3 = new Figure("Triangle", "Green", 12.0);
            Console.WriteLine(fig3.GetFigureInfo());

            Console.WriteLine("\n=== Кінець методу Main, підготовка до GC ===");

            fig1 = null;
            fig2 = null;
            fig3 = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("=== Завершення програми ===");
        }
    }
}