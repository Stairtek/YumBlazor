using System.ComponentModel.DataAnnotations.Schema;

namespace YumBlazor.Data;

public class ShoppingCart
{
    public int Id { get; set; }
    
    public required string UserId { get; set; }
    
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }
    
    public int ProductId { get; set; }
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    
    public int Count { get; set; }
    
    public decimal ItemTotal => Product.Price * Count;
}