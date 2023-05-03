namespace Athens.Abstractions.Products.Contracts;

public interface IProduct
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}