using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public  class AddActivityCommand : IRequest<ActivityEntity>
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

        public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, ActivityEntity>
        {
            public readonly ApplicationContext _context;

            public AddActivityCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
              
            public async Task<ActivityEntity> Handle(AddActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = new ActivityEntity();
                activity.Title = request.Title;
                activity.Longitude = request.Longitude;
                activity.Latitude = request.Latitude;
                activity.StartDate = request.StartDate; 
                activity.EndDate = request.EndDate;
                activity.CreatedDate = DateTime.Now;
                activity.ModifiedDate = DateTime.Now;
                activity.Info = request.Info;
                activity.Order = request.Order;
                activity.IsActive = request.IsActive;
                activity.IsDeleted = false;

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return activity;

            }
        }
    }
}
