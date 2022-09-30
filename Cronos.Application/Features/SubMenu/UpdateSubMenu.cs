﻿using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.SubMenu
{
    public class UpdateSubMenu
    {
        public class UpdateSubMenuCommand : IRequest<bool>
        {
            public UpdateSubMenuCommand(SubMenusDto obj)
            {
                this.Obj = obj;
            }

            public SubMenusDto Obj { get; private set; }
        }

        public class UpdateSubMenuHandler : IRequestHandler<UpdateSubMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public UpdateSubMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateSubMenuCommand request, CancellationToken cancellationToken)
            {
                var submenu = await _context.SubMenus.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if (submenu == null)
                {
                    return false;
                }


                var menuAsDto = request.Obj;

                submenu = _mapper.Map<Entities.Menu.SubMenu>(request.Obj);


                submenu.ModifiedDate = DateTime.Now;
                _context.SubMenus.Update(submenu);
                var isUpdated = await _context.SaveChangesAsync(cancellationToken);

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

        public class DeleteSubMenuCommand : IRequest<bool>
        {

            public DeleteSubMenuCommand(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class DeleteMenuHandler : IRequestHandler<DeleteSubMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public DeleteMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(DeleteSubMenuCommand request, CancellationToken cancellationToken)
            {
                var submenu = await _context.SubMenus.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                if (submenu == null)
                {
                    return false;
                }

                //---delete child parents also submenu => submenu2 || Ş.Geyik 30.09.22---
                var submenus2 = _context.SubMenus2.AsNoTracking().Where(c => c.ParentId == submenu.Id);


                foreach (var submenu2 in submenus2)
                {
                    if (submenu2 != null)
                    {
                        submenu2.IsDeleted = true;
                        submenu2.ModifiedDate = DateTime.Now;
                        _context.SubMenus2.Update(submenu2);



                    }



                }


                //---delete child parents also submenu => submenu2 || Ş.Geyik 30.09.22---



                submenu.IsDeleted = true;
                submenu.ModifiedDate = DateTime.Now;
                _context.SubMenus.Update(submenu);
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
