
namespace Cronos.Application.Entities
{
    public class BannerEntity : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public string RedirectUrl { get; set; }
    }
}
