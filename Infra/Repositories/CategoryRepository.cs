using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly ApplicationDbContext _categoryContext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _categoryContext = context;
    }

    public async Task<Category> AddAsync(Category category)
    {
        _categoryContext.Categories.Add(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> GetByIdAsync(int? id)
    {
        return await _categoryContext.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryContext.Categories.ToListAsync();
    }

    public async Task<Category> RemoveAsync(Category category)
    {
        _categoryContext.Categories.Remove(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _categoryContext.Entry(category).State = EntityState.Modified;
        await _categoryContext.SaveChangesAsync();
        return category;
    }
}