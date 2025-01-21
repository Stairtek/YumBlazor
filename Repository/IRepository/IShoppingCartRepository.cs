using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository;

public interface IShoppingCartRepository
{
    public Task<bool> UpdateCart(string userId, int product, int updateBy);

    public Task<IEnumerable<ShoppingCart>> GetAll(string? userId);
    
    public Task<bool> ClearCart(string? userId);
}