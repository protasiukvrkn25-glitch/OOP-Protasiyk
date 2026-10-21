
using lab12v20;
using Xunit;

namespace lab12v20.Tests;

public class InMemoryRepositoryTests
{
    private readonly InMemoryRepository<Event> _repository = new();

    private Event CreateEvent(int id = 1, string name = "Конференція")
    {
        return new Event
        {
            Id = id,
            Name = name,
            Location = "Рівне",
            EventDate = new DateTime(2026, 11, 10)
        };
    }

    [Fact]
    public void Add_ShouldAddItem()
    {
        _repository.Add(CreateEvent());

        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void GetById_ShouldReturnItem()
    {
        var item = CreateEvent();
        _repository.Add(item);

        Assert.Equal(item, _repository.GetById(1));
    }

    [Fact]
    public void GetById_ShouldThrowWhenItemDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(
            () => _repository.GetById(99));
    }

    [Fact]
    public void GetAll_ShouldReturnAllItems()
    {
        _repository.Add(CreateEvent(1));
        _repository.Add(CreateEvent(2, "Виставка"));

        Assert.Equal(2, _repository.GetAll().Count());
    }

    [Fact]
    public void Update_ShouldUpdateExistingItem()
    {
        _repository.Add(CreateEvent());

        var updated = CreateEvent(1, "IT-конференція");
        _repository.Update(updated);

        Assert.Equal("IT-конференція",
            _repository.GetById(1).Name);
    }

    [Fact]
    public void Update_ShouldThrowWhenItemDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(
            () => _repository.Update(CreateEvent(99)));
    }

    [Fact]
    public void Delete_ShouldRemoveItem()
    {
        _repository.Add(CreateEvent());

        _repository.Delete(1);

        Assert.Empty(_repository.GetAll());
    }

    [Fact]
    public void Delete_ShouldThrowWhenItemDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(
            () => _repository.Delete(99));
    }

    [Fact]
    public void Add_ShouldThrowWhenIdAlreadyExists()
    {
        _repository.Add(CreateEvent());

        Assert.Throws<InvalidOperationException>(
            () => _repository.Add(CreateEvent()));
    }
}
