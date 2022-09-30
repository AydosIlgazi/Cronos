using Cronos.Application.Dtos.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class GetAllActivityQuery :IRequest<ActivityViewModel>
    {

        public class GetAllActivityHandler : IRequestHandler<GetAllActivityQuery, ActivityViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetAllActivityHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActivityViewModel> Handle(GetAllActivityQuery request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activities.DisplayedEntitiesCms().ToListAsync(cancellationToken);

                ActivityViewModel activityViewModel = new ActivityViewModel()
                {
                    Activities = _mapper.Map<List<ActivityDto>>(activities)
                };

                return activityViewModel;
            }
        }
    }
}
