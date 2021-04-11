using FluentValidation;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator(HotChocolateDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Amount)
                .GreaterThan(0);
            RuleFor(x => x.Price)
                .GreaterThan(0);
                
                
        }
    }
}
