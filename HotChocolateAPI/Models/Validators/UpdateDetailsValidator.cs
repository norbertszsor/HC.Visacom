using FluentValidation;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class UpdateDetailsValidator : AbstractValidator<UpdateDetailsDto>
    {
        public UpdateDetailsValidator(HotChocolateDbContext context)
        {

            RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .MaximumLength(57); // według wujka google
            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .MaximumLength(81); // według wujka google
            RuleFor(x => x.PhoneNumber)
                .Length(9); //BO JESTESMY W POLSCE 


        }
    }
}
