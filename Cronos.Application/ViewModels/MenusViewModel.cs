using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronos.Application.Entities.Menu;

namespace Cronos.Application.ViewModels
{
    //Menus ve SubMenus SubMenus2 tablolarını  kapsayan  View Model -- Şerif Geyik 22.09.22 --
    public class MenusViewModel
    {

        public List<MenusDto> Menus { get; set; }

        public List<SubMenusDto> SubMenus { get; set; }

        public List<SubMenus2Dto> SubMenus2 { get; set; }

    }
}
