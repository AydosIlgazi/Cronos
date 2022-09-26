using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public class UpdateActivityCommand : IRequest<ActivityEntity>
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
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand, ActivityEntity>
        {
            public readonly ApplicationContext _context;
            public UpdateActivityHandler(ApplicationContext context)
            {
                _context = context;
            }
    
            public async Task<ActivityEntity> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if(activity == null)
                {
                    return default;
                }
                else
                {
                    activity.Id = request.Id;
                    activity.CreatedDate = request.CreatedDate;
                    activity.ModifiedDate = request.ModifiedDate;
                    activity.StartDate = request.StartDate;
                    activity.EndDate = request.EndDate;
                    activity.Order = request.Order;
                    activity.IsActive = request.IsActive;
                    activity.IsDeleted = request.IsDeleted;
                    activity.Title = request.Title;
                    activity.Info = request.Info;
                    activity.Latitude = request.Latitude;
                    activity.Longitude = request.Longitude;

                    await _context.SaveChangesAsync();
                    return activity;

                }


            }
        }
    }
}
