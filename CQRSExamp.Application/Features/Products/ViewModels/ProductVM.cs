namespace CQRSExamp.Application.Features.Products.ViewModels;

public class ProductVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public double Price { get; set; }
}
