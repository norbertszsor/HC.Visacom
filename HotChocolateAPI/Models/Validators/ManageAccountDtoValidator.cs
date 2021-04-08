using FluentValidation;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class ManageAccountDtoValidator: AbstractValidator<ManageAccountDto>
    {
        public ManageAccountDtoValidator(HotChocolateDbContext dbContext)
        {
            RuleFor(x => x.IsActivated)
                .NotEmpty()
                .Custom((value, context) =>
                {
                if (value.GetType()!=typeof(bool))
                        
                        context.AddFailure("IsActivated","wrong value");      
                });
        }
    }
}
