


namespace Cronos.Application.Dtos
{
    public class BannerDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public string RedirectUrl { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
