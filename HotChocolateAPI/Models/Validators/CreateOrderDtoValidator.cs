using FluentValidation;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<(CreateOrderDto, Order)>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.Item2.AddressId)
                .NotEmpty();
            RuleFor(x => x.Item2.User.Id).NotEmpty();
            RuleFor(x => x.Item1.ProductId).NotEmpty();
            
        }
    }
}
