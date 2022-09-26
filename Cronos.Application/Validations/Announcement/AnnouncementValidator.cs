using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Validations.Announcement
{
    public class AnnouncementValidator : AbstractValidator<AnnouncementEntity>
    {
        public AnnouncementValidator()
        {
            RuleFor(x => x.Header).NotNull();
            RuleFor(x => x.ShortDescription).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.ShortDescription).Length(0, 200).WithMessage("Short description must between 0-200 char."); ;
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date can not be earlier than start date"); ;
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("End date can not be earlier than start date"); ;
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0"); ;

        }
    }

    public class AnnouncementCreateValidator : AbstractValidator<CreateAnnouncementDto>
    {
        public AnnouncementCreateValidator()
        {
            RuleFor(x => x.Header).NotNull();
            RuleFor(x => x.ShortDescription).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
            RuleFor(x => x.ShortDescription).Length(0, 200).WithMessage("Short description must between 0-200 char.");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date can not be earlier than start date");
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("End date can not be earlier than start date"); ;
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0");

        }
    }
}
