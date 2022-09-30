using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cronos.Application.Features.Activity
{
    public class UpdateActivityCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string? locationUrl { get; set; }

        public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand, bool>
        {

            public readonly ApplicationContext _context;
            public UpdateActivityHandler(ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
            {

                var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null)
                {
                    return false;
                }
                else
                {
                    //Order algoritması
                    
                    bool SameOrder = false;
                    bool NewOrderSmall = true;
                    var currentActivity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == request.Id);
                    
                    int OrderBefore = currentActivity.Order;
                    if (currentActivity == null)
                    {
                        return false;
                    }
                    activity.Order = request.Order;
                    if (activity.Order > OrderBefore)
                    {
                        NewOrderSmall = false;
                    }
                    List<ActivityEntity> entities = await _context.Activities.ToListAsync();
                    foreach (var item in entities)
                    {
                        if (item.Order == request.Order && item.Id != request.Id)
                        {
                            SameOrder = true;
                        }
                    }
                    if (SameOrder == true)
                    {
                        foreach (var item in entities)
                        {
                            if (item.Order >= request.Order && item.Order < OrderBefore && NewOrderSmall == true && item.Id != request.Id)
                            {
                                item.Order++;
                               
                            }
                            else if (item.Order <= request.Order && item.Order > OrderBefore && NewOrderSmall == false && item.Id != request.Id)
                            {
                                item.Order--;
                                
                            }
                        }
                    }
                    //Order algoritması sonu

                    activity.Id = request.Id;
                        activity.CreatedDate = request.CreatedDate;
                        activity.ModifiedDate = DateTime.Now;
                        activity.StartDate = request.StartDate;
                        activity.EndDate = request.EndDate;
                        activity.Order = request.Order;
                        activity.IsActive = request.IsActive;
                        activity.IsDeleted = request.IsDeleted;
                        activity.Title = request.Title;
                        activity.Info = request.Info;
                        activity.locationUrl = request.locationUrl;

                        _context.Activities.Update(activity);
                        await _context.SaveChangesAsync(cancellationToken);
                        return true;
                    }


                }    

        }

    }

    
}
