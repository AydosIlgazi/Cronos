using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class GetActivityByIdQuery: IRequest<ActivityEntity>
    {
        public int Id { get; set; }

        public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityEntity>
        {   
            private readonly ApplicationContext _context;
            public GetActivityByIdQueryHandler(ApplicationContext context)
            {
                _context = context; 
            }
            public async Task<ActivityEntity> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                return activity;
            }
        }
    }
}
