using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cronos.Application.Features.Menu.CreateMenu;

namespace Cronos.Application.Features.SubMenu
{
    public class CreateSubMenu
    {
        public class CreateSubMenuCommand : IRequest<bool>
        {
            public CreateSubMenuCommand(SubMenusDto obj)
            {
                this.Obj = obj;
            }

            public SubMenusDto Obj { get; private set; }
        }

        public class CreateSubMenuHandler : IRequestHandler<CreateSubMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public CreateSubMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(CreateSubMenuCommand request, CancellationToken cancellationToken)
            {
                var submenu = new Entities.Menu.SubMenu();
                submenu = _mapper.Map(request.Obj, submenu);
                await _context.SubMenus.AddAsync(submenu);
                var isSaved = await _context.SaveChangesAsync(cancellationToken);
                if (isSaved > 0)
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
