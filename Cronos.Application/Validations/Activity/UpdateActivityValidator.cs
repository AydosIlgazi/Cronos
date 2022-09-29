using Cronos.Application.Features.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Validations
{
    public class UpdateActivityValidator : AbstractValidator<UpdateActivityCommand>
    {
        public UpdateActivityValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Bu alan boş olamaz.").MaximumLength(50).WithMessage("Bu alan 50 karakterden fazla olamaz.");
            RuleFor(b => b.Info).NotEmpty().WithMessage("Bu alan boş olamaz.").Length(1, 250).WithMessage("Bu alan 250 karakterden fazla olamaz.");
            RuleFor(b => b.StartDate).NotEmpty().WithMessage("Bu alan boş olamaz.");
            RuleFor(b => b.EndDate).NotEmpty().WithMessage("Bu alan boş olamaz.");
            RuleFor(b => b.Order).NotEmpty().WithMessage("Bu alan boş olamaz.").GreaterThan(0).WithMessage("Sıra değeri 1den küçük olamaz."); ;
            RuleFor(b => b.locationUrl).Must(BeAValidUrl).WithMessage("Geçersiz URL");

            RuleFor(b => b.StartDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Başlangıç tarihi bugünün tarihinden önce olamaz");
            RuleFor(b => b.EndDate).GreaterThan(DateTime.Now).WithMessage("Bitiş tarihi bugünün tarihinden önce olamaz.")
                                   .GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden erken olamaz.");
        }


        private static bool BeAValidUrl(string arg)
        {
            Uri result;
            return Uri.TryCreate(arg, UriKind.Absolute, out result);
        }


    }
}
