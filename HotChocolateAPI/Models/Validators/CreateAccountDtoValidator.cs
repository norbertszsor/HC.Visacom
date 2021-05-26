using FluentValidation;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator(HotChocolateDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(35);
            RuleFor(x => x.FirstName)
                .MinimumLength(1)
                .MaximumLength(35);
            RuleFor(x => x.PhoneNumber)
                .Length(9);
            RuleFor(x => x.LastName)
                .MinimumLength(1)
                .MaximumLength(35);
            RuleFor(x => x.Password)
                .MinimumLength(8);
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
            RuleFor(x => x.RoleId)
                .NotEmpty()
                 .Custom((value, context) =>
                 {
                     if (!(value > 0 && value < 5))
                         context.AddFailure("RoleId", "RoleId musi być między 1, a 4.");
                 });
        }
    }
}
