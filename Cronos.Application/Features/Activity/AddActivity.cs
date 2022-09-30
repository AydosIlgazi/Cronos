using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Activity
{
    public  class AddActivityCommand : IRequest<bool>
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


        public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, bool>
        {

            public readonly ApplicationContext _context;

            public AddActivityCommandHandler(ApplicationContext context)
            {
                _context = context;
            }

            
            public async Task<bool> Handle(AddActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = new ActivityEntity();

                activity.Title = request.Title;
                activity.StartDate = request.StartDate;
                activity.EndDate = request.EndDate;
                activity.locationUrl = request.locationUrl;
                activity.CreatedDate = DateTime.Now;
                activity.ModifiedDate = DateTime.Now;
                activity.Info = request.Info;
                activity.Order = request.Order;
                activity.IsActive = request.IsActive;
                activity.IsDeleted = false;

                //Araya yeni aktivity eklendiğinde, eklenen orderdan sonraki activity'nin order'ları 1er artar.

                bool isSameOrder = false;
                List<ActivityEntity> entities = await _context.Activities.ToListAsync();

                foreach (var item in entities)
                {
                    if (item.Order == activity.Order)
                    {
                        isSameOrder = true;
                    }
                    if(isSameOrder == true)
                    {
                        if (item.Order >= activity.Order)
                        {
                            item.Order++;
                        }
                    }
                }

                //Order sonu


                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
