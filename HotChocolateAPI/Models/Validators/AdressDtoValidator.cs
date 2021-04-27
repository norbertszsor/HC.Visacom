using FluentValidation;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class AdressDtoValidator : AbstractValidator<AddressDto>

    {
        public AdressDtoValidator()
        {
            RuleFor(x => x.Town)
                .MaximumLength(58);
            RuleFor(x => x.Street)
                .MaximumLength(58);
            RuleFor(x => x.HouseNumber)
                .MaximumLength(50);
            RuleFor(x => x.PostalCode)
                .MaximumLength(10);
        }
    }
}
