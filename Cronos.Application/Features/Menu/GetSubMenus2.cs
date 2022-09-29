using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{
    public class GetSubMenus2
    {
        public class GetSubMenus2Query : IRequest<SubMenu2ViewModel>
        {

        }
        public class GetSubMenusHandler : IRequestHandler<GetSubMenus2Query, SubMenu2ViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetSubMenusHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SubMenu2ViewModel> Handle(GetSubMenus2Query request, CancellationToken cancellationToken)
            {

                var submenus2 = await _context.SubMenus2.DisplayedEntities().ToListAsync(cancellationToken);


                SubMenu2ViewModel subMenu2ViewModel = new SubMenu2ViewModel()
                {
                    SubMenus2 = _mapper.Map<List<SubMenus2Dto>>(submenus2),
                };



                return subMenu2ViewModel;

            }

        }


    }
}
