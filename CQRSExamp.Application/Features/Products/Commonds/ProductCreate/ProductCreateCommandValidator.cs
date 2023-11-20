using FluentValidation;

namespace CQRSExamp.Application.Features.Products.Commonds.ProductCreate;

public class ProductCreateCommandValidator: AbstractValidator<ProductCreateCommand>
{
    public ProductCreateCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(64).WithMessage("Name must not exceed 64 characters");

        RuleFor(r => r.Price)
                .GreaterThan(0);
    }
}
