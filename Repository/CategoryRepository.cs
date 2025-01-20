using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class CategoryRepository(ApplicationDbContext context) 
    : ICategoryRepository
{
    public async Task<Category> Create(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> Update(Category category)
    {
        var categoryFromDb = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == category.Id);

        if (categoryFromDb is null) 
            return category;
        
        categoryFromDb.Name = category.Name;

        context.Categories.Update(categoryFromDb);
        await context.SaveChangesAsync();
            
        return categoryFromDb;

    }

    public async Task<bool> Delete(int id)
    {
        var categoryFromDb = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoryFromDb == null) 
            return false;
        
        context.Categories.Remove(categoryFromDb);
        return await context.SaveChangesAsync() > 0;

    }

    public async Task<Category?> Get(int id)
    {
        var categoryFromDb = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return categoryFromDb ?? null;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await context.Categories.ToListAsync();
    }
}