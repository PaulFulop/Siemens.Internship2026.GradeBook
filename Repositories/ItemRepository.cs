using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Buffers.Text;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly HttpClient _httpClient;
    private const string baseUrl = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw";
    protected List<Item> _items = [];

    public ItemRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task EnsureItemsAreLoadedAsync()
    {
        if (_items.Count == 0)
        {
            var response = await _httpClient.GetFromJsonAsync<ItemWrapper>(baseUrl);
            _items = response?.Items ?? [];
        }
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        await EnsureItemsAreLoadedAsync();
        return _items.FirstOrDefault(i => i.Id == id && i.IsActive);
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        await EnsureItemsAreLoadedAsync();
        return _items.Where(i => i.IsActive);
    }
}
