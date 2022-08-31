using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class ProducerValidator : AbstractValidator<ProducerDTO>
    {
        public ProducerValidator()
        {
            RuleFor(c => c.Producer)
               .NotEmpty().WithMessage("Insira um nome para o Produtor.")
               .NotNull().WithMessage("Insira um nome para o Produtor.");

            RuleFor(c => c.Interval)
               .NotEmpty().WithMessage("Insira um intervalo entre os premios")
               .NotNull().WithMessage("Insira um intervalo entre os premios");

            RuleFor(c => c.PreviousWin)
               .NotEmpty().WithMessage("Insira o ano do primeiro premio")
               .NotNull().WithMessage("Insira o ano do primeiro premio");

            RuleFor(c => c.FollowingWin)
               .NotEmpty().WithMessage("Insira o ano do segundo premio")
               .NotNull().WithMessage("Insira o ano do segundo premio");
        }
    }
}
