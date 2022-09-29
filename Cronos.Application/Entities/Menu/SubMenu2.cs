using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Entities.Menu
{
   public class SubMenu2:BaseEntity
    {

        [Required]
        public int ParentId { get; set; }

        [Required]
        public string? SubMenuName { get; set; }

        [Required]
        public string? Content { get; set; }



    }
}
