using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class DeleteActivityCommand : IRequest<ActivityEntity>
    {
        public int Id { get; set; } 
        public bool isDeleted { get; set; }
        public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, ActivityEntity>
        {
            private readonly ApplicationContext _context;
            public DeleteActivityCommandHandler(ApplicationContext context)
            {
                _context = context; 
            }
            public async Task<ActivityEntity> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                activity.IsDeleted = true;
                await _context.SaveChangesAsync();
                return activity;
            }
        }
    }
}
