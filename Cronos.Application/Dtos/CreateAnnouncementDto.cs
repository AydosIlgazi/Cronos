using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Dtos
{
    public class CreateAnnouncementDto
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Header { get; set; }
        public string RedirectUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
