using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class DeleteActivityCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        
        public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, bool>
        {
            private readonly ApplicationContext _context;
            public DeleteActivityCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
          
            public async Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (activity == null)
                {
                    return false;
                }
                activity.IsDeleted = !activity.IsDeleted;
                activity.ModifiedDate = DateTime.Now;
                _context.Activities.Update(activity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
