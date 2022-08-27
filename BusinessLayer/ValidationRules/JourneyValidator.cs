using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class JourneyValidator : AbstractValidator<Journey>
    {
        public JourneyValidator()
        {
            RuleFor(x => x.JourneyShortDescription).MinimumLength(20).WithMessage("Minimum 20 Karakter Girmelisiniz...!");
            RuleFor(x => x.JourneyName).NotNull().WithMessage("Bu alan Boş Geçilemez!");
        }
    }
}
