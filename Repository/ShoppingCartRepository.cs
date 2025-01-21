using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class ShoppingCartRepository(
    ApplicationDbContext context)
    : IShoppingCartRepository
{
    public async Task<bool> UpdateCart(string userId, int productId, int updateBy)
    {
        if (string.IsNullOrEmpty(userId))
            return false;
        
        var cart = await context.ShoppingCarts
            .Where(c => c.ProductId == productId)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart is null)
        {
            cart = new ShoppingCart
            {
                UserId = userId,
                ProductId = productId,
                Count = updateBy
            };
            
            await context.ShoppingCarts.AddAsync(cart);
        }
        else
        {
            cart.Count += updateBy;
            
            if (cart.Count <= 0)
                context.ShoppingCarts.Remove(cart);
        }
        
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ShoppingCart>> GetAll(string? userId)
    {
        return await context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();
    }

    public async Task<bool> ClearCart(string? userId)
    {
        var cartItems = await context.ShoppingCarts
            .Where(c => c.UserId == userId)
            .ToListAsync();
        
        context.ShoppingCarts.RemoveRange(cartItems);
        
        return await context.SaveChangesAsync() > 0;
    }
}