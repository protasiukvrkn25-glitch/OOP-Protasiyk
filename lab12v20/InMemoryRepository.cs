
namespace lab12v20;

public class InMemoryRepository<T> : IRepository<T>
    where T : IEntity
{
    private readonly List<T> _items = new();

    public T GetById(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item == null)
            throw new KeyNotFoundException($"Елемент з ID {id} не знайдено.");

        return item;
    }

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public void Add(T item)
    {
        if (_items.Any(x => x.Id == item.Id))
            throw new InvalidOperationException(
                $"Елемент з ID {item.Id} вже існує.");

        _items.Add(item);
    }

    public void Update(T item)
    {
        var index = _items.FindIndex(x => x.Id == item.Id);

        if (index == -1)
            throw new KeyNotFoundException(
                $"Елемент з ID {item.Id} не знайдено.");

        _items[index] = item;
    }

    public void Delete(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item == null)
            throw new KeyNotFoundException(
                $"Елемент з ID {id} не знайдено.");

        _items.Remove(item);
    }
}
