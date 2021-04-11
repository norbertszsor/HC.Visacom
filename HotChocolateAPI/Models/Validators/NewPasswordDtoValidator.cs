using FluentValidation;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class NewPasswordDtoValidator : AbstractValidator<NewPasswordDto>
    {
        public NewPasswordDtoValidator(HotChocolateDbContext dbContext)
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(8);
            RuleFor(x => x.ConfirmNewPassword)
                .Equal(e => e.NewPassword);
            
        }
    }
}