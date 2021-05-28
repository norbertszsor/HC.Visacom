using FluentValidation;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Validators
{
    public class ProductQueryValidator : AbstractValidator<ProductQuery>
    {
        private string[] allowedColumnNames = { nameof(Product.Name).ToLower(), nameof(Product.Price).ToLower(), nameof(Product.Amount).ToLower() };
        public ProductQueryValidator()
        {
            RuleFor(x => x.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedColumnNames.Contains(value))
                .WithMessage($"Sortowanie jest opcjonalne lub może sortować kolumny[{string.Join(",", allowedColumnNames)}]");
        }
    }
}
