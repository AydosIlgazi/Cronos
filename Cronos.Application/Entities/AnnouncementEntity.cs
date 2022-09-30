using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Entities
{
    public class AnnouncementEntity : BaseEntity
    {
        public string Header { get; set; }
        public string RedirectUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Description   { get; set; }
    }
}
