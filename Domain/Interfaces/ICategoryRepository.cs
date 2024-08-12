using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    //Task<> - Defines as Asynchronous
    //IEnumerable<> - Defines as List of one or more records
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetByIdAsync(int? id);
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<Category> RemoveAsync(Category category);
}