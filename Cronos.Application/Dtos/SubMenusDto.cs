using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Dtos
{
    public class SubMenusDto
    {

        public int Id { get; set; }
        public int ParentId { get; set; }

  
        public string? SubMenuName { get; set; }

      
        public string? Content { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public int Order { get; set; }


        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }


    }
}
