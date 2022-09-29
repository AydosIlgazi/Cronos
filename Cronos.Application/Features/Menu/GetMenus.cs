using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{
    public class GetMenus
    {
        public class GetMenusQuery : IRequest<MenuViewModel>
        {

        }
        public class GetMenusHandler : IRequestHandler<GetMenusQuery, MenuViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetMenusHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MenuViewModel> Handle(GetMenusQuery request, CancellationToken cancellationToken)
            {

                var menus = await _context.Menus.DisplayedEntities().AsNoTracking().ToListAsync(cancellationToken);

                MenuViewModel menuViewModel = new MenuViewModel()
                {
                    Menus = _mapper.Map<List<MenusDto>>(menus),
                };



                return menuViewModel;

            }

        }



    }
}
