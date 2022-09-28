using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string locationUrl { get; set; }

        public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand, bool>
        {

            public readonly ApplicationContext _context;
            public UpdateActivityHandler(ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null)
                {
                    return false;
                }
                
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
