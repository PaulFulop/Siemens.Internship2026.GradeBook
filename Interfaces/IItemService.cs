using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;
public interface IItemService
{
    Task<List<Item>> GetAllItemsAsync();
    Task<Statistic> GetStatisticAsync();
    Task<Item?> GetByIdAsync(int id);
    Task<List<Item>> GetTopPassiveGradesAsync(int N);
}