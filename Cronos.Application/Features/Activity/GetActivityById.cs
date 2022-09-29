using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class GetActivityByIdQuery: IRequest<UpdateActivityViewModel>
    {
        public int Id { get; set; }

        public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, UpdateActivityViewModel>
        {

            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetActivityByIdQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
          
            public async Task<UpdateActivityViewModel> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Where(x => x.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
                if(activity == null) { return null; }

                UpdateActivityViewModel activityUpdate = _mapper.Map<UpdateActivityViewModel>(activity);
                return activityUpdate;
            }
        }
    }
}
