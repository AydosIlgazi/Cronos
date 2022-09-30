using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Dtos.Activity
{
    public class ActivityDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string? locationUrl { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Order { get; set; }

    }
}
