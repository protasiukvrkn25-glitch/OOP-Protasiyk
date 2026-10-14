
using System;

class Building
{
    public virtual void Describe()
    {
        Console.WriteLine("Це будівля.");
    }
}

class House : Building
{
    public override void Describe()
    {
        Console.WriteLine("Це житловий будинок.");
    }
}

class Office : Building
{
    public new void Describe()
    {
        Console.WriteLine("Це офісна будівля.");
    }
}

class Program
{
    static void Main()
    {
        Building building = new Building();

        Building house = new House();
        Building office = new Office();

        Console.WriteLine("--- Виклик через базовий тип ---");
        building.Describe();
        house.Describe();
        office.Describe();

        Console.WriteLine("\n--- Виклик через похідний тип ---");
        ((House)house).Describe();
        ((Office)office).Describe();

        Console.WriteLine("\n--- Різниця між override та new ---");
        Console.WriteLine("House використовує override: працює поліморфізм.");
        Console.WriteLine("Office використовує new: метод залежить від типу посилання.");
    }
}
