
using System.ComponentModel.DataAnnotations;

namespace Cronos.Application.Entities
{
    public class BannerEntity : BaseEntity
    {
        //25.09.2022 Irem Kesemen
        //null validasyonunun default olmaması için nullable yapılarak
        //oluşturulan kuralların uygulanması sağlandı


        public string? ImageUrl { get; set; }


        public string? AltText { get; set; }


        public string? RedirectUrl { get; set; }
    }
}
