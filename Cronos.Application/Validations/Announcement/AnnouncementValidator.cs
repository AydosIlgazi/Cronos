using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Validations.Announcement
{
    public class AnnouncementValidator : AbstractValidator<AnnouncementUpdateViewModel>
    {
        public AnnouncementValidator()
        {
            RuleFor(x => x.Header).NotNull().WithMessage("This field is required");
            RuleFor(x => x.ShortDescription).NotNull().WithMessage("This field is required");
            RuleFor(x => x.Description).NotNull().WithMessage("This field is required");
            RuleFor(x => x.RedirectUrl).NotNull().WithMessage("This field is required");
            RuleFor(x => x.StartDate).NotNull().WithMessage("This field is required");
            RuleFor(x => x.EndDate).NotNull().WithMessage("This field is required");
            RuleFor(x => x.ShortDescription).Length(0, 255).WithMessage("Short description must between 0-255 char.");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date can not be earlier than start date");
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("End date can not be earlier than start date");
            RuleFor(x => x.Order).NotNull().WithMessage("This field is required");
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0");
            RuleFor(x => x.RedirectUrl).Must(LinkMustBeAUri).WithMessage("Redirect url must be valid Url");

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

    public class AnnouncementCreateValidator : AbstractValidator<CreateAnnouncementDto>
    {
        public AnnouncementCreateValidator()
        {
            RuleFor(x => x.Header).NotNull().WithMessage("This field is required");
            RuleFor(x => x.ShortDescription).NotNull().WithMessage("This field is required");
            RuleFor(x => x.Description).NotNull().WithMessage("This field is required");
            RuleFor(x => x.RedirectUrl).NotNull().WithMessage("This field is required");
            RuleFor(x => x.StartDate).NotNull().WithMessage("This field is required");
            RuleFor(x => x.EndDate).NotNull().WithMessage("This field is required");
            RuleFor(x => x.ShortDescription).Length(0, 255).WithMessage("Short description must between 0-255 char.");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date can not be earlier than start date");
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("End date can not be earlier than start date");
            RuleFor(x => x.Order).NotNull().WithMessage("This field is required");
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0");
            RuleFor(x => x.RedirectUrl).Must(LinkMustBeAUri).WithMessage("Redirect url must be valid Url");

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
