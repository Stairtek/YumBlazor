using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class ProductRepository(ApplicationDbContext context) 
    : IProductRepository
{
    public async Task<Product> Create(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> Update(Product product)
    {
        var productFromDb = await context.Products
            .FirstOrDefaultAsync(c => c.Id == product.Id);

        if (productFromDb is null) 
            return product;
        
        productFromDb.Name = product.Name;
        productFromDb.Price = product.Price;
        productFromDb.Description = product.Description;
        productFromDb.ImageUrl = product.ImageUrl;
        productFromDb.CategoryId = product.CategoryId;

        context.Products.Update(productFromDb);
        await context.SaveChangesAsync();
            
        return productFromDb;

    }

    public async Task<bool> Delete(int id)
    {
        var productFromDb = await context.Products
            .FirstOrDefaultAsync(c => c.Id == id);

        if (productFromDb == null) 
            return false;
        
        context.Products.Remove(productFromDb);
        return await context.SaveChangesAsync() > 0;

    }

    public async Task<Product?> Get(int id)
    {
        var productFromDb = await context.Products
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return productFromDb ?? null;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await context.Products.ToListAsync();
    }
}