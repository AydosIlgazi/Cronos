using Cronos.Application.Dtos;
using Cronos.Application.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Menu
{
    public class GetSubMenus
    {

        public class GetSubMenusQuery : IRequest<SubMenuViewModel>
        {

        }
        public class GetSubMenusHandler : IRequestHandler<GetSubMenusQuery, SubMenuViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetSubMenusHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SubMenuViewModel> Handle(GetSubMenusQuery request, CancellationToken cancellationToken)
            {

                var submenus = await _context.SubMenus.DisplayedEntities().ToListAsync(cancellationToken);


                SubMenuViewModel subMenuViewModel = new SubMenuViewModel()
                {
                    SubMenus = _mapper.Map<List<SubMenusDto>>(submenus),
                };



                return subMenuViewModel;

            }

        }


    }
}
