using FluentValidation;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class OrderStatusDtoValidator: AbstractValidator<OrderStatusDto>
    {
        public OrderStatusDtoValidator()
        {
            RuleFor(x=>x.StatusId)
                .NotEmpty()
                 .Custom((value, context) =>
                  {
                      if (value > 5 || value < 1)
                          context.AddFailure("StatusId", "Value must be from 1 to 5");
                  });
        }
    }
}
