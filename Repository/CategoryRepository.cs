using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class CategoryRepository(ApplicationDbContext context) 
    : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    
    public Category Create(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();

        return category;
    }

    public Category Update(Category category)
    {
        var categoryFromDb = _context.Categories
            .FirstOrDefault(c => c.Id == category.Id);

        if (categoryFromDb is null) 
            return category;
        
        categoryFromDb.Name = category.Name;

        _context.Categories.Update(categoryFromDb);
        _context.SaveChanges();
            
        return categoryFromDb;

    }

    public bool Delete(int id)
    {
        var categoryFromDb = _context.Categories
            .FirstOrDefault(c => c.Id == id);

        if (categoryFromDb == null) 
            return false;
        
        _context.Categories.Remove(categoryFromDb);
        return _context.SaveChanges() > 0;

    }

    public Category? Get(int id)
    {
        var categoryFromDb = _context.Categories
            .FirstOrDefault(c => c.Id == id);
        
        return categoryFromDb ?? null;
    }

    public IEnumerable<Category> GetAll()
    {
        return _context.Categories.ToList();
    }
}