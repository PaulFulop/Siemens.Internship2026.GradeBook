using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Reflection.PortableExecutable;

namespace Siemens.Internship2026.GradeBook.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        this._itemRepository = itemRepository;
    }

    private async Task<List<Item>> GetItemListAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return items.ToList();
    }

    public async Task<List<Item>> GetAllItemsAsync() => await GetItemListAsync();

    public async Task<Statistic> GetStatisticAsync()
    {
        var itemList = await GetItemListAsync();
        var totalCount = itemList.Count;
        var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;
        Console.WriteLine($"[LOG] Returning {totalCount} items, average value: {averageValue}");

        return new Statistic
        {
            TotalCount = totalCount,
            AverageValue = averageValue,
            RetrievedAt = DateTime.UtcNow
        };
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _itemRepository.GetByIdAsync(id);
    }

    public async Task<List<Item>> GetTopPassiveGradesAsync(int N)
    {
        if (N < 0)
            throw new ArgumentException("N should be a positive integer.");

        return (await GetItemListAsync())
            .Where(i => i.IsActive && i.Value >= 5)
            .Take(N)
            .ToList();
    }
}
