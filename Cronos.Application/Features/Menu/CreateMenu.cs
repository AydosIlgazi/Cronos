using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{
    public class CreateMenu
    {
        public class CreateMenuCommand : IRequest<bool>
        {
            public CreateMenuCommand(MenusDto obj)
            {
                this.Obj = obj;
            }

            public MenusDto Obj { get; private set; }
        }

        public class CreateMenuHandler : IRequestHandler<CreateMenuCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public CreateMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
            {
                var menu = new Entities.Menu.Menu();
                menu = _mapper.Map(request.Obj, menu);
                await _context.Menus.AddAsync(menu);
              var isSaved =  await _context.SaveChangesAsync(cancellationToken);
                if (isSaved>0)
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
