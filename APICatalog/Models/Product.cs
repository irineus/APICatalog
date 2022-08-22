using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

public class Product
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string? ImageURL { get; set; }
    
    public float Inventory { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}
