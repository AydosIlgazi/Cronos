using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Entities.Menu
{
    public class Menu:BaseEntity
    {
        

        [Required]
        public string? MenuName { get; set; }

        [Required]
        public string? Content{ get; set; }

        




    }
}
