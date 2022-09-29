using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cronos.Application.Features.Menu.CreateMenu;

namespace Cronos.Application.Features.SubMenu2
{
    public class CreateSubMenu2
    {
        public class CreateSubMenu2Command : IRequest<bool>
        {
            public CreateSubMenu2Command(SubMenus2Dto obj)
            {
                this.Obj = obj;
            }

            public SubMenus2Dto Obj { get; private set; }
        }

        public class CreateSubMenuHandler : IRequestHandler<CreateSubMenu2Command, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public CreateSubMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(CreateSubMenu2Command request, CancellationToken cancellationToken)
            {
                var submenu2 = new Entities.Menu.SubMenu2();
                submenu2 = _mapper.Map(request.Obj, submenu2);
                await _context.SubMenus2.AddAsync(submenu2);
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
