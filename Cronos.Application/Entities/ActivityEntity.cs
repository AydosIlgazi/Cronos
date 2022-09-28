using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Entities
{
    public class ActivityEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public string locationUrl { get; set; } 

    }
}
