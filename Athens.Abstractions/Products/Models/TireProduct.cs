using Athens.Abstractions.Products.Contracts;

namespace Athens.Abstractions.Products.Models;

public class TireProduct : IProduct
{
    public Guid Id { get; set; }

    public string Code { get; set; }
    public string Description { get; set; }
    public int Profile { get; set; }
    public int Section { get; set; }
    public int RimSize { get; set; }
    public char WetRating { get; set; }
    public char FuelRating { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}