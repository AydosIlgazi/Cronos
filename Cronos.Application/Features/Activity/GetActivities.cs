using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronos.Application.Dtos.Activity;

namespace Cronos.Application.Features.Activity
{
    public class GetActivities
    {
        public class GetActivitiesQuery : IRequest<ActivityViewModel> {
         
        }

        public class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, ActivityViewModel>
        {

            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public GetActivitiesHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ActivityViewModel> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activities.DisplayedEntities().ToListAsync(cancellationToken);

                ActivityViewModel activityViewModel = new ActivityViewModel()
                {
                    Activities = _mapper.Map<List<ActivityDto>>(activities)
                };

                return activityViewModel;
            }
        }
    }
}
