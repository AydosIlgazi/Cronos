using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Dtos
{
    public class MenusDto
    {

        public int Id { get; set; }

        public string MenuName { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ModifiedDate{ get; set; }
        public DateTime EndDate { get; set; }

        public string Content { get; set; }
   
     

    }
}
