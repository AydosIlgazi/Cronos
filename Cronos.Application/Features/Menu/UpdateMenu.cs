using AutoMapper;
using Cronos.Application.Dtos;
using Cronos.Application.Entities;
using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{

    public class UpdateMenu
    {
        public class UpdateMenuCommand : IRequest<bool>
        {
            public UpdateMenuCommand(MenusDto obj)
            {
                this.Obj = obj;
            }

            public MenusDto Obj { get; private set; }
        }

        public class UpdateMenuHandler : IRequestHandler<UpdateMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public UpdateMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
            {
                var menu = await _context.Menus.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if (menu == null)
                {
                    return false;
                }

                var menuAsDto = request.Obj;

                menu = _mapper.Map<Entities.Menu.Menu>(request.Obj);


                menu.ModifiedDate = DateTime.Now;
                _context.Menus.Update(menu);
               var isUpdated= await _context.SaveChangesAsync(cancellationToken);

                if (isUpdated > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        public class DeleteMenuCommand : IRequest<bool>
        {

            public DeleteMenuCommand(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class DeleteMenuHandler : IRequestHandler<DeleteMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
           private readonly IMapper _mapper;
                public DeleteMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                    _mapper = mapper;
                }

                public async Task<bool> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
                {
                    var menu = await _context.Menus.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                    if (menu == null)
                    {
                        return false;
                    }

                    //---delete child parents also menu=> submenu => submenu2 || Ş.Geyik 30.09.22---
                    var submenus= _context.SubMenus.AsNoTracking().Where(c => c.ParentId == menu.Id);
                List<int> deletedSubMenusIds=new List<int>();

                    foreach (var submenu in submenus)
                    {
                        if (submenu != null)
                        {
                        submenu.IsDeleted = true;
                        submenu.ModifiedDate = DateTime.Now;
                        _context.SubMenus.Update(submenu);

                        deletedSubMenusIds.Add(submenu.Id);
                    
                        }


                  
                    }

                  foreach(var submenuDeletedId in deletedSubMenusIds)
                {

                    var submenus2 = _context.SubMenus2.AsNoTracking().Where(c => c.ParentId ==submenuDeletedId );


                    foreach (var submenu2 in submenus2)
                    {
                        if (submenu2 != null)
                        {
                            submenu2.IsDeleted = true;
                            submenu2.ModifiedDate = DateTime.Now;
                            _context.SubMenus2.Update(submenu2);

                            

                        }



                    }

                }

                // End ---- delete child parents also menu=> submenu => submenu2 || Ş.Geyik 30.09.22 End ----


                menu.IsDeleted = true;
                    menu.ModifiedDate = DateTime.Now;
                    _context.Menus.Update(menu);
                  var isDeleted = await _context.SaveChangesAsync(cancellationToken);

              

                if (isDeleted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

    }

}
