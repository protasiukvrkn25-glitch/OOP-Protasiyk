
using lab12v20;

IRepository<Event> repository = new InMemoryRepository<Event>();

repository.Add(new Event
{
    Id = 1,
    Name = "Конференція",
    Location = "Рівне",
    EventDate = new DateTime(2026, 11, 10)
});

repository.Add(new Event
{
    Id = 2,
    Name = "Виставка",
    Location = "Львів",
    EventDate = new DateTime(2026, 12, 5)
});

Console.WriteLine("Усі події:");

foreach (var item in repository.GetAll())
{
    Console.WriteLine($"{item.Id}. {item.Name} — {item.Location}");
}

var updatedEvent = repository.GetById(1);
updatedEvent.Name = "IT-конференція";
repository.Update(updatedEvent);

Console.WriteLine($"\nОновлена подія: {repository.GetById(1).Name}");

repository.Delete(2);

Console.WriteLine($"Кількість подій після видалення: {repository.GetAll().Count()}");
