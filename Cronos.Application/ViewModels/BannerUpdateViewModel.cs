using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.ViewModels
{
    public class BannerUpdateViewModel
    {
        //28.09.2022 Irem Kesemen
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }
        public string? RedirectUrl { get; set; }

        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
