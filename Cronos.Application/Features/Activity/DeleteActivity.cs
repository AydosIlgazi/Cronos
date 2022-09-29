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

                if (activity.IsDeleted == false)
                {
                    List<ActivityEntity> entities = await _context.Activities.DisplayedEntitiesCms().ToListAsync();
                    var lastItem = entities.LastOrDefault();
                    int oldOrder = activity.Order;
                    activity.Order = lastItem.Order;
                    foreach (var item in entities)
                    {
                        if (item.Order > oldOrder && item.Id != activity.Id)
                        {
                            item.Order--;

                        }
                    }

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
