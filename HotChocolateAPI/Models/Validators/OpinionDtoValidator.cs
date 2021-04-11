using FluentValidation;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class OpinionDtoValidator : AbstractValidator<OpininDto>
    {
        public OpinionDtoValidator(HotChocolateDbContext dbContext)
        {
            RuleFor(x => x.Stars)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (value>5 || value<1)
                        context.AddFailure("Opinion", "Value must be from 1 to 5");
                });
        }
    }
}
