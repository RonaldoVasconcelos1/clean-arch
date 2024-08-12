using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationDbContext _productContext;
    public ProductRepository(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }

    public async Task<Product> AddAsync(Product product)
    {
        _productContext.Products.Add(product);
        await _productContext.SaveChangesAsync();
        return product;
    }
    
    public async Task<Product> GetByIdAsync(int? id)
    {            
        return await _productContext.Products
            .Include(c => c.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productContext.Products.ToListAsync();
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        _productContext.Products.Remove(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _productContext.Update(product);
        await _productContext.SaveChangesAsync();
        return product;
    }
}