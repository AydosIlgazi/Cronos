using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Cronos.Application.Features.Banner.SaveBanners;
//using static Cronos.Application.Features.Banner.UpdateBanners;
namespace Cronos.Application.Data.Configurations
{
    //29.09.2022 İrem Kesemen
    public class UpdateBannerValidator : AbstractValidator<BannerUpdateViewModel>
    {
        public UpdateBannerValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Boş bırakılamaz!");

            RuleFor(b => b.StartDate).GreaterThan(DateTime.Today).WithMessage("Başlama tarihi kayıt tarihinden önce olamaz!").NotEmpty().WithMessage("Boş bırakılamaz!");


            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("Update Bitiş tarihi başlangıç tarihinden önce olamaz!").NotEmpty().WithMessage("Boş bırakılamaz!");


            RuleFor(x => x.AltText).NotEmpty().WithMessage("Boş bırakılamaz!");

            RuleFor(x => x.IsActive).NotNull().WithMessage("Boş bırakılamaz!");

            RuleFor(x => x.Order)

                .GreaterThanOrEqualTo(1)
                .WithMessage("Numaralandırma 1'den başlamalıdır.")
                .NotEmpty().WithMessage("Boş bırakılamaz!");



            RuleFor(x => x.RedirectUrl).Must(LinkMustBeAUri).WithMessage("Lütfen geçerli bir link girin!").NotEmpty().WithMessage("Boş bırakılamaz!");

        }


        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
