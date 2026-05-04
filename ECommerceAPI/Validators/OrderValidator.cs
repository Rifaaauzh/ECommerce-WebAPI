using ECommerceAPI.DTOs;
using FluentValidation;

namespace ECommerceAPI.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Order must have at least one item");

            RuleForEach(x => x.OrderDetails).ChildRules(detail =>
            {
                detail.RuleFor(x => x.ProductId)
                    .GreaterThan(0).WithMessage("ProductId must be greater than 0");

                detail.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be at least 1");
            });
        }
    }
}