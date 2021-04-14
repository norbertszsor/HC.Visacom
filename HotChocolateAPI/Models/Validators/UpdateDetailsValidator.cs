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
                .NotEmpty()
                .MaximumLength(35);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Town)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.HouseNumber)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(8);

                
                
                
        }
    }
}
