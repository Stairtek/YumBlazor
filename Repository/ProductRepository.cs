using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class ProductRepository(
    ApplicationDbContext context,
    IWebHostEnvironment webHostEnvironment) 
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
        
        if (product.ImageUrl == null && productFromDb.ImageUrl != null)
            DeleteImage(productFromDb.ImageUrl);
        
        productFromDb.Name = product.Name;
        productFromDb.Price = product.Price;
        productFromDb.Description = product.Description;
        productFromDb.ImageUrl = product.ImageUrl;
        productFromDb.CategoryId = product.CategoryId;

        context.Products.Update(productFromDb);
        await context.SaveChangesAsync();
        
        return productFromDb;
    }
    
    private void DeleteImage(string imageUrl)
    {
        var imagePath = Path.Combine(webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
        
        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }

    public async Task<bool> Delete(int id)
    {
        var productFromDb = await context.Products
            .FirstOrDefaultAsync(c => c.Id == id);

        if (productFromDb == null) 
            return false;

        if (!string.IsNullOrEmpty(productFromDb.ImageUrl))
        {
            DeleteImage(productFromDb.ImageUrl);
            productFromDb.ImageUrl = null;
        }
        
        context.Products.Remove(productFromDb);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Product?> Get(int id)
    {
        var productFromDb = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return productFromDb ?? null;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await context.Products
            .Include(c => c.Category)
            .ToListAsync();
    }
}