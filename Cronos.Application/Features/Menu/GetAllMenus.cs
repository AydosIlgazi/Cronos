using Cronos.Application.Dtos;
using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{
    public class GetAllMenus
    {

        public class GetAllMenusQuery : IRequest<MenusViewModel>
        {

        }
        public class GetAllMenusHandler : IRequestHandler<GetAllMenusQuery, MenusViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetAllMenusHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MenusViewModel> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
            {

                var menus = await _context.Menus.DisplayedEntities().AsNoTracking().ToListAsync(cancellationToken);

                var submenus = await _context.SubMenus.DisplayedEntities().AsNoTracking().ToListAsync(cancellationToken);

                var submenus2 = await _context.SubMenus2.DisplayedEntities().AsNoTracking().ToListAsync(cancellationToken);

                MenusViewModel menuViewModel = new MenusViewModel()
                {

                    Menus = _mapper.Map<List<MenusDto>>(menus),
                    SubMenus = _mapper.Map<List<SubMenusDto>>(submenus),
                    SubMenus2= _mapper.Map<List<SubMenus2Dto>>(submenus2),

                };

                return menuViewModel;
            }

        }


    }
}
