using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogShortDescription).
                MinimumLength(20).
                WithMessage("Minimum 20 Karakter Girininiz...!");
            RuleFor(x => x.BlogShortDescription).MaximumLength(400).
                WithMessage("Maximum 200 Karakter Girininiz...!");

            RuleFor(x => x.BlogDescription).
                MinimumLength(20).
                WithMessage("Minimum 20 Karakter Girininiz...!");
            RuleFor(x => x.BlogDescription).
                MaximumLength(30000).
                WithMessage("Maximum 30000 Karakter Girininiz...!");
        }
    }
}
