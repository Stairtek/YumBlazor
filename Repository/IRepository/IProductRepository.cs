using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository;

public interface IProductRepository
{
    public Task<Product> Create(Product product);
    
    public Task<Product?> Update(Product product);
    
    public Task<bool> Delete(int id);
    
    public Task<Product?> Get(int id);
    
    public Task<IEnumerable<Product>> GetAll();
}